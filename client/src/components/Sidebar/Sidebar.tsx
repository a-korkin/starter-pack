import React, { useEffect, useRef } from "react";
import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";

import "./Sidebar.scss";

interface ISidebarProps {
    collapse: boolean;
}

const Sidebar: React.FC<ISidebarProps> = ({collapse}) => {
    const { fetchEntityTypes } = useActions();
    const { isLoading, error, data } = useTypedSelector(state => state.entityTypes);

    const _fetchEntityTypes = useRef(() => {});
    _fetchEntityTypes.current = fetchEntityTypes;

    useEffect(() => {
        _fetchEntityTypes.current();
    }, [])

    console.log(data);

    return (
        <div 
            className={collapse ? "sidebar sidebar--collapsed" : "sidebar"}
        >
            {isLoading && <div>Loading...</div>}
            {error && <div>Fuuuuuucckkkk</div>}
            This is sidebar
        </div>
    );
}

export default Sidebar;
