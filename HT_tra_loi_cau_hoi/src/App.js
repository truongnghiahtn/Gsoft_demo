import React from "react";
import "./App.css";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import HomeTemplate from "./templates/HomeTemplate";
import AdminTemplate from "./templates/AdminTemplate";
import PageNotFound from "./page/pageNotFound";
import { routesHome } from "./routes";
import { routesAdmin } from "./routes";

const showMenusHome = (routes) => {
  if (routes && routes.length > 0) {
    return routes.map((item, index) => (
      <HomeTemplate
        path={item.path}
        exact={item.exact}
        Component={item.component}
        key={index}
      />
    ));
  }
};
const showMenusAdmin = (routes) => {
  if (routes && routes.length > 0) {
    return routes.map((item, index) => (
      <AdminTemplate
        path={item.path}
        exact={item.exact}
        Component={item.component}
        key={index}
      />
    ));
  }
};

function App() {
  return (
    <BrowserRouter>
      <div>
        <Switch>
          {showMenusAdmin(routesAdmin)}
          {showMenusHome(routesHome)}
          <Route path="" exact={false} component={PageNotFound} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
