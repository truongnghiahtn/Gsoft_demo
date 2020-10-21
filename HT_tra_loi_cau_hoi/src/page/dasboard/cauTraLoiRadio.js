import React, { Component } from "react";
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import $ from "jquery";

export default class cautraloiradio extends Component {
  constructor(props) {
    super(props);
    this.state = {
      soluongtl: 0,
    };
  }
  componentDidMount() {
    setTimeout(() => {
      this.render3DPie();
    }, 200);
  }
  render3DPie = () => {
    var x = 0;
    if (this.props.dem) {
      x = this.props.dem;
    }

    let mangSL = this.props.data.NoiDung.map((item) => item.SoLuong);
    let total = mangSL.reduce(
      (accumulator, currentValue) => accumulator + currentValue
    );

    let newmang = [];

    this.props.data.NoiDung.map((item, index) => {
      let obj = {
        country: "",
        litres: "",
      };
      obj.country = item.TenSub;
      obj.litres = item.SoLuong;
      newmang.push(obj);
    });

    this.setState({
      soluongtl: total,
    });
    am4core.ready(function () {
      am4core.useTheme(am4themes_animated);
      // Themes end

      var chart = am4core.create(`am-3dpie-chart${x}`, am4charts.PieChart3D);
      chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

      chart.legend = new am4charts.Legend();
      chart.data = newmang;

      var series = chart.series.push(new am4charts.PieSeries3D());
      series.colors.list = [
        am4core.color("#089bab"),
        am4core.color("#FC9F5B"),
        am4core.color("#57de53"),
        am4core.color("#f26361"),
        am4core.color("#ababab"),
        am4core.color("#61e2fc"),
      ];
      series.dataFields.value = "litres";
      series.dataFields.category = "country";
    });
  };
  render() {
    return (
      <div className="DS-content col-12" style={{ margin: "0 auto" }}>
        <div className="content-Parent tp-content">
          <div className="tp-cotent__title">{this.props.data.TenCauHoi} ? </div>
          <div className="content">
            <div className="iq-cards ">
              <div className="iq-card-header d-flex justify-content-between">
                <div className="iq-header-title">
                  <h4 className="card-title"> </h4>
                </div>
              </div>
              <div className="iq-card-body">
                <div
                  id={"am-3dpie-chart" + this.props.dem}
                  style={{ height: "290px" }}
                ></div>
              </div>
            </div>
            <p className="pt-3 m-0">
              {" "}
              Tổng số câu trả lời : {this.state.soluongtl}{" "}
            </p>
          </div>
        </div>
      </div>
    );
  }
}
