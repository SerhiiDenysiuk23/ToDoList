import React, {useEffect, useState} from 'react';
import {useAppSelector} from "../hooks/useAppDispatch";
import {useDispatch} from "react-redux";
import {fetchCategoryListAction} from "../store/Category/categorySlice";
import {EmptyToDo, ToDo} from "../types/ToDo";
import {createToDoAction} from "../store/ToDo/todoSlice";

const ToDoCreator = () => {
    const [toDo, setToDo] = useState<ToDo>(EmptyToDo)
    const dispatch = useDispatch()
    const {categoryList} = useAppSelector(state => state.rootReducer.category)

    useEffect(() => {
        dispatch(fetchCategoryListAction())
    }, [])

    const handleInputTitleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setToDo({...toDo, title: e.target.value})
    }
    const handleTextareaDescriptionChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setToDo({...toDo, description: e.target.value})
    }
    const handleInputDueDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const dueDate = new Date(e.target.value);
        setToDo({...toDo, dueDate: dueDate.toISOString()})
    }
    const handleSelectCategoryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setToDo({
            ...toDo,
            categoryId: e.target.value != "null" ? parseInt(e.target.value) : null
        })
    }
    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        dispatch(createToDoAction(toDo))
    }

    useEffect(() => {
        if (toDo.categoryId)
            setToDo({...toDo, category: categoryList.find(item => item.id == toDo.categoryId) ?? null})
    }, [toDo.categoryId])

    return (
        <form onSubmit={handleFormSubmit}>
            <input onChange={handleInputTitleChange} type="text" placeholder="Title"/>
            <textarea onChange={handleTextareaDescriptionChange} placeholder="Description"/>
            <input onChange={handleInputDueDateChange} type="datetime-local"/>
            <select onChange={handleSelectCategoryChange}>
                <option value="null">No Choice</option>
                {
                    !!categoryList &&
                    categoryList.map(item =>
                        <option
                            key={item.id}
                            value={item.id}>
                            {item.name}
                        </option>)
                }
            </select>
            <button type="submit">Create</button>
        </form>
    );
};

export default ToDoCreator;