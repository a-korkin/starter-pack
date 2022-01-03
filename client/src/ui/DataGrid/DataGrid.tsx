import React from "react";
import { ICell } from "../../models/base/ICell";

import "./DataGrid.scss";
import Header from "./Header";

const DataGrid: React.FC = () => {
    const headers: ICell[] = [
        {id: "1", row: 1, column: 1, value: "Email"},
        {id: "2", row: 1, column: 2, value: "Phone"},
        {id: "3", row: 1, column: 3, value: "LastName"},
        {id: "4", row: 1, column: 4, value: "FirstName"},
        {id: "5", row: 1, column: 5, value: "BirthDate"},
    ];

    const data: ICell[] = [
        {id: "1", row: 2, column: 1, value: "mail1.cr"},
        {id: "2", row: 2, column: 2, value: "54-451-41"},
        {id: "3", row: 2, column: 3, value: "firstname1"},
        {id: "4", row: 2, column: 4, value: "lastname1"},
        {id: "5", row: 2, column: 5, value: "01.11.1989"},

        {id: "1", row: 3, column: 1, value: "mail2.cr"},
        {id: "2", row: 3, column: 2, value: "54-451-42"},
        {id: "3", row: 3, column: 3, value: "firstname2"},
        {id: "4", row: 3, column: 4, value: "lastname2"},
        {id: "5", row: 3, column: 5, value: "02.11.1989"},

        {id: "1", row: 4, column: 1, value: "mail3.cr"},
        {id: "2", row: 4, column: 2, value: "54-451-43"},
        {id: "3", row: 4, column: 3, value: "firstname3"},
        {id: "4", row: 4, column: 4, value: "lastname3"},
        {id: "5", row: 4, column: 5, value: "03.11.1989"},

        {id: "1", row: 5, column: 1, value: "mail4.cr"},
        {id: "2", row: 5, column: 2, value: "54-451-44"},
        {id: "3", row: 5, column: 3, value: "firstname4"},
        {id: "4", row: 5, column: 4, value: "lastname4"},
        {id: "5", row: 5, column: 5, value: "04.11.1989"},
    ];

    const dropHeaderHandler = (e: React.DragEvent<HTMLDivElement>) => {
        e.preventDefault();
        console.log(e.currentTarget);
    }

    const dragHandler = (e: React.DragEvent<HTMLDivElement>) => {
        e.preventDefault();
    }

    return (

        <div className="grid-wrapper">

            <div className="grid grid--5">

                <div 
                    className="group-panel" 
                    onDrop={e => dropHeaderHandler(e)}
                    onDragStart={e => dragHandler(e)}
                    onDragEnter={e => dragHandler(e)}
                    onDragLeave={e => dragHandler(e)}
                    onDragEnd={e => dragHandler(e)}
                    onDragOver={e => dragHandler(e)}
                ></div>

                <div className="grid__header">
                    {
                        headers.map((cell) => 
                            <Header key={cell.id} cell={cell} width={100} height={30} />
                        )
                    }
                </div>

                <div className="grid__data">
                    {
                        data.map((cell) =>
                            <div 
                                key={cell.id}
                                className="grid-cell">
                                {cell.value}
                            </div>
                        )
                    }
                </div>
            </div>

        </div>
    );
}

export default DataGrid;
