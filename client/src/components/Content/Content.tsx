import React from "react";
import Button from "../../ui/Button";
import Input from "../../ui/Input";
import Select from "../../ui/Select";

import "./Content.scss";

const Content: React.FC = () => {
    const selectOptions = new Map<string, string>([
        ["1", "Москва"],
        ["2", "Минск"],
        ["3", "Киев"],
        ["4", "Нижний Новгород"]
    ]);
    

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
