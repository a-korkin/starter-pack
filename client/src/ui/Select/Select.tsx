import React, { useState } from "react";
import { FaAngleDown } from "react-icons/fa";

import "./Select.scss";

interface ISelectProps {
    id: string;
    label: string;
    variant?: string;
    options: Map<string, string>;
}

const Select: React.FC<ISelectProps> = ({id, label, variant, options}) => {
    const [value, setValue] = useState<string>("");
    const [visible, setVisible] = useState<boolean>(false);

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        console.log(e.currentTarget.value);
    }

    const clickArrowHandler = (e: React.MouseEvent<HTMLSpanElement>) => {
        setVisible(!visible);
    }

    return (
        <div className="input input--select">
            <span 
                className={visible ? "input--select__icon visible" : "input--select__icon"}
                onClick={e => clickArrowHandler(e)}>
                <FaAngleDown />
            </span>

            <input 
                id={id} 
                type="text" 
                name={id} 
                placeholder={label}
                className="input__field"
                value={value}
                onChange={e => changeHandler(e)}
            />

            <label 
                htmlFor={id}
                className="input__label"
            >
                {label}
            </label>

            <div className={visible ? "select-options" : "select-options hide"}>
                {
                    Array.from(options).map(([key, value]) => 
                        <div 
                            key={key}
                            className="select-options__item"
                        >
                            {value}
                        </div>
                    )
                }
            </div>
        </div>
    );
}

export default Select;
