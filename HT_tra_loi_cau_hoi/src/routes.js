import UserPage from "./page/home/UserPage";
import AdminUserPage from "./page/dasboard/adminUserpage";
import KetQua from "./page/home/pageKetqua";
import Loading from "./components/loading";
import Submit from "./page/home/pageSubmit";
import DashboardThongKe from "./page/dasboard/Dasboard";
import MyDashboard from "./page/dasboard/DashboardChuDe";
import DashboardTemPlate from "./page/dasboard/DashboardTemplate";
const routesHome = [
  {
    path: `/`,
    exact: true,
    component: UserPage,
  },
  {
    path: "/KetQua",
    exact: false,
    component: KetQua,
  },
  {
    path: "/Submit",
    exact: false,
    component: Submit,
  },
  {
    path: "/loading",
    exact: false,
    component: Loading,
  },
  {
    path: `/thongke`,
    exact: false,
    component: DashboardThongKe,
  },
];

export { routesHome };

const routesAdmin = [
  {
    path: `/admin`,
    exact: true,
    component: MyDashboard,
  },
  {
    path: `/admin/template`,
    exact: false,
    component: DashboardTemPlate,
  },
  {
    path: `/admin/thongke`,
    exact: false,
    component: DashboardThongKe,
  },
  {
    path: `/admin/user`,
    exact: false,
    component: AdminUserPage,
  },
  {
    path: "/admin/KetQua",
    exact: false,
    component: KetQua,
  },
  {
    path: "/admin/Submit",
    exact: false,
    component: Submit,
  },
];

export { routesAdmin };
