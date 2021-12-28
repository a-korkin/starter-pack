import React from "react";
import { IDictionary } from "../../models/base/IDictionary";
import Button from "../../ui/Button";
import CheckBox from "../../ui/CheckBox";
import DropDown from "../../ui/DropDown";
import Input from "../../ui/Input";

import "./Content.scss";

const Content: React.FC = () => {
    const cities: IDictionary[] = [
        {id: "1", value: "Москва"},
        {id: "2", value: "Минск"},
        {id: "3", value: "Киев"},
        {id: "4", value: "Нижний Новгород"},
        {id: "5", value: "Казань"},
        {id: "6", value: "Гавана"},
        {id: "7", value: "Сан Франциско"},
        {id: "8", value: "Лондон"},
        {id: "9", value: "Берлин"},
    ];

    const countries: IDictionary[] = [
        {id: "1", value: "Россия"},
        {id: "2", value: "Белорусь"},
        {id: "3", value: "Украина"},
        {id: "4", value: "Германия"},
        {id: "5", value: "Великобритания"},
        {id: "6", value: "США"},
        {id: "7", value: "Куба"},
        {id: "8", value: "Монголия"},
        {id: "9", value: "Китай"},
        {id: "10", value: "Нидерладны"},
        {id: "11", value: "Сербия"},
    ];
    
    const changeSelectOptionHandler = (isMultiple: boolean, options: IDictionary[]) => {
        console.log(options);
    }

    return (
        <div className="content">
            <Button>Сохранить</Button>
            <Button variant="info">Расчёт</Button>
            <Button variant="danger" disable>Удалить</Button>
            <Button variant="warning">Внимание</Button>
            <Button variant="success">Успех</Button>
            <br /><br />
            <Input id="d91ccf5f-9772-44a6-af72-e814af537b04" label="Фамилия" />
            <DropDown
                id="d91ccf5f-9772-44a6-af72-e814af537b04"
                label="Город"
                options={cities}
                multiple={false}
                onChange={changeSelectOptionHandler}
            />
            <br /><br />
            <DropDown
                id="afcf8e27-db65-4c16-8153-f67229fbe579"
                label="Страна"
                options={countries}
                multiple={true}
                onChange={changeSelectOptionHandler}
            />
            <br /><br />
            <CheckBox 
                id="18e583a7-d942-4a6d-a122-d059ce61164a"
                checked={true}
                variant="check"
            >
                Выплачено
            </CheckBox>
            <br /><br />
            <CheckBox 
                id="368d5010-100c-411b-9a65-a11253bf5677"
                checked={true}
                variant="switch"
            >
                Пенсионер
            </CheckBox>
        </div>
    );
}

export default Content;
