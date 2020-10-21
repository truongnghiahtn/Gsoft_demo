import React, { Component, Fragment } from "react";
import { Select, Input, MenuItem, Button } from "@material-ui/core";
// import PageTitleArea from "../../components/PageTitleArea";
import Axios from "axios";
// import { NavLink } from "react-router-dom";
import Cauhoi from "./cauHoi";
import CauTraLoi from "./cauTraLoi";
import CauTraLoiText from "./cauTraLoiText";
import CauTraLoiRadio from "./cauTraLoiRadio";
import CauTraLoiCheck from "./cauTraLoiCheck";
import { connect } from "react-redux";
import * as action from "../../redux/action/index";
import Loading from "./../../components/loading";

class dasboard extends Component {
  constructor(props) {
    super(props);
    this.state = {
      chuDe: [],
      template: [],
      chuDeSelected: -1,
      templateSelected: -1,
      CauHoi: [],
      CauTraLoi: [],
      CauTraLoiText: [],
      CauTraLoiRadio: [],
      CauTraLoiCheckBox: [],
      slchude: 0,
      slTempalte: 0,
      isloading: true,
    };
  }

  componentDidMount() {
    this.getChuDe();
    this.getall();
    this.props.gettabdata("3");
    this.getDefaul();
  }
  getDefaul = () => {
    if (sessionStorage.getItem("ChuDe")) {
      var ChuDelocal = JSON.parse(sessionStorage.getItem("ChuDe"));
      if (this.props.Tab === "ok") {
        this.setState(
          {
            chuDeSelected: ChuDelocal.IDChuDe,
            templateSelected: ChuDelocal.IDTemplate,
          },
          () => {
            this.getTemplate(this.state.chuDeSelected);
            this.getCauHoi(this.state.templateSelected);
            this.getCauTraLoi(this.state.templateSelected);
            this.getCauTraLoiText(this.state.templateSelected);
            this.getCauTraLoiRadio(this.state.templateSelected);
            this.getCauTraLoiCheckBox(this.state.templateSelected);
          }
        );
      } else {
        this.setState({
          isloading: false,
        });
      }
    } else {
      this.setState({
        isloading: false,
      });
    }
  };
  getChuDe = () => {
    Axios({
      method: "GET",
      url: "http://localhost:50663/api/ApiChuDe",
    })
      .then((result) => {
        this.setState({
          chuDe: result.data,
          slchude: result.data.length,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getTemplate = (id) => {
    this.setState({
      isloading: true,
    });
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/Templates/${id}`,
    })
      .then((result) => {
        this.setState({
          template: result.data,
          isloading: false,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getall = () => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiAllTemplate`,
    })
      .then((result) => {
        this.setState(
          {
            all: result.data,
            template: result.data,
          },
          () => {
            this.setState({
              slTempalte: result.data.length,
            });
          }
        );
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getCauHoi = (id) => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/CauHoi/${id}`,
    })
      .then((result) => {
        this.setState({
          CauHoi: result.data,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getCauTraLoi = (id) => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiTemplate_4/${id}`,
    })
      .then((result) => {
        this.setState({
          CauTraLoi: result.data.reverse(),
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };

  getCauTraLoiText = (id) => {
    this.setState({
      isloading: true,
    });
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiTemplate_text/${id}`,
    })
      .then((result) => {
        this.setState({
          CauTraLoiText: result.data.reverse(),
          isloading: false,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getCauTraLoiRadio = (id) => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiTemplate_radio/${id}`,
    })
      .then((result) => {
        this.setState({
          CauTraLoiRadio: result.data,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
  getCauTraLoiCheckBox = (id) => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiTemplate_checkbox/${id}`,
    })
      .then((result) => {
        this.setState({
          CauTraLoiCheckBox: result.data,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };

  renderChuDe = () => {
    return this.state.chuDe.map((item, index) => (
      <MenuItem key={index} value={item.IDChuDe}>
        <em>{item.TenChuDe}</em>
      </MenuItem>
    ));
  };
  renderTemplate = () => {
    return this.state.template.map((item, index) => (
      <MenuItem key={index} value={item.IDTemplate}>
        <em>{item.TenTemplate}</em>
      </MenuItem>
    ));
  };
  onChangeChuDe = (e) => {
    this.setState(
      {
        chuDeSelected: e.target.value,
        templateSelected: -1,
      },
      () => {
        this.getTemplate(e.target.value);
      }
    );
  };
  onChangeTemplate = (e) => {
    this.setState(
      {
        templateSelected: e.target.value,
      },
      () => {
        this.getCauHoi(e.target.value);
        this.getCauTraLoi(e.target.value);
        this.getCauTraLoiText(e.target.value);
        this.getCauTraLoiRadio(e.target.value);
        this.getCauTraLoiCheckBox(e.target.value);
      }
    );
  };

  renderhtmlCauTraLoiText = () => {
    return this.state.CauTraLoiText.map((item, index) => {
      return <CauTraLoiText key={index} data={item} />;
    });
  };
  renderhtmlCauTraLoiRadio = () => {
    return this.state.CauTraLoiRadio.map((item, index) => {
      return <CauTraLoiRadio key={index} data={item} dem={index} />;
    });
  };
  renderhtmlCauTraLoiCheckBox = () => {
    return this.state.CauTraLoiCheckBox.map((item, index) => {
      return <CauTraLoiCheck key={index} data={item} dem={index} />;
    });
  };

  render() {
    return (
      <Fragment>
        {this.state.isloading ? (
          <Loading />
        ) : (
          <Fragment>
            <div className="wrapper_customer">
              <div className="row">
                <div className="col-12 " style={{ margin: "10px auto" }}>
                  <div className="row">
                    <div className="col-md-6 col-lg-6">
                      <div className="iq-card">
                        <div
                          className="iq-card-body iq-bg-primary rounded"
                          style={{ borderRadius: "1px" }}
                        >
                          <div className="d-flex align-items-center justify-content-between">
                            <div className="rounded-circle iq-card-icon bg-primary">
                              <i
                                className="fa fa-id-card"
                                aria-hidden="true"
                                style={{ padding: "10px", lineHeight: "36px" }}
                              ></i>
                            </div>
                            <div className="text-right">
                              <h2 className="mb-0">{this.state.slchude}</h2>
                              <h5>Chủ đề</h5>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div className="col-md-6 col-lg-6">
                      <div className="iq-card">
                        <div className="iq-card-body iq-bg-warning rounded">
                          <div className="d-flex align-items-center justify-content-between">
                            <div className="rounded-circle iq-card-icon bg-warning">
                              <i
                                className="fa fa-id-card-o"
                                aria-hidden="true"
                                style={{ padding: "10px", lineHeight: "36px" }}
                              ></i>
                            </div>
                            <div className="text-right">
                              <h2 className="mb-0">{this.state.slTempalte}</h2>
                              <h5>Template</h5>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div
                className="tp-content col-12   "
                style={{ margin: "0 auto" }}
              >
                <div className="tp-cotent__title">Lựa chọn form</div>
                <div className="input-group chude-temp">
                  <div className="row ">
                    <div className="col-12">
                      <Select
                        input={<Input />}
                        value={this.state.chuDeSelected}
                        onChange={this.onChangeChuDe}
                      >
                        <MenuItem value={-1}>
                          <em>Vui lòng chọn chủ đề</em>
                        </MenuItem>
                        {this.renderChuDe()}
                      </Select>
                    </div>
                    {this.state.chuDeSelected === -1 ? (
                      ""
                    ) : (
                      <div className="col-12" style={{ marginTop: "20px" }}>
                        <Select
                          input={<Input />}
                          value={this.state.templateSelected}
                          onChange={this.onChangeTemplate}
                          disabled={
                            this.state.chuDeSelected === -1 ? true : false
                          }
                        >
                          <MenuItem disabled value={-1}>
                            <em>Vui lòng chọn Template</em>
                          </MenuItem>
                          {this.renderTemplate()}
                        </Select>
                      </div>
                    )}
                  </div>
                </div>
              </div>
              <div
                className={this.state.templateSelected != -1 ? "vi" : "d-none"}
              >
                <div className="Dasboard row">
                  <Cauhoi data={this.state.CauHoi} />
                  <CauTraLoi data={this.state.CauTraLoi} title="Hoten" />
                  <CauTraLoi data={this.state.CauTraLoi} title="MSNV" />
                  <CauTraLoi data={this.state.CauTraLoi} title="Email" />
                  {this.renderhtmlCauTraLoiText()}
                  {this.renderhtmlCauTraLoiRadio()}
                  {this.renderhtmlCauTraLoiCheckBox()}
                </div>
              </div>
            </div>
          </Fragment>
        )}
      </Fragment>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    temPlate: state.deMoReducer.template,
    chuDe: state.deMoReducer.chude,
    Tab: state.deMoReducer.hoanthanh,
  };
};
const mapDispatchToProps = (dispatch) => {
  return {
    gettabdata: (data) => {
      dispatch(action.getTab(data));
    },
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(dasboard);
