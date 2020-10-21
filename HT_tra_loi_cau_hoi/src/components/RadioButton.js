import React, { useState, useEffect } from "react";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FormControl from "@material-ui/core/FormControl";

export default function RadioButton(props) {
  const [Value, setValue] = useState({ option: "" });
  const [valid, setValid] = useState(!props.data.BatBuoc);
  const onchangeValues = (e) => {
    setValue({ option: e.target.value });
    let valid = false;
    if (props.data.BatBuoc) {
      valid = e.target.value === "" ? false : true;
    } else {
      valid = true;
    }

    props.dataradio({
      IDCauHoi: props.data.IDCauHoi,
      CauTraLoi: e.target.value,
      inputValid: valid,
      BatBuoc: props.data.BatBuoc,
    });
  };
  const renderbutton = () => {
    return props.data.NoiDung.map((item, index) => {
      return (
        <FormControlLabel
          key={index}
          value={item.value}
          control={<Radio color="primary" />}
          label={item.label}
        />
      );
    });
  };

  return (
    <div className={`input-group ${false ? "input-err" : ""}`}>
      <h4>
        {props.data.TieuDe}
        {props.data.BatBuoc ? <span> *</span> : ""}
      </h4>
      <FormControl>
        <RadioGroup
          aria-label="gender"
          value={Value.option}
          name="gender1"
          onChange={onchangeValues}
        >
          {renderbutton()}
        </RadioGroup>
        {/* <FormHelperText className={false ? "err-text" : "d-none"}>
          <i className="fa fa-exclamation"></i> đây là câu hỏi bắt buộc
        </FormHelperText> */}
      </FormControl>
    </div>
  );
}
