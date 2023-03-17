module Fsdemo.TodoStore

open System
open BizLayer

type DefaultToDoStore (initialItems:ToDoItem list) =
    let mutable items = initialItems
    member this.AddToDo (newToDo:string) : IncompleteToDoItem =
        let result =
            {
                Description = newToDo
            }

        items <-
            ToDoItem.Incomplete result
            |> List.singleton
            |> List.append items

        result

    member this.GetToDos() =
        items

    member this.GetToDo index : ToDoItem option =
        items
        |> List.tryItem index

    member this.ToggleToDo index =
        let mItem = this.GetToDo index
        match mItem with
        | None -> InvalidIndex index |> Error
        | Some item -> 
            match item with
            | Incomplete _ ->
                let result =
                    {
                        Description = item.Description
                        CompletedDate = DateTimeOffset.Now
                    }
                    |> Complete

                items <- List.updateAt index result items

                Ok result
            | Complete _ ->
                let result =
                    {
                        Description = item.Description
                    }
                    |> Incomplete

                items <- List.updateAt index result items

                Ok result
                
    member this.ClearCompletedToDos() =
        items <-
            items
            |> List.filter (ToDoItem.isComplete >> not)

    interface IToDoStore with
        member this.AddToDo newToDo = this.AddToDo newToDo
        member this.GetToDo index = this.GetToDo index
        member this.GetToDos() = this.GetToDos()
        member this.ToggleToDo index = this.ToggleToDo index
        member this.ClearCompletedToDos() = this.ClearCompletedToDos()