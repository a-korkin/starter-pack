import React from "react";

import "./Input.scss";

interface IInputProps {
    id: string;
    variant?: string;
    label: string;
}

const Input: React.FC<IInputProps> = ({id, variant, label}) => {
    return (
        <div className="input-group">
            <input 
                id={id} 
                type="text" 
                name={id} 
                placeholder={label}
                className="input-group__field"
            />
            
            <label 
                htmlFor={id}
                className="input-group__label"
            >
                {label}
            </label>
        </div>
    );
}

export default Input;
