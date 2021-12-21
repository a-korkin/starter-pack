import { EntityType } from "../../models/common/EntityType";
import { ActionType } from "../action-types";

interface FetchEntityTypesAction {
    type: ActionType.FETCH_ENTITY_TYPES;
}

interface FetchEntityTypesSuccessAction {
    type: ActionType.FETCH_ENTITY_TYPES_SUCCESS;
    payload: EntityType[];
}

interface FetchEntityTypesErrorAction {
    type: ActionType.FETCH_ENTITY_TYPES_ERROR;
    payload: string;
}

export type Action = 
    FetchEntityTypesAction | 
    FetchEntityTypesSuccessAction | 
    FetchEntityTypesErrorAction;