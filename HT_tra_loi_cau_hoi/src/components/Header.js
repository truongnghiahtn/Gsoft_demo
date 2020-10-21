import React, { Component } from "react";

export default class header extends Component {
  render() {
    return (
      <div className="Header ">
        <div className="container">
          <div className="row">
            <div className="logo-header col-1">
              <img src="./asset/img/logoyte.png" width={100} />
            </div>
            <div className="title-header col-3">
              <h3 style={{ color: "white" }}>Bộ y tế</h3>
              <h1 style={{ color: "white" }}>CỤC Y TẾ DỰ PHÒNG</h1>
              <h4 style={{ color: "white" }}>Hệ thống quản lý khai báo y tế</h4>
            </div>
            <div className="img-header col-8 ">
              <div className="row">
                <div className="img-header-left"></div>
                <img src="./asset/img/yte2.png" height={120} />
                <div className="img-header-right"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
