module TodoList.InMemory.Types

open System

let printHelp() = 
    printfn "to add a todo:"
    printfn "add <description> "
    printfn "to toggle a todo:"
    printfn "toggle <index>"
    printfn "Access one item or list all:"
    printfn "list <index/all>"
    printfn "to clear todos:"
    printfn "clear"
    printfn "to exit:"
    printfn "exit"

//Declare an active pattern method
let (|Prefix|_|) (prefix: string) (str: string) = 
    if (str.ToLower().StartsWith(prefix.ToLower())) then
        match (str.Substring(prefix.Length).Trim()) with
        | "" -> None
        | rest -> Some rest
    else None

//If the active pattern parse works, return int value
let (|Index|_|) (str: string) =
    match Int32.TryParse(str) with
    | (true, i) -> if i >= 0 then Some i else None
    | _ -> None

let (|Exact|_|) (value: string) (str: string) =
    if (value.ToLower() = str.ToLower().Trim()) then
        Some value
    else
        None

type Command =
    | Add of string
    | Toggle of int
    | List of int
    | ListAll
    | Clear
    | Exit
    | Help

[<RequireQualifiedAccess>]
module Command =
    let ofString (cmd: string): Result<Command, string> = 
        match cmd with
        | Prefix "Add " (rest: string) -> Command.Add rest |> Ok
        | Prefix "Toggle " (rest: string) -> 
            match rest with
            | Index i -> Command.Toggle i |> Ok
            | _ -> sprintf "Unrecognized index %s" rest |> Error //typesafe error handled
        | Exact "List all" _ -> Command.ListAll |> Ok
        | Prefix "List " (rest: string) -> 
            match rest with
            | Index i -> Command.List i |> Ok
            | _ -> sprintf "Unrecognized index %s" rest |> Error //typesafe error handled
        | Exact "Clear" _ -> Command.Clear |> Ok
        | Exact "Help" _ -> Command.Help |> Ok
        | Exact "Exit" _ -> Command.Exit |> Ok
        | _ -> sprintf "Unrecognized command: %s" cmd |> Error