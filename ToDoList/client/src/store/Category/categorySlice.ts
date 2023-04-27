import {createAction, createSlice, PayloadAction} from "@reduxjs/toolkit";
import {ToDo} from "../../types/ToDo";
import {filter} from "rxjs";
import {Category} from "../../types/Category";

interface categoryState {
    categoryList: Category[]
}

const initialState: categoryState = {
    categoryList: []
}

export const categorySlice = createSlice({
    name: "categorySlice",
    initialState,
    reducers: {
        fetch_category_list:(state, action: PayloadAction<Category[]>) => {
            return {...state, categoryList: action.payload}
        },
        create_category:(state, action: PayloadAction<Category>) =>{
            return {...state, categoryList: [action.payload, ...state.categoryList]}
        },
        delete_category:(state, action:PayloadAction<number>)=>{
            return {...state, categoryList: state.categoryList.filter(item => item.id != action.payload)}
        }
    }
})

export const fetchCategoryListAction = createAction<undefined>("fetchCategoryList")
export const createCategoryAction = createAction<Category>("createCategory")
export const deleteCategoryAction = createAction<number>("deleteCategory")

export const {fetch_category_list, create_category, delete_category} = categorySlice.actions