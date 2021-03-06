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
    const [term, setTerm] = useState<string>("");
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
        
        if (multiple) {
            setSelectedOptions(prev => {
                return [...prev, option];
            });
    
            setVisibleOptions(prev => {
                return [...prev.slice(0, prev.findIndex(a => a.id === option.id)),
                        ...prev.slice(prev.findIndex(a => a.id === option.id) + 1)];
            });

            onChange(multiple, [...selectedOptions, option]);
        } else {
            setSelectedOptions([option]);
            setTerm(option.value);
            onChange(multiple, [option]);
        }
    }

    const removeOptionHandler = (e: React.MouseEvent<HTMLSpanElement>, option: IDictionary) => {
        setVisibleOptions(prev => {
            return [...prev, option];
        });

        setSelectedOptions(prev => {
            return [...prev.slice(0, prev.findIndex(a => a.id === option.id)),
                    ...prev.slice(prev.findIndex(a => a.id === option.id) + 1)];
        });
    }

    const serchHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        const _term = e.currentTarget.value;
        setTerm(_term);
        const findedOptions = options.filter(({value}) => value.toLowerCase().includes(_term.toLowerCase()));
        setVisibleOptions(findedOptions);
        setVisible(findedOptions.length > 0);
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

                {multiple &&
                    <div className="select__group-options">
                        {
                            selectedOptions.map((option) => 
                                <div 
                                    key={option.id} 
                                    className="selected-option"
                                >
                                    {option.value}
                                    <span
                                        className="selected-option__remove"
                                        onClick={e => removeOptionHandler(e, option)}
                                    >
                                        &times;
                                    </span>
                                </div>
                            )
                        }
                    </div>
                }

                <input 
                    id={id}
                    type="text" 
                    name={id}  
                    placeholder={label}
                    ref={searchInput}
                    className={multiple ? "select__group-field" : "select__group-field single"}
                    onClick={setFocus}
                    onChange={serchHandler}
                    value={term}
                />

            </div>
            
            <div className={visible ? "select__options-list" : "select__options-list hide"}>
                {
                    visibleOptions.map((option) => 
                        <div
                            key={option.id}
                            className="select__options-list-item"
                            onClick={e => optionSelectHandler(e, option)}
                        >
                            {option.value}
                        </div>
                    )
                }
            </div>
        </div>
    );
}

export default DropDown;
