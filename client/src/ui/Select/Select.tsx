import React, { useRef, useState } from "react";
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
    const searchInput = useRef<HTMLInputElement>(null);

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        setValue(e.currentTarget.value);
    }

    const setFocus = () => {
        setVisible(!visible);
        if (!visible)
            searchInput.current?.focus();
        else 
            searchInput.current?.blur();
    }

    const optionSelectHandler = (e: React.MouseEvent<HTMLDivElement>, key: string, value: string) => {
        setFocus();
        setValue(value);
    }

    return (
        <div className="input input--select">
            <span 
                className={visible ? "input--select__icon visible" : "input--select__icon"}
                onClick={setFocus}
            >
                <FaAngleDown />
            </span>

            <input 
                id={id} 
                type="text" 
                name={id} 
                placeholder={label}
                className="input__field"
                value={value}
                ref={searchInput}
                onChange={e => changeHandler(e)}
                onClick={setFocus}
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
                            onClick={e => optionSelectHandler(e, key, value)}
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
