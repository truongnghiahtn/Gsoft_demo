import * as ActionType from "../constants/actionType";

let initialState = {
  TemplateList: [],
  tab: "1",
  template: "all",
  chude: "all",
  isLoading: false,
  ketQua: [],
  hoanthanh: "",
};
const deMoReducer = (state = initialState, action) => {
  switch (action.type) {
    case ActionType.LOADING:
      state.isLoading = action.isLoading;
      return { ...state };
    case ActionType.GET_LIST_TEMPLATE:
      state.TemplateList = action.Template;
      sessionStorage.setItem("TabTemplate", JSON.stringify(action.Template));
      return { ...state };
    case ActionType.ACTION_TAB:
      state.tab = action.Tab;
      return { ...state };
    case ActionType.ACTION_SELECT:
      state.template = action.Select.IDTemplate;
      state.chude = action.Select.IDChuDe;
      return { ...state };
    case ActionType.KETQUA:
      state.ketQua = action.ketQua;
      return { ...state };
    case ActionType.HOANTHANH:
      state.hoanthanh = action.hoanthanh;
      return { ...state };

    default:
      return { ...state };
  }
};
export default deMoReducer;
