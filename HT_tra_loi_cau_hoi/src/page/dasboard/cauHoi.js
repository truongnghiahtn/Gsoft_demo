import React, { Component } from "react";

export default class componentName extends Component {
  renderhtml = () => {
    return this.props.data.map((item, index) => {
      return <p key={index}> {item.TieuDe}</p>;
    });
  };
  render() {
    return (
      <div className="DS-content col-12 " style={{ margin: "0 auto" }}>
        <div className="content-Parent tp-content">
          <div className="tp-cotent__title"> Câu hỏi</div>
          <div className="content">
            {this.renderhtml()}
            <p className="pt-3 m-0">
              {" "}
              Tổng số câu hỏi: <b>{this.props.data.length}</b>{" "}
            </p>
          </div>
        </div>
      </div>
    );
  }
}
