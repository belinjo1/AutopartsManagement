import React from "react";
import { Route, Redirect } from "react-router-dom";

export const ProtectedRoute = ({
    isAuth: isAuth,
    role: role,
    component: Component,
    ...rest
}) => {
    return (
        <Route
            {...rest}
            render={(props) => {
                if (isAuth === true) {
                    return <Component />;
                } else {
                    return (
                        <Redirect
                            to={{ pathname: "/NotFound", state: { from: props.location } }}
                        />
                    );
                }
            }}
        />
    );
};
