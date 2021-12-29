import { IMonth } from "./IMonth";

export interface IDate {
    year: number;
    month?: IMonth;
    day: number;
}