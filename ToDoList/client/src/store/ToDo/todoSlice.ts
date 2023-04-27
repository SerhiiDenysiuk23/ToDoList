import {createAction, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {ToDo} from "../../types/ToDo";
import {filter} from "rxjs";

interface todoState {
    todoList: ToDo[]
}

const initialState: todoState = {
    todoList: []
}

export const todoSlice = createSlice({
    name: "todoSlice",
    initialState,
    reducers: {
        fetch_todo_list:(state, action: PayloadAction<ToDo[]>) => {
            return {...state, todoList: action.payload}
        },
        create_todo:(state, action: PayloadAction<ToDo>) =>{
            return {...state, todoList: [action.payload, ...state.todoList]}
        },
        delete_todo:(state, action:PayloadAction<number>)=>{
            return {...state, todoList: state.todoList.filter(item => item.id != action.payload)}
        },
        update_todo:(state, action: PayloadAction<ToDo>)=>{
            const index = state.todoList.findIndex(item => item.id == action.payload.id)
            if (index !== -1) {
                state.todoList[index] = action.payload;
            }
        }
    }
})

export const fetchToDoListAction = createAction<undefined>("fetchToDoList")
export const createToDoAction = createAction<ToDo>("createToDo")
export const updateToDoAction = createAction<ToDo>("updateToDo")
export const deleteToDoAction = createAction<number>("deleteToDo")

export const {fetch_todo_list, create_todo, delete_todo, update_todo} = todoSlice.actions