import React, { Component } from "react";

export default class cautraloi extends Component {
  constructor(props) {
    super(props);
    this.state = {
      show: false,
      mang: [],
    };
  }
  componentWillReceiveProps(nextProps) {
    this.phanmang();
  }

  xemthem = () => {
    this.setState(
      {
        show: !this.state.show,
      },
      () => {
        this.phanmang();
      }
    );
    // window.scrollTo({
    //   top: document.getElementById(this.props.title).offsetTop,
    //   behavior: "smooth",
    // });
    // console.log(document.getElementById("Hoten").offsetTop);
  };
  phanmang = () => {
    let { mang } = this.state;
    if (!this.state.show) {
      mang = this.props.data.filter((item, index) => {
        return index < 7;
      });
    } else {
      mang = this.props.data;
    }
    this.setState({
      mang,
    });
  };
  renderhtml = () => {
    switch (this.props.title) {
      case "Hoten":
        return this.state.mang.map((item, index) => {
          return this.state.mang.length === index + 1 ? (
            <p id={this.props.title} key={index}>
              {" "}
              {item.Hoten}
            </p>
          ) : (
            <p key={index}> {item.Hoten}</p>
          );
        });
      case "MSNV":
        return this.state.mang.map((item, index) => {
          return <p key={index}> {item.MSNV}</p>;
        });
      case "Email":
        return this.state.mang.map((item, index) => {
          return <p key={index}> {item.Email}</p>;
        });
      default:
        break;
    }
  };
  render() {
    return (
      <div className="DS-content  col-12" style={{ margin: "0 auto" }}>
        <div className="content-Parent tp-content">
          <div id="nghia" className="tp-cotent__title">
            {" "}
            {this.props.title}
          </div>
          <div className="content">
            {this.renderhtml()}
            {this.props.data.length > 7 ? (
              this.state.show === false ? (
                <p className="btn-xemthem" onClick={this.xemthem}>
                  Xem thêm...
                </p>
              ) : (
                <p className="btn-xemthem" onClick={this.xemthem}>
                  Ẩn dữ liệu
                </p>
              )
            ) : (
              ""
            )}

            <p className="pt-3 m-0">
              {" "}
              Tổng số {this.props.title}: <b>{this.props.data.length}</b>{" "}
            </p>
          </div>
        </div>
      </div>
    );
  }
}
