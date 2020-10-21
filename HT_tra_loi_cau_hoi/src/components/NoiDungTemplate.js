import React, { Component, Fragment } from "react";
import { NavLink } from "react-router-dom";

export default class noidungtemplate extends Component {
  onnext = (data) => {
    sessionStorage.setItem("ChuDe", JSON.stringify(data));
    // this.props.history.push("user");
  };

  renderhtml = () => {
    return this.props.data.map((item, key) => {
      return (
        <NavLink
          to="user"
          className="col-4"
          onClick={() => {
            this.onnext(item);
          }}
        >
          <div
            dangerouslySetInnerHTML={{
              __html: item.Content,
            }}
          ></div>
        </NavLink>
      );
    });
  };
  render() {
    return <Fragment>{this.renderhtml()}</Fragment>;
  }
}
