import React, { useState, useEffect } from "react";
import FormControl from "@material-ui/core/FormControl";
import FormHelperText from "@material-ui/core/FormHelperText";
import Input from "@material-ui/core/Input";
export default function Inputuser(props) {
  const [Value, setValue] = useState({ text: "", inputValid: true });
  const [errValue, seterrValue] = useState("");
  const Onchange = (e) => {
    let { name, value } = e.target;

    let message = e.target.value === "" ? " Đây là một câu hỏi bắt buộc" : "";
    let Valid = false;
    switch (name) {
      case "Email":
        Valid = message !== "" ? false : true;
        if (
          value !== "" &&
          !value.match(
            "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" +
              "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$"
          )
        ) {
          Valid = false;
          message = "email không hợp lệ";
        }
        break;
      case "HoTen":
        Valid = message !== "" ? false : true;
        if (
          value !== "" &&
          !value.match(
            "^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶ" +
              "ẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆẾỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợ" +
              "ụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s]+$"
          )
        ) {
          Valid = false;
          message = "Họ tên của bạn không hợp lệ";
        }
        break;
      case "MSNV":
        Valid = message !== "" ? false : true;
        break;
      default:
        break;
    }
    props.datatext({
      CauTraLoi: value,
      IDCauHoi: props.data.idName,
      name: name,
      inputValid: Valid,
    });
    setValue({
      text: value,
      inputValid: Valid,
    });

    seterrValue(message);
  };
  //

  //
  return (
    <div className={`input-group ${!Value.inputValid ? "input-err" : ""}`}>
      <h4>
        {props.data.cauHoi}
        {true ? <span> *</span> : ""}
      </h4>
      <FormControl err={"false"}>
        <Input
          onChange={Onchange}
          error={!Value.inputValid}
          placeholder="Câu trả lời của bạn"
          name={props.data.name}
        />
        <FormHelperText className={!Value.inputValid ? "err-text" : "d-none"}>
          <i className="fa fa-exclamation"></i> {errValue}
        </FormHelperText>
      </FormControl>
    </div>
  );
}
