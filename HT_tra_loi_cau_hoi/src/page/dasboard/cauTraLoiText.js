import React, { Component } from "react";

export default class cautraloitext extends Component {
  constructor(props) {
    super(props);
    this.state = {
      show: false,
      mang: [],
    };
  }
  componentWillReceiveProps(nextProps) {
    if (nextProps.data) {
      this.phanmang();
    }
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
  };
  phanmang = () => {
    let { mang } = this.state;
    if (!this.state.show) {
      mang = this.props.data.NoiDungCauTraLoi.filter((item, index) => {
        return index < 6;
      });
    } else {
      mang = this.props.data.NoiDungCauTraLoi;
    }
    this.setState({
      mang,
    });
  };

  renderhtml = () => {
    return this.state.mang.map((item, index) => {
      return <p key={index}>{item}</p>;
    });
  };

  render() {
    return (
      <div className="DS-content col-12 " style={{ margin: "0 auto" }}>
        <div className="content-Parent tp-content">
          <div className="tp-cotent__title">{this.props.data.TenCauHoi} </div>
          <div className="content">
            {this.renderhtml()}
            {this.props.data.NoiDungCauTraLoi.length > 7 ? (
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
              Tổng số câu trả lời : {this.props.data.SoLuong}{" "}
            </p>
          </div>
        </div>
      </div>
    );
  }
}
