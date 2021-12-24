import React, { useRef, useState } from "react";
import { FaAngleDown } from "react-icons/fa";
import { IDictionary } from "../../models/IDictionary";

import "./Select.scss";

interface ISelectProps {
    id: string;
    label: string;
    variant?: string;
    options: IDictionary[];
}

const Select: React.FC<ISelectProps> = ({id, label, variant, options}) => {
    const [term, setTerm] = useState<string>("");
    const [visible, setVisible] = useState<boolean>(false);
    const [opts, setOpts] = useState<IDictionary[]>(options);
    
    const searchInput = useRef<HTMLInputElement>(null);

    const changeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        const _term = e.currentTarget.value;
        setTerm(_term);
        const findedOptions = options.filter(({value}) => value.toLowerCase().includes(_term.toLowerCase()));
        setOpts(findedOptions);
        setVisible(findedOptions.length > 0);
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
        setTerm(value);
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
                autoComplete="off"
                className="input__field"
                value={term}
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
                    opts.map(({id, value}) => 
                        <div 
                            key={id}
                            className="select-options__item"
                            onClick={e => optionSelectHandler(e, id, value)}
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
