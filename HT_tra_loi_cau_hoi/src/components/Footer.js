import React, { Component } from 'react';

export default class footer extends Component {
    render() {
        return (
            <div className="footer ">
                <div className="container">
                    <div className="d-flex justify-content-between">
                        <div className="footer-left ">
                            <div className="footer-icon-right"> <i className="fa fa-volume-control-phone" aria-hidden="true"></i></div>
                            <div className="footer-title">
                                <p>Đường dây nóng của bộ y tế</p>
                                <h2>19003228/ 19009095</h2>
                            </div>
                        </div>
                        <div className="footer-right ">
                            <p>https://ncovi.vnpt.vn</p>
                        </div>
                    </div>

                </div>
            </div>
        );
    }
}
