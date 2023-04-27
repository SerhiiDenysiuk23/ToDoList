import React, {useEffect} from 'react';
import {useAppSelector} from "../hooks/useAppDispatch";
import ToDoItem from "../elements/ToDoItem";
import ToDoCreator from "../elements/ToDoCreator";
import {useDispatch} from "react-redux";
import {fetchToDoListAction} from "../store/ToDo/todoSlice";

const ToDoList = () => {
    const {todoList} = useAppSelector(state => state.rootReducer.todo)

    const dispatch = useDispatch()

    useEffect(()=>{
        dispatch(fetchToDoListAction())
    },[])

    return (
        <section>
            <ToDoCreator/>
            <table className={"todo-list-table"}>
                <thead>
                <tr>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Due Date</th>
                    <th>Category</th>
                    <th>Status</th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                {
                    todoList.map(item => <ToDoItem key={item.id} item={item}/>)
                }
                </tbody>
            </table>
        </section>
    );
};

export default ToDoList;