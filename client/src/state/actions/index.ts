import { EntityTypeModel } from "../../models/common/EntityTypeModel";
import { PaginatedList } from "../../models/base/PaginatedList";
import { ActionType } from "../action-types";

interface FetchEntityTypesAction {
    type: ActionType.FETCH_ENTITY_TYPES;
}

interface FetchEntityTypesSuccessAction {
    type: ActionType.FETCH_ENTITY_TYPES_SUCCESS;
    payload: PaginatedList<EntityTypeModel>;
}

interface FetchEntityTypesErrorAction {
    type: ActionType.FETCH_ENTITY_TYPES_ERROR;
    payload: string;
}

export type Action = 
    FetchEntityTypesAction | 
    FetchEntityTypesSuccessAction | 
    FetchEntityTypesErrorAction;