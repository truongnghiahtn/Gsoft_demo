import React, { Component, Fragment } from "react";
import Axios from "axios";
import { connect } from "react-redux";
import * as action from "../../redux/action/index";
import UserComponent from "../../components/UserComponent";
import Allquestion from "../../components/allquestion";
import { Select, Input, MenuItem, Button } from "@material-ui/core";
import { map } from "jquery";
import Loading from "../../components/loading";
import uniqby from "lodash.uniqby";
class userpage extends Component {
  // constructor(props) {
  //   super(props);
  //   this.state = {

  //   };
  // }
  state = {
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
    Default: [],
    isloading: false,
    DetailDefault: {},
    status: true,
  };
  componentDidMount() {
    this.getDefaul();
  }
  getDefaul = () => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiDefault`,
    })
      .then((result) => {
        this.setState(
          {
            Default: result.data,
          },
          () => {
            if (this.state.Default.length === 1) {
              this.setState(
                {
                  DetailDefault: this.state.Default[0],
                  socauhoi: this.state.Default[0].SoLgHienThi,
                },
                () => {
                  sessionStorage.setItem(
                    "ChuDe",
                    JSON.stringify(this.state.DetailDefault)
                  );
                  this.getCauHoi();
                }
              );
            } else {
              if (this.state.Default[0].chose === false) {
                window.location.href = "/admin";
              } else {
                this.setState({
                  isloading: true,
                  status: false,
                });
              }
            }
          }
        );
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getCauHoi = () => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/CauHoi/${this.state.DetailDefault.IDTemplate}`,
      // url: `http://localhost:50663/api/CauHoi/10`,
    })
      .then((result) => {
        this.setState(
          {
            Cauhoi: result.data,
          },
          () => {
            this.checkpage();
            this.checkloading();
          }
        );
      })
      .catch((err) => {
        console.log(err);
      });
  };
  checkloading = () => {
    if (this.state.Cauhoi.length > 0) {
      this.setState({
        isloading: true,
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
          IDTemplate: this.state.DetailDefault.IDTemplate,
          IDChuDe: this.state.DetailDefault.IDChuDe,
        },
      },
      () => {
        if (this.state.endpage == 1) {
          this.setState({
            isloading: false,
          });
          Axios({
            method: "POST",
            url: "http://localhost:50663/api/ApiCauTraLoi",
            data: this.state.values,
          })
            .then((result) => {
              this.props.getketqua(result.data);
              sessionStorage.setItem("ketqua", JSON.stringify(result.data));
              if (this.state.DetailDefault.TinhDiem === true) {
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

    let newCauTraLoi = uniqby(
      CauTraLoi_ChiTietUpdate.reverse(),
      (item) => item.IDCauHoi
    );

    this.setState(
      {
        values: {
          ...this.state.values,
          CauTraLoi_ChiTiet: newCauTraLoi,
        },
      },
      () => {}
    );
  };
  postData = (data) => {
    const CauTraLoi_ChiTietUpdate = this.state.values.CauTraLoi_ChiTiet.concat(
      data
    );
    this.setState(
      {
        values: {
          ...this.state.values,
          CauTraLoi_ChiTiet: CauTraLoi_ChiTietUpdate,
        },
        isloading: false,
      },
      () => {
        Axios({
          method: "POST",
          url: "http://localhost:50663/api/ApiCauTraLoi",
          data: this.state.values,
        })
          .then((result) => {
            this.props.getketqua(result.data);
            sessionStorage.setItem("ketqua", JSON.stringify(result.data));
            if (this.state.DetailDefault.TinhDiem === true) {
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
    return (
      <div className="page-title-area">
        <h1>{this.state.DetailDefault.TenTemplate}</h1>
        <p>{this.state.DetailDefault.Content}</p>
        <span>*Bắt buộc</span>
      </div>
    );
  };

  nextpage = (data) => {
    this.setState({
      indexPage: data,
    });
  };
  renderChuDe = () => {
    return this.state.Default.map((item, index) => (
      <MenuItem key={index} value={item}>
        <em>{item.TenTemplate}</em>
      </MenuItem>
    ));
  };
  onChangeChuDe = (e) => {
    sessionStorage.setItem("ChuDe", JSON.stringify(e.target.value));
    this.setState(
      {
        DetailDefault: e.target.value,
        status: true,
        socauhoi: e.target.value.SoLgHienThi,
      },
      () => {
        this.getCauHoi();
      }
    );
  };
  renderselect = () => {
    return (
      <div className="tp-content col-12   " style={{ margin: "0 auto" }}>
        <div className="tp-cotent__title">Lựa chọn form</div>
        <div className="input-group chude-temp">
          <div className="row ">
            <div className="col-12">
              <Select
                input={<Input />}
                value="-1"
                onChange={this.onChangeChuDe}
              >
                <MenuItem value={-1}>
                  <em>Vui lòng chọn chủ đề</em>
                </MenuItem>
                {this.renderChuDe()}
              </Select>
            </div>
          </div>
        </div>
      </div>
    );
  };
  render() {
    return (
      <Fragment>
        {this.state.isloading === true ? (
          this.state.status === false ? (
            this.renderselect()
          ) : (
            <Fragment>
              {this.renderPageTitle()}
              {this.renderHtmlUser()}
              {this.renderAllquetion()}{" "}
            </Fragment>
          )
        ) : (
          <Loading />
        )}
      </Fragment>
    );
  }
}
const mapDispatchToProps = (dispatch) => {
  return {
    getketqua: (data) => {
      dispatch(action.getKetQua(data));
    },
  };
};
export default connect(null, mapDispatchToProps)(userpage);
