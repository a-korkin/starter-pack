import { applyMiddleware, createStore } from "redux";
import thunk from "redux-thunk";
import reducers from "./reducers";

export const state = createStore(reducers, applyMiddleware(thunk));
export type RootState = ReturnType<typeof state.getState>;