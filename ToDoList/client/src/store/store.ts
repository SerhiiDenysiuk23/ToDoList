import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {combineEpics, createEpicMiddleware} from "redux-observable";
import {todoSlice} from "./ToDo/todoSlice";
import {categorySlice} from "./Category/categorySlice";
import {todoEpics} from "./ToDo/todoEpic";
import {categoryEpics} from "./Category/categoryEpic";

const epicMiddleware = createEpicMiddleware()

const rootEpic = combineEpics(
    todoEpics,
    categoryEpics
)

const rootReducer = combineReducers({
    todo: todoSlice.reducer,
    category: categorySlice.reducer
})

export const store = configureStore({
    reducer: {
        rootReducer
    },
    middleware: getDefaultMiddleware =>
        getDefaultMiddleware({
            serializableCheck: false,
        }).concat(epicMiddleware)
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
epicMiddleware.run(rootEpic)