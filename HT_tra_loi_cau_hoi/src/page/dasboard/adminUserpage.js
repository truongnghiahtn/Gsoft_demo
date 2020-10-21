import React, { Component, Fragment } from "react";
import Axios from "axios";

import UserComponent from "../../components/UserComponent";
import Allquestion from "../../components/allquestion";
import { map } from "jquery";
import Loading from "./../../components/loading";
export default class adminUserpage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Cauhoi: [],
      soLoaiCauHoi: [1],
      indexPage: 1,
      values: {
        HoTen: "",
        MSNV: "",
        Email: "",
        IDChuDe: "",
        IDTemplate: "",
        CauTraLoi_ChiTiet: [],
      },
      endpage: null,
      socauhoi: 5,
      isloading: false,
    };
  }
  componentDidMount() {
    this.getCauHoi();
  }
  getCauHoi = () => {
    if (sessionStorage.getItem("ChuDe")) {
      var temPlateLocal = JSON.parse(sessionStorage.getItem("ChuDe"));
      if (temPlateLocal.SoLgHienThi != null) {
        this.setState({
          socauhoi: temPlateLocal.SoLgHienThi,
        });
      }
      Axios({
        method: "GET",
        url: `http://localhost:50663/api/CauHoi/${temPlateLocal.IDTemplate}`,
      })
        .then((result) => {
          this.setState(
            {
              Cauhoi: result.data,
            },
            () => {
              this.checkpage();
            }
          );
        })
        .catch((err) => {
          console.log(err);
        });
    }
  };
  checkpage = () => {
    let page = parseInt(this.state.Cauhoi.length / this.state.socauhoi);
    let page2 = this.state.Cauhoi.length / this.state.socauhoi;
    if (page != page2) {
      page += 1;
    }
    if (this.state.Cauhoi.length === 0) {
      this.setState({
        endpage: 1,
      });
    } else {
      this.setState({
        endpage: page + 1,
      });
    }
  };

  renderHtmlUser = () => {
    return (
      <div className={this.state.indexPage === 1 ? "vi" : "d-none"}>
        <UserComponent
          next={this.nextpage}
          submitUser={this.dataUser}
          endpage={this.state.endpage}
        />
      </div>
    );
  };

  dataUser = (data) => {
    var hoten, msnv, email;
    if (sessionStorage.getItem("ChuDe")) {
      var temPlateLocal = JSON.parse(sessionStorage.getItem("ChuDe"));
      data.map((item) => {
        if (item.name === "HoTen") {
          hoten = item.CauTraLoi;
        } else {
          if (item.name === "MSNV") {
            msnv = item.CauTraLoi;
          } else {
            email = item.CauTraLoi;
          }
        }
      });
      this.setState(
        {
          values: {
            ...this.state.values,
            HoTen: hoten,
            MSNV: msnv,
            Email: email,
            IDTemplate: temPlateLocal.IDTemplate,
            IDChuDe: temPlateLocal.IDChuDe,
          },
        },
        () => {
          if (this.state.endpage == 1) {
            Axios({
              method: "POST",
              url: "http://localhost:50663/api/ApiCauTraLoi",
              data: this.state.values,
            })
              .then((result) => {
                sessionStorage.setItem("ketqua", JSON.stringify(result.data));
                if (temPlateLocal.TinhDiem === true) {
                  this.props.history.push("KetQua");
                } else {
                  this.props.history.push("Submit");
                }
              })
              .catch((err) => {
                console.log(err);
              });
          }
        }
      );
    }
  };

  renderAllquetion = () => {
    let mang = [];
    for (let i = 1; i < this.state.endpage; i++) {
      mang.push(i);
    }
    if (mang.length != 0) {
      return mang.map((item, index) => {
        return (
          <div
            className={this.state.indexPage === item + 1 ? "vi" : "d-none"}
            key={index}
          >
            <Allquestion
              data={this.state.Cauhoi}
              page={item}
              socauhoi={this.state.socauhoi}
              next={this.nextpage}
              postdata={this.postData}
              submit={this.dataValue}
              endpage={this.state.endpage}
            />
          </div>
        );
      });
    }
  };
  dataValue = (data) => {
    const CauTraLoi_ChiTietUpdate = this.state.values.CauTraLoi_ChiTiet.concat(
      data
    );
    this.setState({
      values: {
        ...this.state.values,
        CauTraLoi_ChiTiet: CauTraLoi_ChiTietUpdate,
      },
    });
  };
  postData = (data) => {
    this.setState({
      isloading: true,
    });
    var temPlateLocal = JSON.parse(sessionStorage.getItem("ChuDe"));
    const CauTraLoi_ChiTietUpdate = this.state.values.CauTraLoi_ChiTiet.concat(
      data
    );
    this.setState(
      {
        values: {
          ...this.state.values,
          CauTraLoi_ChiTiet: CauTraLoi_ChiTietUpdate,
        },
      },
      () => {
        Axios({
          method: "POST",
          url: "http://localhost:50663/api/ApiCauTraLoi",
          data: this.state.values,
        })
          .then((result) => {
            this.setState({
              isloading: false,
            });
            sessionStorage.setItem("ketqua", JSON.stringify(result.data));
            if (temPlateLocal.TinhDiem === true) {
              this.props.history.push("KetQua");
            } else {
              this.props.history.push("Submit");
            }
          })
          .catch((err) => {
            console.log(err);
          });
      }
    );
  };
  renderPageTitle = () => {
    if (sessionStorage.getItem("ChuDe")) {
      var temPlateLocal = JSON.parse(sessionStorage.getItem("ChuDe"));
      return (
        <div className="page-title-area">
          <h1>{temPlateLocal.TenTemplate}</h1>
          <p>{temPlateLocal.Content}</p>
          <span>*Bắt buộc</span>
        </div>
      );
    }
  };

  nextpage = (data) => {
    this.setState({
      indexPage: data,
    });
  };
  render() {
    return (
      <Fragment>
        {this.state.isloading == true ? (
          <Loading />
        ) : (
          <div className="wrapper_customer">
            {this.renderPageTitle()}
            {this.renderHtmlUser()}
            {this.renderAllquetion()}
          </div>
        )}
      </Fragment>
    );
  }
}
