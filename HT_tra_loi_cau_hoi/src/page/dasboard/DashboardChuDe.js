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
      ChuDe: [],
      isloading: true,
    };
  }
  componentDidMount() {
    this.getChuDe();
    this.getDefaul();
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
  getChuDe = () => {
    Axios({
      method: "GET",
      url: "http://localhost:50663/api/ApiChuDe",
    })
      .then((result) => {
        this.setState({
          ChuDe: result.data,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getTab = (data) => {
    this.props.getdata(data, this.props.history);
    this.props.gettabdata("2");
  };
  renderhtml = () => {
    return this.state.ChuDe.map((item, index) => {
      return (
        <NavLink
          to="/admin/template"
          className="card col-12 col-md-4 mt-3"
          style={{
            textAlign: "center",
            backgroundColor: "transparent",
          }}
          key={index}
        >
          <div
            className="content-card"
            onClick={() => {
              this.getTab(item);
            }}
          >
            <div className="card-body">
              <h4
                className="card-title"
                style={{ color: "white", fontSize: "25px", fontWeight: "bold" }}
              >
                {item.TenChuDe}
              </h4>
              <p className="card-text">{item.MoTa}</p>
              <i className="fa fa-star" aria-hidden="true"></i>
            </div>
            <div
              className="img-cover"
              style={{
                backgroundImage: `url("./asset/img/background_${index}.jpg") `,
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
const mapDispatchToProps = (dispatch) => {
  return {
    getdata: (data, history) => {
      dispatch(action.actGetListTemplateID(data, history));
    },
    gettabdata: (data) => {
      dispatch(action.getTab(data));
    },
  };
};

export default connect(null, mapDispatchToProps)(mydashboard);
