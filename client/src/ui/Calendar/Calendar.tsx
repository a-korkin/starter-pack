import React, { useEffect, useRef, useState } from "react";
import { FaAngleLeft, FaAngleRight } from "react-icons/fa";
import { IDate } from "../../models/base/IDate";
import { IMonth } from "../../models/base/IMonth";

import "./Calendar.scss";

interface ICalendarProps {
    value: Date;
    onChange: (val: Date) => void;
}

const Calendar: React.FC<ICalendarProps> = ({value, onChange}) => {
    const months: IMonth[] = [
        { id: 0, name: "January", days: 31, code: 1 },
        { id: 1, name: "February", days: 28, code: 4 },
        { id: 2, name: "March", days: 31, code: 4 },
        { id: 3, name: "April", days: 30, code: 0 },
        { id: 4, name: "May", days: 31, code: 2 },
        { id: 5, name: "June", days: 30, code: 5 },
        { id: 6, name: "July", days: 31, code: 0 },
        { id: 7, name: "August", days: 31, code: 3 },
        { id: 8, name: "September", days: 30, code: 6 },
        { id: 9, name: "October", days: 31, code: 1 },
        { id: 10, name: "November", days: 30, code: 4 },
        { id: 11, name: "December", days: 31, code: 6 }
    ];

    const createDays = (count: number, empty: boolean = false): number[] => {
        let days: number[] = [];

        for (let i = 1; i <= count; i++) {
            if (!empty)
                days.push(i);
            else 
                days.push(i * -1);
        }
        return days;
    }

    const getDayOfWeek = (day: number, month: number, year: number): number[] => {
        const yearFtwo = parseInt(year.toString().substring(0, 2));
        let century = 6;
        switch (yearFtwo) {
            case 16:
            case 20:
                century = 6;
                break;
            case 17:
            case 21:
                century = 4;
                break;
            case 18:
            case 22:
                century = 2;
                break;
            case 19: 
            case 23:
                century = 0;
                break;    
        }
        const yearLtwo = parseInt(year.toString().slice(-2));
        const yearCode = (century + yearLtwo + parseInt((yearLtwo / 4).toString())) % 7;
        const monthCode = months.find(w => w.id === month)?.code ?? 0;
        let dayOfWeek = (day + monthCode + yearCode) % 7;

        if (dayOfWeek < 2)
            dayOfWeek += 5;
        else 
            dayOfWeek -= 2;

        return createDays(dayOfWeek, true);
    }

    const isLeapYear = (year: number): boolean => new Date(year, 1, 29).getMonth() === 1;

    const getDays = (year: number, month: number): number[] => {
        let countDaysInMonth = months.find(w => w.id === month)?.days ?? 30;
        if (month === 2 && isLeapYear(year)) {
            countDaysInMonth = 29;
        }

        const days: number[] = [
            ...getDayOfWeek(1, month, year), 
            ...createDays(countDaysInMonth)
        ];

        return days;
    }
    
    const convertDate = (date: Date): IDate => {
        const result: IDate = {
            year: date.getFullYear(),
            month: months.find(w => w.id === date.getMonth()),
            day: date.getDate()
        };
        return result;
    }
    
    const [date, setDate] = useState<IDate>(convertDate(value));
    const [days, setDays] = useState<number[]>(getDays(value.getFullYear(), value.getMonth()));
    const [activeMonth, setActiveMonth] = useState<boolean>(false);
    
    const changeYearHandler = (e: React.MouseEvent<HTMLSpanElement>, type: string) => {
        switch (type) {
            case "add":
                setDate(prevState => {
                    setDays(getDays(date.year + 1, date.month?.id ?? 1))
                    return {...prevState, year: date.year + 1};
                })
                break;
            case "remove":
                setDate(prevState => {
                    setDays(getDays(date.year - 1, date.month?.id ?? 1));
                    return {...prevState, year: date.year - 1};
                });
                break;
        }
    }

    const changeMonthHandler = (e: React.MouseEvent<HTMLButtonElement>, key: number) => {
        setDate(prevState => ({
            ...prevState, month: months.find(w => w.id === key)
        }));
        setActiveMonth(false);
        setDays(getDays(date.year, months.find(w => w.id === key)?.id ?? 1));
    }

    const changeDayHandler = (e: React.MouseEvent<HTMLDivElement>, day: number) => {
        setDate(prevState => ({
            ...prevState, day: day
        }));

        const newDate = new Date(date.year, date.month?.id ?? 1, day);
        onChange(newDate);
    }

    const setConvertDate = useRef((value: Date) => (value: React.SetStateAction<IDate>) => {})  ;

    useEffect(() => {
        setConvertDate.current(value);
    }, [setConvertDate, value]);

    return (
        <div className="calendar">
            <div className={activeMonth ? "calendar__months active" : "calendar__months"}>
                {
                    months.map(({id, name}) =>     
                        <button 
                            className="calendar__months-item" 
                            key={id}
                            onClick={e => changeMonthHandler(e, id)}
                        >
                            {name}
                        </button>)
                }
            </div>
            <div className="calendar__year-month">
                <button className="month" onClick={e => setActiveMonth(!activeMonth)}>{date.month?.name}</button>
                <div className="year">
                    <button onClick={e => changeYearHandler(e, "remove")}><FaAngleLeft /></button>
                    <span>{date.year}</span>
                    <button onClick={e => changeYearHandler(e, "add")}><FaAngleRight /></button>
                </div>
            </div>
            <div className="calendar__week">
                <div className="calendar__week-item">Mon</div>
                <div className="calendar__week-item">Tue</div>
                <div className="calendar__week-item">Wed</div>
                <div className="calendar__week-item">Thu</div>
                <div className="calendar__week-item">Fri</div>
                <div className="calendar__week-item">Sat</div>
                <div className="calendar__week-item">Sun</div>
            </div>
            <div className="calendar__days">
                {
                    days.map(day => {
                        let classes = "calendar__days-item";
                        if (day <= 0) 
                            classes += " disable";
                        else if (date.day === day)
                            classes += " active";

                        return (
                            <div 
                                key={day} 
                                className={classes}
                                onClick={e => changeDayHandler(e, day)}
                            >
                                {day <= 0 ? "" : day}
                                {day > 0 &&
                                    <>
                                    <span></span><span></span><span></span><span></span>
                                    </>
                                }
                            </div>
                        );
                    })
                }
            </div>
        </div>
    );
}

export default Calendar;