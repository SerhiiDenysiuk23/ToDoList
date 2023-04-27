import React, {FC} from 'react';
import {ToDo} from "../types/ToDo";
import {useDispatch} from "react-redux";
import {deleteToDoAction, updateToDoAction} from "../store/ToDo/todoSlice";

const ToDoItem: FC<{ item: ToDo }> = ({item}) => {

    const dispatch = useDispatch()

    const handleStatusButtonClick = (): void => {
        dispatch(updateToDoAction({...item, status: item.status == "In progress" ? "Completed" : "In progress"}))
    }

    const handleDeleteButtonClick = (): void => {
        dispatch(deleteToDoAction(item.id))
    }

    return (
        <tr>
            <td>{item.title}</td>
            <td>{item.description}</td>
            <td>{item.dueDate}</td>
            <td>{item.category != null ? item.category.name : "NO CATEGORY"}</td>
            <td>
                <button onClick={handleStatusButtonClick}>{item.status}</button>
            </td>
            <td>
                <button onClick={handleDeleteButtonClick}>Delete</button>
            </td>
        </tr>
    );
};

export default ToDoItem;