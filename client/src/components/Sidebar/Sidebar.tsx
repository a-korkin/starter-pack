import React from "react";

import "./Sidebar.scss";

interface ISidebarProps {
    collapse: boolean;
}

const Sidebar: React.FC<ISidebarProps> = ({collapse}) => {
    return (
        <div 
            className={collapse ? "sidebar sidebar--collapsed" : "sidebar"}
        >
            This is sidebar
        </div>
    );
}

export default Sidebar;
