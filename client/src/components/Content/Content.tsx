import React from "react";
import Button from "../../ui/Button";

import "./Content.scss";

const Content: React.FC = () => {
    return (
        <div className="content">
            <Button>Сохранить</Button>
            <Button variant="info">Расчёт</Button>
            <Button variant="danger">Удалить</Button>
            <Button variant="warning">Внимание</Button>
            <Button variant="success">Успех</Button>
        </div>
    );
}

export default Content;
