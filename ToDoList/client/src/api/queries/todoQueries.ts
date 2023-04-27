export const toDoGetList = `
query ToDoGetList{
  toDoQuery {
    toDoGetList {
      id
      title
      description
      dueDate
      status
      categoryId
      category {
        id
        name
      }
    }
  }
}
`

export const toDoCreate = `
mutation ToDoCreate($toDo: ToDoInput){
  toDoMutation {
    toDoCreate(toDo: $toDo) {
      id
      title
      description
      dueDate
      status
      categoryId
      category {
        id
        name
      }
    }
  }
}
`

export const toDoUpdate = `
mutation ToDoUpdate($toDo: ToDoInput){
  toDoMutation {
    toDoUpdate(toDo: $toDo) {
      id
      title
      description
      dueDate
      status
      categoryId
      category {
        id
        name
      }
    }
  }
}
`

export const toDoDelete = `
mutation ToDoDelete($id: ID){
  toDoMutation {
    toDoDelete(id: $id)
  }
}
`