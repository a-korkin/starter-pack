// import { Type } from "typescript";
import { EntityTypeModel } from "./common/EntityTypeModel";

export interface PaginatedList {
    items: EntityTypeModel[]; // TODO сделать generic
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}