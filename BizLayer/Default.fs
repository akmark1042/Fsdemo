[<AutoOpen>]
module BizLayer.Default

let addToDo (store: IToDoStore) (description: string) =
    store.AddToDo description

let toggleToDo (store: IToDoStore) (index: int): Result<ToDoItem, ToggleError> =
    store.ToggleToDo index

let getToDo (store: IToDoStore) (index: int) =
    store.GetToDo index

let getToDos (store: IToDoStore) =
    store.GetToDos()

let clearCompleted (store: IToDoStore) =
    store.ClearCompletedToDos()