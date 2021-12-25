import React, { useRef, useState } from "react";
import { FaAngleDown } from "react-icons/fa";
import { IDictionary } from "../../models/base/IDictionary";

import "./Select.scss";

interface ISelectProps {
    id: string;
    label: string;
    options: IDictionary[];
    variant?: string;
    currentValue?: IDictionary;
    multiple?: boolean;
    onChange: (isMultiple: boolean, value: IDictionary[]) => void;
}

const Select: React.FC<ISelectProps> = ({id, label, options, currentValue, multiple = false, onChange}) => {
    const [term, setTerm] = useState<string>(currentValue !== undefined ? currentValue.value : "");
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

    const optionSelectHandler = (e: React.MouseEvent<HTMLDivElement>, val: IDictionary) => {
        setFocus();
        setTerm(val.value);
        onChange(false, [val]);
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
                    opts.map((o) => 
                        <div 
                            key={o.id}
                            className="select-options__item"
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

export default Select;
