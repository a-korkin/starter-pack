import React, { useState } from "react";
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
    const [opts, setOpts] = useState<IDictionary[]>(options);
    
    return (
        <div className="select">
            <div className="select__group">
                <div className="select__group-options">
                    <div className="selected-option">Чикаго</div>
                    <div className="selected-option">Рим</div>
                </div>
                <input 
                    id={id}
                    type="text" 
                    name={id}  
                    placeholder={label}
                    className="select__group-field"
                />
            </div>

            <div className="select__options-list">
                {
                    opts.map((o) => 
                        <div
                            key={o.id}
                            className="select__options-list-item"
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
