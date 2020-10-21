import React, { useState, useEffect } from "react";
import Checkbox from "@material-ui/core/Checkbox";
import FormControl from "@material-ui/core/FormControl";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FormHelperText from "@material-ui/core/FormHelperText";

export default function CheckBox(props) {
  const [values, setValue] = useState([]);
  const [err, setErr] = useState(false);

  useEffect(() => {
    props.datacheck({
      IDCauHoi: props.data.IDCauHoi,
      CauTraLoi: values.join(),
      inputValid: true,
      BatBuoc: props.data.BatBuoc,
    });
    setErr(false);
  }, [values]);
  const helo = () => {
    let valid = false;
    if (props.data.BatBuoc) {
      if (!values.length) {
        setErr(true);
        valid = false;
      } else {
        valid = true;
      }
    } else {
      setErr(false);
      valid = true;
    }
    props.datacheck({
      IDCauHoi: props.data.IDCauHoi,
      CauTraLoi: values.join(),
      inputValid: valid,
      BatBuoc: props.data.BatBuoc,
    });
  };
  const onchangeFormControl = (e) => {
    let { value } = e.target;
    let index = values.findIndex((item) => {
      return item === value;
    });
    if (index != -1) {
      values.splice(index, 1);
      setValue(values);
      helo();
    } else {
      setValue([...values, value]);
    }
  };

  return (
    <div className={`input-group ${err ? "input-err" : ""}`}>
      <h4>
        {props.data.TieuDe}
        {props.data.BatBuoc ? <span> *</span> : ""}
      </h4>
      <FormControl error={true} onChange={onchangeFormControl}>
        {props.data.NoiDung.map((item, index) => (
          <FormControlLabel
            value={item.value}
            key={index}
            control={<Checkbox color="primary" />}
            label={item.label}
            // labelPlacement={item}
          />
        ))}
        <FormHelperText className={err ? "err-text" : "d-none"}>
          <i className="fa fa-exclamation"></i> Đây là một câu hỏi bắt buộc
        </FormHelperText>
      </FormControl>
    </div>
  );
}
