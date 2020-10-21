import React, { Component } from "react";

export default class loading extends Component {
  render() {
    return (
      <section className="circle-loadding-wrap">
        <img
          src="https://lg-bv0kx7xc-1253325524.cos.ap-shanghai.myqcloud.com/loading-2.png"
          className="circle-loadding-inner circle-loadding-small"
        />
        <img
          src="https://lg-bv0kx7xc-1253325524.cos.ap-shanghai.myqcloud.com/laoding-3.png"
          className="circle-loadding-inner circle-loadding-big"
        />
        <div hidden className="circle-loadding-inner circle-loadding-upper" />
        <div
          hidden
          className="circle-loadding-inner circle-loadding-upper circle-loadding-second"
        />
      </section>
    );
  }
}
