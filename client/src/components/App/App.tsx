import React, { useState } from "react";
import TopPanel from "../TopPanel";
import Sidebar from "../Sidebar";
import Content from "../Content";

import "./App.scss";

const App: React.FC = () => {
    const [collapse, setCollapse] = useState<boolean>(false);

    return (
        <div className={collapse ? "container collapsed" : "container"}>
            <TopPanel collapseSidebar={setCollapse} />
            <Sidebar collapse={collapse} />
            <Content />
        </div>
    );
}

export default App;
