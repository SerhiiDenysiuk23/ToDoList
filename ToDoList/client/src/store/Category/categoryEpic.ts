import {categoryCreate, categoryDelete, categoryGetList} from "../../api/queries/categoryQueries";
import {
    create_category,
    createCategoryAction, delete_category,
    deleteCategoryAction,
    fetch_category_list,
    fetchCategoryListAction
} from "./categorySlice";
import {from, map, mergeMap, Observable} from "rxjs";
import {request} from "../../api/core";
import {combineEpics, Epic, ofType} from "redux-observable";
import {Category} from "../../types/Category";
import {PayloadAction} from "@reduxjs/toolkit";

const fetchCategoryListEpic: Epic = ($action: Observable<ReturnType<typeof fetchCategoryListAction>>) => {
    return $action.pipe(
        ofType(fetchCategoryListAction.type),
        mergeMap(() => from(request(categoryGetList)).pipe(
            map(response => {
                try {
                    return fetch_category_list(response.data.categoryQuery.categoryGetList);
                } catch (e) {
                    console.error(e)
                }
            })
        ))
    )
}

const createCategoryEpic: Epic = ($action: Observable<ReturnType<typeof createCategoryAction>>) => {
    return $action.pipe(
        ofType(createCategoryAction.type),
        mergeMap((action: PayloadAction<Category>) => from(request(categoryCreate, {
            category: {
                id: action.payload.id,
                name: action.payload.name
            } as Category
        })).pipe(
            map(response => {
                try {
                    return create_category(response.data.categoryMutation.categoryCreate);
                } catch (e) {
                    console.error(e)
                }
            })
        ))
    )
}

const deleteCategoryEpic: Epic = ($action: Observable<ReturnType<typeof deleteCategoryAction>>) => {
    return $action.pipe(
        ofType(deleteCategoryAction.type),
        mergeMap((action: PayloadAction<number>) => from(request(categoryDelete, {
            id: action.payload
        })).pipe(
            map(response => {
                try {
                    if (response.data.categoryMutation.categoryDelete)
                        return delete_category(action.payload);
                } catch (e) {
                    console.error(e)
                }
            })
        ))
    )
}

export const categoryEpics = combineEpics(fetchCategoryListEpic, createCategoryEpic, deleteCategoryEpic)