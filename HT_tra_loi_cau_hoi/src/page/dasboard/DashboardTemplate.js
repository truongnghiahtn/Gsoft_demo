import React, { Component, Fragment } from "react";
import * as action from "../../redux/action/index";
import Axios from "axios";
import { connect } from "react-redux";
import { NavLink } from "react-router-dom";
import Loading from "./../../components/loading";

class mydashboard extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isloading: true,
    };
  }

  componentDidMount() {
    this.getDefaul();
    this.props.gettabdata("2");

    setTimeout(() => {
      this.setState({
        isloading: false,
      });
    }, 200);
  }

  getDefaul = () => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiDefault`,
    })
      .then((result) => {
        if (result.data[0].chose === true) {
          window.location.href = "/";
        }
      })
      .catch((err) => {
        console.log(err);
      });
  };
  onnext = (data) => {
    sessionStorage.setItem("ChuDe", JSON.stringify(data));
  };
  renderhtml = () => {
    let mang = [];
    if (sessionStorage.getItem("TabTemplate")) {
      mang = JSON.parse(sessionStorage.getItem("TabTemplate"));
    }
    return mang.map((item, index) => {
      return (
        <NavLink
          to="/admin/user"
          className="card col-12 col-sm-6 col-md-4 mt-3"
          style={{
            textAlign: "center",
            backgroundColor: "transparent",
          }}
          onClick={() => {
            this.onnext(item);
          }}
          key={index}
        >
          <div className="content-card">
            <div className="card-body">
              <h4
                className="card-title"
                style={{
                  color: "white",
                  fontSize: "20px",
                  textAlign: "center",
                  marginTop: "20px",
                }}
              >
                {item.TenTemplate}
              </h4>
              <i className="fa fa-star" aria-hidden="true"></i>
            </div>
            <div
              className="img-cover"
              style={{
                backgroundImage: `url("../asset/img/background_${index}.jpg") `,
              }}
            ></div>
          </div>
        </NavLink>
      );
    });
  };
  render() {
    return (
      <Fragment>
        {this.state.isloading === true ? (
          <Loading />
        ) : (
          <div className="data-content">
            <div className="container">
              <div className="row">{this.renderhtml()}</div>
            </div>
          </div>
        )}
      </Fragment>
    );
  }
}
const mapStateToProps = (state) => {
  return {
    data: state.deMoReducer.TemplateList,
  };
};
const mapDispatchToProps = (dispatch) => {
  return {
    gettabdata: (data) => {
      dispatch(action.getTab(data));
    },
    getTemplate: () => {
      dispatch(action.actGetListTemplate());
    },
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(mydashboard);
