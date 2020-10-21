import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import * as action from "../redux/action/index";
import { connect } from "react-redux";

class newsidebar extends Component {
  // constructor(props) {
  //   super(props);
  //   this.state = {
  //     action: "1",
  //   };
  // }
  ActionTab = (data) => {
    if (data == "2") {
      this.props.getTemplate();
    }
    this.props.getTab(data);
  };
  thongke = () => {
    this.props.getTab("3");
    this.props.getsubmit("");
    const data = {
      IDChuDe: "all",
      IDTemplate: "all",
    };

    this.props.getselect(data);
  };

  render() {
    return (
      <nav className="main-menu " id="Main-menu">
        <ul className="listMennu">
          <li
            className={
              this.props.tab === "1" ? "has-subnav active" : "has-subnav"
            }
            onClick={() => {
              this.ActionTab("1");
            }}
          >
            <NavLink to="/admin">
              <i className="fa fa-home fa-2x" />
              <span style={{ width: "76%" }} className="nav-text">
                Chủ đề
              </span>
            </NavLink>
          </li>
          <li
            className={
              this.props.tab === "2" ? "has-subnav active" : "has-subnav"
            }
            onClick={() => {
              this.ActionTab("2");
            }}
          >
            <NavLink to="/admin/template">
              <i className="fa fa-laptop fa-2x" />
              <span className="nav-text">Template</span>
            </NavLink>
          </li>
          <li
            className={
              this.props.tab === "3" ? "has-subnav active" : "has-subnav"
            }
            onClick={this.thongke}
          >
            <NavLink to="/admin/thongke">
              <i className="fa fa-list fa-2x" />
              <span className="nav-text">Thống kê</span>
            </NavLink>
          </li>
        </ul>
      </nav>
    );
  }
}
const mapStateToProps = (state) => {
  return {
    tab: state.deMoReducer.tab,
  };
};
const mapDispatchToProps = (dispatch) => {
  return {
    getTemplate: () => {
      dispatch(action.actGetListTemplate());
    },
    getTab: (data) => {
      dispatch(action.getTab(data));
    },
    getselect: (data) => {
      dispatch(action.getSelect(data));
    },
    getsubmit: (data) => {
      dispatch(action.getSubmit(data));
    },
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(newsidebar);
