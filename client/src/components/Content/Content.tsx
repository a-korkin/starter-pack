import React from "react";
import Button from "../../ui/Button";
import Input from "../../ui/Input";

import "./Content.scss";

const Content: React.FC = () => {
    return (
        <div className="content">
            <Button>Сохранить</Button>
            <Button variant="info">Расчёт</Button>
            <Button variant="danger" disable>Удалить</Button>
            <Button variant="warning">Внимание</Button>
            <Button variant="success">Успех</Button>
            <br />
            <br />
            <Input id="d91ccf5f-9772-44a6-af72-e814af537b04" label="Фамилия" />
        </div>
    );
}

export default Content;
