import React, {useState} from 'react';


const dbList: string[] = ["SQL", "XML"]

const Header = () => {
    const [currentDB, setCurrentDB] = useState<string>(dbList[0])

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        // e.preventDefault()
        document.cookie = `dbType=${currentDB}`
        // document.location.reload()
    }

    const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setCurrentDB(e.target.value);
    }

    return (
        <header className="header">
            <nav>
                <ul>
                    <li><a href="/">ToDo</a></li>
                    <li><a href="/categories">Category</a></li>
                </ul>
            </nav>

            <form className="db-switch" onSubmit={handleFormSubmit}>
                <select onChange={handleSelectChange}>
                    {
                        dbList.map(item =>
                            <option selected={(document.cookie.split("; ").find(row => row.startsWith("dbType="))?.split("=")[1]) == item}
                                    key={item}
                                    value={item}>
                                {item}
                            </option>)
                    }
                </select>
                <button type="submit">Change DB</button>
            </form>
        </header>
    );
};

export default Header;