import React, { Fragment, Component } from "react";
import InputUser from "./Inputuser";
import { Button } from "@material-ui/core";
import { NavLink } from "react-router-dom";

export default class UserComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      noiDungCauHoi: [],
      valid: false,
      loaiCauHoi: [
        { name: "HoTen", idName: 0, cauHoi: "Họ và tên" },
        { name: "MSNV", idName: 1, cauHoi: "Mã số nhân viên" },
        { name: "Email", idName: 2, cauHoi: "Địa chỉ Email" },
      ],
    };
  }

  renderUser = () => {
    return this.state.loaiCauHoi.map((item, index) => (
      <InputUser key={index} datatext={this.text} data={item} />
    ));
  };

  text = (data) => {
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
        this.checktext();
      }
    );
  };

  checktext = () => {
    let valid;
    let index = this.state.noiDungCauHoi.findIndex((item) => {
      return item.CauTraLoi === "" || item.inputValid === false;
    });
    if (
      this.state.loaiCauHoi.length === this.state.noiDungCauHoi.length &&
      index === -1
    ) {
      valid = true;
    } else {
      valid = false;
    }
    this.setState({
      valid,
    });
  };

  next = () => {
    this.props.submitUser(this.state.noiDungCauHoi);
    this.props.next(2);
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  submit = () => {
    this.props.submitUser(this.state.noiDungCauHoi);
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };
  render() {
    return (
      <div>
        {this.renderUser()}
        {this.props.endpage === 1 ? (
          <Button
            variant="contained"
            color="primary"
            disabled={!this.state.valid}
            onClick={this.submit}
          >
            submit
          </Button>
        ) : (
          <Button
            variant="contained"
            color="primary"
            disabled={!this.state.valid}
            onClick={this.next}
          >
            tiep
          </Button>
        )}
      </div>
    );
  }
}
