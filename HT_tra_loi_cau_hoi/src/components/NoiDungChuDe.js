import React, { Component, Fragment } from "react";

export default class noidungchude extends Component {
  renderhtml = () => {
    return this.props.data.map((item, key) => {
      return (
        <div
          className="card col-4"
          style={{
            textAlign: "center",
          }}
        >
          <div
            className="content-card"
            style={{
              border: "1px solid black",
              cursor: "pointer",
              borderRadius: "5px",
            }}
            onClick={() => {
              this.props.getdata(item);
            }}
          >
            <div className="card-body">
              <h4 className="card-title">{item.TenChuDe}</h4>
              <p className="card-text">{item.MoTa}</p>
            </div>
          </div>
        </div>
      );
    });
  };
  render() {
    return <Fragment>{this.renderhtml()}</Fragment>;
  }
}
