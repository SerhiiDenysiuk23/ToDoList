import React, {useEffect, useState} from 'react';
import {useAppSelector} from "../hooks/useAppDispatch";
import {useDispatch} from "react-redux";
import {createCategoryAction, deleteCategoryAction, fetchCategoryListAction} from "../store/Category/categorySlice";
import {Category, EmptyCategory} from "../types/Category";

const CategoryList = () => {
    const {categoryList} = useAppSelector(state => state.rootReducer.category)
    const [category, setCategory] = useState<Category>(EmptyCategory)

    const dispatch = useDispatch()
    const handleDeleteButtonClick = (id: number): void => {
        dispatch(deleteCategoryAction(id))
    }

    const handleInputNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setCategory({...category, name: e.target.value})
    }

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        dispatch(createCategoryAction(category))
    }

    useEffect(() => {
        dispatch(fetchCategoryListAction())
    }, [])

    return (
        <section>
            <form onSubmit={handleFormSubmit}>
                <input placeholder="Name" onChange={handleInputNameChange} type="text"/>
                <button type="submit">Create</button>
            </form>
            <ul>
                {
                    categoryList.map(item => <li key={item.id}>
                        <div>{item.name}</div>
                        <button onClick={() =>{ handleDeleteButtonClick(item.id)}}>Delete</button>
                    </li>)
                }
            </ul>
        </section>
    );
};

export default CategoryList;