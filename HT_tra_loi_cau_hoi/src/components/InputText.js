import React, { useState, useEffect } from "react";
import FormControl from "@material-ui/core/FormControl";
import FormHelperText from "@material-ui/core/FormHelperText";
import Input from "@material-ui/core/Input";
export default function InputText(props) {
  const [Value, setValue] = useState({ text: "", inputValid: true });
  const [errValue, seterrValue] = useState("");
  useEffect(() => {
    if (!Value.inputValid) {
      seterrValue("Đây là một câu hỏi bắt buộc");
    }
  }, [Value, props]);

  const Onchange = (e) => {
    let valid = false;
    if (props.data.BatBuoc) {
      valid = e.target.value === "" ? false : true;
    } else {
      valid = true;
    }
    props.datatext({
      CauTraLoi: e.target.value,
      IDCauHoi: props.data.IDCauHoi,
      inputValid: valid,
      BatBuoc: props.data.BatBuoc,
    });
    setValue({
      text: e.target.value,
      inputValid: valid,
    });
  };
  //

  //
  return (
    <div className={`input-group ${!Value.inputValid ? "input-err" : ""}`}>
      <h4>
        {props.data.TieuDe}
        {props.data.BatBuoc ? <span> *</span> : ""}
      </h4>
      <FormControl err={"false"}>
        <Input
          onChange={Onchange}
          error={!Value.inputValid}
          placeholder="Câu trả lời của bạn"
        />
        <FormHelperText className={!Value.inputValid ? "err-text" : "d-none"}>
          <i className="fa fa-exclamation"></i> {errValue}
        </FormHelperText>
      </FormControl>
    </div>
  );
}
