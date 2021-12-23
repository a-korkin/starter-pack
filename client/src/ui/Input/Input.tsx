import React from "react";

import "./Input.scss";

interface IInputProps {
    id: string;
    label: string;
    variant?: string;
}

const Input: React.FC<IInputProps> = ({id, label, variant}) => {
    return (
        <div className="input">
            <input 
                id={id} 
                type="text" 
                name={id} 
                placeholder={label}
                className="input__field"
            />

            <label 
                htmlFor={id}
                className="input__label"
            >
                {label}
            </label>
        </div>
    );
}

export default Input;
