import React, { Component } from "react";
import $ from "jquery";

export default class headeradmin extends Component {
  toggleClass = () => {
    $("#Main-menu").toggleClass("off-menu");
    $("#content-dashboard").toggleClass("show-content");
    $("#headerLeft").toggleClass("hiddenLogo");
  };

  render() {
    return (
      <div className="NavBar-header" id="Main-menu1">
        <div className="row">
          <div
            className="header-left  col-12 col-sm-4 col-lg-2"
            id="headerLeft"
          >
            {/* <h1 className="title-header">Admin Gsoft</h1> */}
            <a href="/admin">
              <img src="/asset/img/logo_new_1.png" width="130px" />
            </a>

            <i
              style={{ paddingTop: "5px", cursor: "pointer", fontSize: "20px" }}
              className="fa fa-bars"
              aria-hidden="true"
              onClick={this.toggleClass}
            ></i>
          </div>
          <div className="header-right col-12 col-sm-4 col-lg-3 ml-auto ">
            {/* <i className="fa fa-envelope" aria-hidden="true"></i>
            <i className="fa fa-bell" aria-hidden="true"></i>
            <i className="fa fa-flag" aria-hidden="true"></i>
            <i className="fa fa-user-circle-o" aria-hidden="true"></i>
            <i className="fa fa-cogs" aria-hidden="true"></i> */}
          </div>
        </div>
      </div>
    );
  }
}
