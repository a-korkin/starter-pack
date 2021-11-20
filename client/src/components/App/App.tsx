import React from "react";
import TopPanel from "../TopPanel";
import Sidebar from "../Sidebar";

import "./App.scss";

const App: React.FC = () => {
    return (
        <div className="container">
            <TopPanel />
            <Sidebar />
            <div className="content">This is content</div>
        </div>
    );
}

export default App;
