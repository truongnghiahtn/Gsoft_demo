import React, { Component, Fragment } from "react";
import { NavLink } from "react-router-dom";
import { connect } from "react-redux";
import * as action from "../../redux/action/index";

class pagesubmit extends Component {
  thongke = () => {
    this.props.gettabdata("3");
    this.props.getsubmit("ok");
    if (sessionStorage.getItem("template")) {
      var temPlateLocal = JSON.parse(sessionStorage.getItem("template"));
      this.props.getselect(temPlateLocal);
      console.log(temPlateLocal);
    }
  };
  render() {
    var temPlateLocal;
    if (sessionStorage.getItem("ChuDe")) {
      temPlateLocal = JSON.parse(sessionStorage.getItem("ChuDe"));
    }
    return (
      <Fragment>
        <div className="wrapper_customer">
          <div className="page-title-area page-title-v2 nghia">
            <h3 className="mt-3" style={{ color: "#1e1a5f" }}>
              Tờ khai báo y tế tự nguyện công ty GSOFT và GOBRANDING
            </h3>
            <p style={{ paddingTop: "30px" }}>
              "Câu trả lời của bạn đã được ghi lại.
            </p>
            <div className="row justify-content-between">
              <NavLink
                className="go-home"
                to={temPlateLocal.chose === true ? "/" : "/admin"}
                onClick={() => {
                  this.props.gettabdata("1");
                }}
              >
                Trang chủ
              </NavLink>
              <NavLink
                className="go-home"
                to={
                  temPlateLocal.chose === true ? "/thongke" : "/admin/thongke"
                }
                onClick={this.thongke}
              >
                {" "}
                Xem chi tiết gửi phản hồi
              </NavLink>
            </div>
          </div>
        </div>
      </Fragment>
    );
  }
}
const mapDispatchToProps = (dispatch) => {
  return {
    gettabdata: (data) => {
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
export default connect(null, mapDispatchToProps)(pagesubmit);
