module Fsdemo.Program

open System
open Fsdemo.Types
open BizLayer
open TodoStore

let printItem i item=
    printfn "\t%i. [%c] %s" i (if ToDoItem.isComplete item then 'x' else ' ') item.Description

let showAllList store =
    getToDos store
    |> List.iteri printItem

let rec loop (toDoStore: IToDoStore): int = 
    let input = Console.ReadLine()
    let command = Command.ofString input

    match command with
    | Error msg ->
        printfn "%s" msg
        loop toDoStore
    | Ok cmd -> 
        match cmd with
        | Command.Help ->
            printHelp()
            loop toDoStore
        | Command.Clear -> 
            clearCompleted toDoStore
            showAllList toDoStore
            loop toDoStore
        | Command.Toggle i ->
            toggleToDo toDoStore i |> ignore
            showAllList toDoStore
            loop toDoStore
        | Command.ListAll ->
            printfn "Printing entire list:"
            showAllList toDoStore
            loop toDoStore
        | Command.List i ->
            match getToDo toDoStore i with
            | None -> printfn "no item was found at: %i" i
            | Some t -> printItem i t
            loop toDoStore
        | Command.Add str ->
            addToDo toDoStore str |> ignore
            showAllList toDoStore
            loop toDoStore
        | Command.Exit -> 0
        
[<EntryPoint>]
let main args =
    printHelp()
    DefaultToDoStore []
    |> loop 