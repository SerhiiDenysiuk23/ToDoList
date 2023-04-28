import React from 'react';
import {DataBases} from "../types/enums";

const Header = () => {
    return (
        <header className="header">
            <nav>
                <ul>
                    <li><a href="/">ToDo</a></li>
                    <li><a href="/categories">Category</a></li>
                </ul>
            </nav>
        </header>
    );
};

export default Header;