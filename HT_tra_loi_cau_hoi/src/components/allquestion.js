import React, { Component } from "react";
import InputText from "./../components/InputText";
import CheckBox from "./../components/CheckBox";
import RadioButton from "./../components/RadioButton";
import { Button } from "@material-ui/core";
import { NavLink } from "react-router-dom";

export default class componentName extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cauhoi: [],
      noiDungCauHoi: [],
      valid: false,
    };
  }
  componentDidMount() {
    this.cauhoi();
  }
  cauhoi = () => {
    let { page, socauhoi, data } = this.props;
    let max = data.length;
    let mang = [];
    for (let i = (page - 1) * socauhoi; i < socauhoi * page; i++) {
      if (i == max) {
        break;
      } else {
        mang.push(data[i]);
      }
    }
    this.setState({
      cauhoi: mang,
    });
  };

  renderQuestion = () => {
    return this.state.cauhoi.map((item, index) => {
      switch (item.DangCauHoi) {
        case "TextBox":
          return <InputText key={index} datatext={this.dataPost} data={item} />;

        case "CheckBox":
          return <CheckBox key={index} datacheck={this.dataPost} data={item} />;

        case "RadioButton":
          return (
            <RadioButton key={index} dataradio={this.dataPost} data={item} />
          );

        default:
          break;
      }
    });
  };

  dataPost = (data) => {
    let noiDungCauHoiUpdate = this.state.noiDungCauHoi;
    //
    let index = this.state.noiDungCauHoi.findIndex((item) => {
      return item.IDCauHoi == data.IDCauHoi;
    });
    if (index != -1) {
      noiDungCauHoiUpdate[index] = data;
    } else {
      // post
      noiDungCauHoiUpdate = [...this.state.noiDungCauHoi, data];
    }
    this.setState(
      {
        noiDungCauHoi: noiDungCauHoiUpdate,
      },
      () => {
        this.check();
      }
    );
  };

  check = () => {
    let valid;
    let index = this.state.noiDungCauHoi.findIndex((item) => {
      return item.inputValid === false;
    });
    let mangTrue = this.state.cauhoi.filter((item) => {
      return item.BatBuoc === true;
    });

    let mangNoiDungCauHoi = this.state.noiDungCauHoi.filter((item) => {
      return item.BatBuoc === true && item.CauTraLoi != "";
    });

    if (mangTrue.length === mangNoiDungCauHoi.length && index === -1) {
      valid = true;
    } else {
      valid = false;
    }
    this.setState({
      valid,
    });
  };
  next = () => {
    this.props.submit(this.state.noiDungCauHoi);
    this.props.next(this.props.page + 2);
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  prve = () => {
    this.props.next(this.props.page);
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  submit = () => {
    this.props.postdata(this.state.noiDungCauHoi);
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  render() {
    return (
      <div>
        {this.renderQuestion()}
        <Button
          variant="contained"
          color="primary"
          className="mr-2"
          onClick={this.prve}
        >
          Quay lại
        </Button>

        {this.props.endpage === this.props.page + 1 ? (
          <Button
            variant="contained"
            color="primary"
            disabled={!this.state.valid}
            onClick={this.submit}
          >
            Hoàn thành
          </Button>
        ) : (
          <Button
            variant="contained"
            color="primary"
            disabled={!this.state.valid}
            onClick={this.next}
          >
            Tiếp
          </Button>
        )}
      </div>
    );
  }
}
