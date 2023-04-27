import {Category} from "./Category";

export interface ToDo {
    id: number
    title: string
    description: string | null
    dueDate: string | null
    categoryId: number | null
    category: Category | null
    status: "In progress" | "Completed"
}

export const EmptyToDo: ToDo = {
    id: 0,
    title: "",
    description: null,
    dueDate: null,
    categoryId: null,
    category: null,
    status: "In progress"
}