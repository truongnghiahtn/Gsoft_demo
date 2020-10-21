import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import $ from "jquery";

export default class headernew extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showScroll: false,
      so: 0,
    };
  }

  checkScrollTop = () => {
    if (!this.state.showScroll && window.pageYOffset > 350) {
      this.setState(
        {
          showScroll: true,
        },
        () => {
          this.hidenclass();
        }
      );
    } else if (this.state.showScroll && window.pageYOffset < 350) {
      this.setState(
        {
          showScroll: false,
        },
        () => {
          this.hidenclass();
        }
      );
    }
  };

  componentDidMount() {
    window.addEventListener("scroll", this.checkScrollTop);
  }
  componentWillUnmount() {
    window.removeEventListener("scroll", this.checkScrollTop);
  }

  hidenclass = () => {
    if (this.state.showScroll) {
      $("#headerNew").addClass("showHeaderNew");
      $("#headerNew").removeClass("hiddenHeaderNew");
    } else {
      $("#headerNew").removeClass("showHeaderNew");
      $("#headerNew").addClass("hiddenHeaderNew");
    }
  };

  render() {
    return (
      <div className="headerNew" id="headerNew">
        <NavLink to="/">
          <img
            src="./asset/img/LogoGSOFTNoSlogan.png"
            style={{
              position: "relative",
              top: "10px",
              left: "30px",
              cursor: "pointer",
              width: "120px",
              padding: "10px 0px",
              paddingBottom: "0px",
            }}
          ></img>
        </NavLink>
      </div>
    );
  }
}
