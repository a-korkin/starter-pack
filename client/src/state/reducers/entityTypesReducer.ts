import { EntityType } from "../../models/common/EntityType";
import { ActionType } from "../action-types";
import { Action } from "../actions";

interface EntityTypesState {
    isLoading: boolean;
    error: string | null;
    data: EntityType[];
}

const initialState: EntityTypesState = {
    isLoading: false,
    error: null,
    data: []
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