import React, { useState } from "react";
import { FaCheck } from "react-icons/fa";

import "./CheckBox.scss";

interface ICheckBoxProps {
    id: string;
    label: string;
    checked: boolean;
}

const CheckBox: React.FC<ICheckBoxProps> = ({id, label, checked}) => {
    const [isChecked, setIsChecked] = useState<boolean>(checked);

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        setIsChecked(!isChecked);
    }

    return (
        <div className="checkbox">
            <label 
                className="checkbox__label"
                htmlFor={id}
            >
                <input 
                    id={id}
                    type="checkbox" 
                    name={id} 
                    className="checkbox__input"
                    checked={isChecked}
                    onChange={changeHandler}
                />
                <span className="checkbox__icon">
                    <FaCheck />
                </span>
                <span
                    className="checkbox__text"
                >
                    {label}
                </span>
            </label>
        </div>
    );
}

export default CheckBox;
