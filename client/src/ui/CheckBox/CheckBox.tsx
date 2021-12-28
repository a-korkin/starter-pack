import React, { useState } from "react";
import { FaCheck } from "react-icons/fa";

import "./CheckBox.scss";

interface ICheckBoxProps {
    id: string;
    checked: boolean;
    variant: string;
}

const CheckBox: React.FC<ICheckBoxProps> = ({id, checked, variant, children}) => {
    const [isChecked, setIsChecked] = useState<boolean>(checked);
    const prefix: string = variant === "switch" ? "checkbox--switch" : "checkbox";
    
    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        setIsChecked(!isChecked);
    }

    return (
        <div className={prefix}>
            <label 
                className={`${prefix}__label`}
                htmlFor={id}
            >
                <input 
                    id={id}
                    type="checkbox" 
                    name={id} 
                    className={`${prefix}__input`}
                    checked={isChecked}
                    onChange={changeHandler}
                />
                {variant === "check" &&
                    <span className={`${prefix}__icon`}>
                        <FaCheck />
                    </span>
                }
                {variant === "switch" &&
                    <span className={`${prefix}__toggler`}>
                    </span>
                }
                <span
                    className={`${prefix}__text`}
                >
                    {children}
                </span>
            </label>
        </div>
    );
}

export default CheckBox;
