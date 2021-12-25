import React, { useRef, useState } from "react";
import { FaAngleDown } from "react-icons/fa";
import { IDictionary } from "../../models/base/IDictionary";

import "./DropDown.scss";

interface IDropDownProps {
    id: string;
    label: string;
    options: IDictionary[];
    variant?: string;
    currentValue?: IDictionary;
    multiple?: boolean;
    onChange: (isMultiple: boolean, value: IDictionary[]) => void;
}

const DropDown: React.FC<IDropDownProps> = ({id, label, options, currentValue, multiple = false, onChange}) => {
    const [visibleOptions, setVisibleOptions] = useState<IDictionary[]>(options);
    const [selectedOptions, setSelectedOptions] = useState<IDictionary[]>([]);
    const [visible, setVisible] = useState<boolean>(false);
    const searchInput = useRef<HTMLInputElement>(null);

    const setFocus = () => {
        setVisible(!visible);
        if (!visible)
            searchInput.current?.focus();
        else 
            searchInput.current?.blur();
    }

    const optionSelectHandler = (e: React.MouseEvent<HTMLDivElement>, option: IDictionary) => {
        setFocus();
        setSelectedOptions(prev => {
            return [...prev, option];
        });

        setVisibleOptions(prev => {
            return [...prev.slice(0, prev.findIndex(a => a.id === option.id)),
                    ...prev.slice(prev.findIndex(a => a.id === option.id) + 1)]
        });
    }

    return (
        <div className="select">
            <label 
                htmlFor={id}
                className="input__label"
            >
                {label}
            </label>

            <div className="select__group">
                <span 
                    className={visible ? "select__group__icon visible" : "select__group__icon"}
                    onClick={setFocus}
                >
                    <FaAngleDown />
                </span>

                <div className="select__group-options">
                    {
                        selectedOptions.map((option) => 
                            <div key={option.id} className="selected-option">{option.value}</div>
                        )
                    }
                </div>

                <input 
                    id={id}
                    type="text" 
                    name={id}  
                    placeholder={label}
                    ref={searchInput}
                    className="select__group-field"
                    onClick={setFocus}
                />

            </div>
            
            <div className={visible ? "select__options-list" : "select__options-list hide"}>
                {
                    visibleOptions.map((o) => 
                        <div
                            key={o.id}
                            className="select__options-list-item"
                            onClick={e => optionSelectHandler(e, o)}
                        >
                            {o.value}
                        </div>
                    )
                }
            </div>
        </div>
    );
}

export default DropDown;
