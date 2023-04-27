export const categoryGetList = `
query CategoryGetList{
  categoryQuery {
    categoryGetList {
      id
      name
    }
  }
}
`

export const categoryCreate = `
mutation CategoryCreate($category: CategoryInput){
  categoryMutation {
    categoryCreate(category: $category) {
      id
      name
    }
  }
}
`

export const categoryDelete = `
mutation categoryDelete($id: ID){
  categoryMutation {
    categoryDelete(id: $id)
  }
}
`