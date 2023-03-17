[<AutoOpen>]
module TodoList.Core.Interfaces

type IToDoStore = 
    abstract AddToDo: string -> IncompleteToDoItem
    abstract ToggleToDo: int -> Result<ToDoItem, ToggleError>
    abstract GetToDos: unit -> ToDoItem list
    abstract GetToDo: int -> ToDoItem option
    abstract ClearCompletedToDos: unit -> unit