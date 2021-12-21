import React from "react";
import ReactDOM from "react-dom";
import App from "./components/App";
import { Provider } from "react-redux";
import { state } from "./state";

ReactDOM.render(
    <Provider store={state}>
        <App />
    </Provider>,
    document.getElementById("root")
);
