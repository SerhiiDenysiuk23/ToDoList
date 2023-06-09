import React from 'react';
import Header from "./components/Header";
import ToDoList from "./components/ToDoList";
import {BrowserRouter, Routes, Route} from "react-router-dom";
import CategoryList from "./components/CategoryList";
import CookiePopup from "./components/CookiePopup";

function App() {
    return (
        <BrowserRouter>
            <Header/>
            <Routes>
                <Route path={"/"} element={<ToDoList/>}/>
                <Route path={"/categories"} element={<CategoryList/>}/>
            </Routes>
            <CookiePopup/>
        </BrowserRouter>
    );
}

export default App;
