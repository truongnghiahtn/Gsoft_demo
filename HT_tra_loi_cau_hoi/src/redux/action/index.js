import * as Actiontype from "./../constants/actionType";
import Axios from "axios";

export const actGetListTemplate = (data) => {
  return (dispatch) => {
    dispatch({
      type: Actiontype.LOADING,
      isLoading: true,
    });
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/ApiAllTemplate`,
    })
      .then((result) => {
        dispatch({
          type: Actiontype.GET_LIST_TEMPLATE,
          Template: result.data,
          isLoading: false,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
};
export const actGetListTemplateID = (data, history) => {
  return (dispatch) => {
    Axios({
      method: "GET",
      url: `http://localhost:50663/api/Templates/${data.IDChuDe}`,
    })
      .then((result) => {
        dispatch({
          type: Actiontype.GET_LIST_TEMPLATE,
          Template: result.data,
        });
      })
      .catch((err) => {
        console.log(err);
      });
  };
};

export const getTab = (data) => {
  return (dispatch) => {
    dispatch({
      type: Actiontype.ACTION_TAB,
      Tab: data,
    });
  };
};
export const getSelect = (data) => {
  return (dispatch) => {
    dispatch({
      type: Actiontype.ACTION_SELECT,
      Select: data,
    });
  };
};
export const getSubmit = (data) => {
  return (dispatch) => {
    dispatch({
      type: Actiontype.HOANTHANH,
      hoanthanh: data,
    });
  };
};
export const getKetQua = (data) => {
  return (dispatch) => {
    dispatch({
      type: Actiontype.KETQUA,
      ketQua: data,
    });
  };
};
