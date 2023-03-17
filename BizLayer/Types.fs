[<AutoOpen>]
module BizLayer.Types

open System

type ToggleError =
    | InvalidIndex of int

type CompleteToDoItem =
    {
        Description: string
        CompletedDate: DateTimeOffset
    }

type IncompleteToDoItem =
    {
        Description: string
    }

type ToDoItem =
    | Complete of CompleteToDoItem
    | Incomplete of IncompleteToDoItem
with
    member this.Description =
        match this with
        | Complete x -> x.Description
        | Incomplete x -> x.Description

[<RequireQualifiedAccess>]
module ToDoItem = 
    let isComplete toDoItem: bool =
        match toDoItem with
        | Complete _ -> true
        | Incomplete _ -> false