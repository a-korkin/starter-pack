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
            <label 
                htmlFor={id}
                className="input__label"
            >
                {label}
            </label>
            
            <input 
                id={id} 
                type="text" 
                name={id} 
                placeholder={label}
                className="input__field"
            />

        </div>
    );
}

export default Input;
