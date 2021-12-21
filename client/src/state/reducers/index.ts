import { combineReducers } from "redux";
import entityTypesReducer from "./entityTypesReducer";

const reducers = combineReducers({
    entityTypes: entityTypesReducer
});

export default reducers;