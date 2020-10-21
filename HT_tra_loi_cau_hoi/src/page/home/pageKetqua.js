import React, { Component, Fragment } from "react";
import { NavLink } from "react-router-dom";
import { connect } from "react-redux";
import Axios from "axios";
import FormControl from "@material-ui/core/FormControl";

import * as action from "../../redux/action/index";

class pageketqua extends Component {
  constructor(props) {
    super(props);
    this.state = {
      ketqua: {},
      diem: 0,
      dapAn: [],
      hidden: true,
      maxdiem: 0,
    };
  }
  componentDidMount() {
    this.getketqua();
  }
  getketqua = () => {
    if (sessionStorage.getItem("ketqua")) {
      var ketqualocal = JSON.parse(sessionStorage.getItem("ketqua"));
    }
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/TinhDiem/${ketqualocal.IDCauTraLoi}/${ketqualocal.IDTemplate}`,
    })
      .then((result) => {
        this.setState(
          {
            ketqua: result.data,
            diem: result.data.TongDiem,
            dapAn: result.data.DapAn,
          },
          () => {
            let { maxdiem } = this.state;
            this.state.dapAn.map((item, index) => {
              maxdiem += item.DiemMax;
            });
            this.setState({
              maxdiem,
            });
          }
        );
      })
      .catch((err) => {
        console.log(err);
      });
  };
  renderKetqua = () => {
    return this.state.dapAn.map((item, index) => {
      return (
        <div className="input-group" style={{ padding: "16px" }} key={index}>
          <h4 style={{ marginRight: "50px" }}>
            {index + 1} ) {item.TenCauHoi}
          </h4>
          <span
            className={
              item.Diem === 0
                ? "full_diem custom_full_diem"
                : item.Diem < item.DiemMax
                ? "full_diem custom_full_diem_1"
                : "full_diem"
            }
          >
            {item.Diem}/{item.DiemMax}
          </span>
          <FormControl>
            <p> {item.CauTraLoi}</p>
          </FormControl>
        </div>
      );
    });
  };

  showdata = () => {
    let vitri = document.getElementById("content_review").offsetTop;

    window.scrollTo({
      top: vitri,
      behavior: "smooth",
    });

    this.setState({
      hidden: true,
    });
  };
  render() {
    var temPlateLocal;
    if (sessionStorage.getItem("ChuDe")) {
      temPlateLocal = JSON.parse(sessionStorage.getItem("ChuDe"));
    }
    return (
      <Fragment>
        <div className="wrapper_customer">
          <div className="pageKetQua">
            <div className="container">
              <div className="row">
                <div className="content col-12 ">
                  <div className="col-12 title-KetQua">
                    <h2 style={{ textAlign: "center" }}>
                      Bạn đã hoàn thành bài thi
                    </h2>
                  </div>
                  <div className="row col-12" style={{ padding: "40px 0" }}>
                    <div className=" col-7 content-KetQua">
                      <p style={{ marginBottom: "0" }}>
                        {this.state.diem} / {this.state.maxdiem} điểm
                      </p>
                    </div>
                    <img src="../asset/img/teacher(1).png"></img>
                  </div>
                  <div className="col-12 backhome">
                    <NavLink to={temPlateLocal.chose === true ? "/" : "/admin"}>
                      <img src="../asset/img/arrow-back-icon-24.jpg"></img>
                    </NavLink>
                  </div>
                </div>
              </div>
              <div id="content_review">
                {this.state.hidden ? (
                  <div className="content-Review" style={{ marginTop: "82px" }}>
                    {this.renderKetqua()}
                  </div>
                ) : (
                  ""
                )}
              </div>
            </div>
          </div>
        </div>
      </Fragment>
    );
  }
}
const mapStateToProps = (state) => {
  return {
    ketQua: state.deMoReducer.ketQua,
  };
};
export default connect(mapStateToProps, null)(pageketqua);
