import {combineEpics, Epic, ofType} from "redux-observable";
import {from, map, mergeMap, Observable} from "rxjs";
import {Action, PayloadAction} from "@reduxjs/toolkit";
import {
    create_todo,
    createToDoAction,
    delete_todo,
    deleteToDoAction,
    fetch_todo_list,
    fetchToDoListAction, update_todo, updateToDoAction
} from "./todoSlice";
import {request} from "../../api/core";
import {toDoCreate, toDoDelete, toDoGetList, toDoUpdate} from "../../api/queries/todoQueries";
import {ToDo} from "../../types/ToDo";

const fetchToDoListEpic: Epic = ($action: Observable<ReturnType<typeof fetchToDoListAction>>) => {
    return $action.pipe(
        ofType(fetchToDoListAction.type),
        mergeMap(() => from(request(toDoGetList)).pipe(
            map(response => {
                try {
                    return fetch_todo_list(response.data.toDoQuery.toDoGetList);
                } catch (e) {
                    console.error(e)
                }
            })
        ))
    )
}

const createToDoEpic: Epic = ($action: Observable<ReturnType<typeof createToDoAction>>) => {
    return $action.pipe(
        ofType(createToDoAction.type),
        mergeMap((action: PayloadAction<ToDo>) => from(request(toDoCreate, {
            toDo: {
                id: action.payload.id,
                title: action.payload.title,
                description: action.payload.description,
                dueDate: action.payload.dueDate,
                categoryId: action.payload.categoryId,
                status: action.payload.status
            } as ToDo
        })).pipe(
            map(response => {
                try {
                    return create_todo(response.data.toDoMutation.toDoCreate);
                } catch (e) {
                    console.error(e)
                }
            })
        ))
    )
}
const updateToDoEpic: Epic = ($action: Observable<ReturnType<typeof updateToDoAction>>) => {
    return $action.pipe(
        ofType(updateToDoAction.type),
        mergeMap((action: PayloadAction<ToDo>) => from(request(toDoUpdate, {
            toDo: {
                id: action.payload.id,
                title: action.payload.title,
                description: action.payload.description,
                dueDate: action.payload.dueDate,
                categoryId: action.payload.categoryId,
                status: action.payload.status,
                category: action.payload.category
            } as ToDo
        })).pipe(
            map(response => {
                try {
                    return update_todo(response.data.toDoMutation.toDoUpdate);
                } catch (e) {
                    console.error(e)
                }
            })
        ))
    )
}

const deleteToDoEpic: Epic = ($action: Observable<ReturnType<typeof deleteToDoAction>>) => {
    return $action.pipe(
        ofType(deleteToDoAction.type),
        mergeMap((action: PayloadAction<number>) => from(request(toDoDelete, {id: action.payload})).pipe(
            map(response => {
                if (response.data.toDoMutation.toDoDelete)
                    try {
                        return delete_todo(action.payload);
                    } catch (e) {
                        console.error(e)
                    }
            })
        ))
    )
}

export const todoEpics = combineEpics(fetchToDoListEpic, createToDoEpic, deleteToDoEpic, updateToDoEpic)