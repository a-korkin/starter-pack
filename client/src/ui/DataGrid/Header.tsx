import React from "react";
import { ResizableBox } from "react-resizable";
import { ICell } from "../../models/base/ICell";

interface IHeaderProps {
    cell: ICell;
    width: number;
    height: number;
}

const Header: React.FC<IHeaderProps> = ({cell,width,height}) => {
    const dragStartHanler = (e: React.DragEvent<HTMLDivElement>, cell: ICell) => {
        // console.log(cell.value);
    }

    const dropHandler = (e: React.DragEvent<HTMLDivElement>, cell: ICell) => {
        e.preventDefault();
    }

    return (
        <ResizableBox 
            width={width} 
            height={height} 
            axis="x"
            minConstraints={[width, height]}
        >
            <div 
                className="grid-cell"
                draggable
                onDragStart={e => dragStartHanler(e, cell)}
                onDrop={e => dropHandler(e, cell)}
            >
                {cell.value}
            </div>
        </ResizableBox>
    );
}

export default Header;
