import React, { useState } from "react";

import "./TopPanel.scss";

interface ITopPanelProps {
    collapseSidebar: (act: boolean) => void;
}

const TopPanel: React.FC<ITopPanelProps> = ({collapseSidebar}) => {
    const [active, setActive] = useState<boolean>(false);

    const onClickHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        setActive(!active);
        collapseSidebar(!active);
    }

    return (
        <div className="top-panel">
            <input 
                className="burger-input"
                type="checkbox" 
                name="burger" 
                id="burger"
                onChange={e => onClickHandler(e)}
            />
            <label 
                className="burger-label"
                htmlFor="burger"
                // onClick={e => onClickHandler(e)}
            >
                <span className="burger-line"></span>
                <span className="burger-line"></span>
                <span className="burger-line"></span>
            </label>
        </div>
    );
}

export default TopPanel;
