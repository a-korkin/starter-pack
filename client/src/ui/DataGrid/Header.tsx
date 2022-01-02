import React from "react";
import { ResizableBox } from "react-resizable";
import { ICell } from "../../models/base/ICell";

interface IHeaderProps {
    cell: ICell;
    width: number;
    height: number;
}

const Header: React.FC<IHeaderProps> = ({cell,width,height}) => {
    return (
        <ResizableBox 
            width={width} 
            height={height} 
            axis="x"
            minConstraints={[width, height]}
        >
            <div 
                className="grid-cell">
                {cell.value}
            </div>
        </ResizableBox>
    );
}

export default Header;
