import { Dispatch } from "redux";
import $api from "../../http";
import { EntityTypeModel } from "../../models/common/EntityTypeModel";
import { ActionType } from "../action-types";
import { Action } from "../actions";

export const fetchEntityTypes = () => async (dispatch: Dispatch<Action>) => {
    dispatch({type: ActionType.FETCH_ENTITY_TYPES});

    try {
        const { data } = await $api.get<EntityTypeModel[]>("/common/entity-types");
        dispatch({type: ActionType.FETCH_ENTITY_TYPES_SUCCESS, payload: data});
    } catch (error: any) {
        dispatch({type: ActionType.FETCH_ENTITY_TYPES_ERROR, payload: error});
    }
}