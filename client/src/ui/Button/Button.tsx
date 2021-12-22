import React from "react";

import "./Button.scss";

interface IButtonProps {
    variant?: string;
}

const Button: React.FC<IButtonProps> = ({variant, children}) => {
    if (variant === undefined)
        variant = "default";

    const classes = `btn btn-${variant}`;

    return (
        <button className={classes}>
            {children}
        </button>
    );
}

export default Button;
