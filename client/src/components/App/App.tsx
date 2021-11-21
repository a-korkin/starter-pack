import React, { useState } from "react";
import TopPanel from "../TopPanel";
import Sidebar from "../Sidebar";

import "./App.scss";

const App: React.FC = () => {
    const [collapse, setCollapse] = useState<boolean>(false);

    return (
        <div className={collapse ? "container collapsed" : "container"}>
            <TopPanel collapseSidebar={setCollapse} />
            <Sidebar collapse={collapse} />
            <div className="content">This is content</div>
        </div>
    );
}

export default App;
