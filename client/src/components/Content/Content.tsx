import React from "react";
import { IDictionary } from "../../models/IDictionary";
import Button from "../../ui/Button";
import Input from "../../ui/Input";
import Select from "../../ui/Select";

import "./Content.scss";

const Content: React.FC = () => {
    const selectOptions: IDictionary[] = [
        {id: "1", value: "Москва"},
        {id: "2", value: "Минск"},
        {id: "3", value: "Киев"},
        {id: "4", value: "Нижний Новгород"},
    ];
    

    return (
        <div className="content">
            <Button>Сохранить</Button>
            <Button variant="info">Расчёт</Button>
            <Button variant="danger" disable>Удалить</Button>
            <Button variant="warning">Внимание</Button>
            <Button variant="success">Успех</Button>
            <br /><br />
            <Input id="d91ccf5f-9772-44a6-af72-e814af537b04" label="Фамилия" />
            <Select 
                id="45ddc1c0-fbee-481c-b9c0-c3f4da477d9c" 
                label="Город"
                options={selectOptions}
            />
        </div>
    );
}

export default Content;
