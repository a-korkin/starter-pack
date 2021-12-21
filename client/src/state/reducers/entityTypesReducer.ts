// import { EntityTypeModel } from "../../models/common/EntityTypeModel";
import { PaginatedList } from "../../models/PaginatedList";
import { ActionType } from "../action-types";
import { Action } from "../actions";

interface EntityTypesState {
    isLoading: boolean;
    error: string | null;
    data: PaginatedList;
}

const initialState: EntityTypesState = {
    isLoading: false,
    error: null,
    data: {} as PaginatedList
}

const entityTypesReducer = (state: EntityTypesState = initialState, action: Action): EntityTypesState => {
    switch (action.type) {
        case ActionType.FETCH_ENTITY_TYPES:
            return { ...state, isLoading: true, error: null };
        case ActionType.FETCH_ENTITY_TYPES_SUCCESS:
            return { ...state, isLoading: false, data: action.payload };
        case ActionType.FETCH_ENTITY_TYPES_ERROR:
            return { ...state, isLoading: false, error: action.payload };
        default:
            return state;
    }
}

export default entityTypesReducer;