import React from "react";
import { IDictionary } from "../../models/base/IDictionary";
import Button from "../../ui/Button";
import DropDown from "../../ui/DropDown";
import Input from "../../ui/Input";
import Select from "../../ui/Select";

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
    
    const changeSelectOptionHandler = (isMultiple: boolean = false, option: IDictionary[]) => {
        if (!isMultiple)
            console.log(option[0].value);
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
            <Select 
                id="45ddc1c0-fbee-481c-b9c0-c3f4da477d9c" 
                label="Город"
                currentValue={cities[2]}
                options={cities}
                onChange={changeSelectOptionHandler}
            />
            {/* <Select
                id="762f9c76-ee26-4199-9e1a-f014b4156b07"
                label="Страна"
                options={countries}
                multiple={true}
                onChange={changeSelectOptionHandler}
            /> */}
            <br /><br />
            <DropDown
                id="afcf8e27-db65-4c16-8153-f67229fbe579"
                label="Страна"
                options={countries}
                multiple={true}
                onChange={changeSelectOptionHandler}
            />
        </div>
    );
}

export default Content;
