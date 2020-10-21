import React, { Component } from "react";

export default class pagenotfound extends Component {
  render() {
    return (
      <div>
        <div>
          <a
            href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwjv6PiE0_TrAhWXPXAKHb1JB2o4ChAWMAN6BAgEEAE&url=https%3A%2F%2Fwww.enationalelectronics.com%2F&usg=AOvVaw0CoGpfiGKRton9yE4Q8_Hv"
            target="_blank"
          >
            <header className="top-header"></header>
            {/*dust particel*/}
            <div>
              <div className="starsec" />
              <div className="starthird" />
              <div className="starfourth" />
              <div className="starfifth" />
            </div>
            {/*Dust particle end-*/}
            <div className="lamp__wrap">
              <div className="lamp">
                <div className="cable" />
                <div className="cover" />
                <div className="in-cover">
                  <div className="bulb" />
                </div>
                <div className="light" />
              </div>
            </div>
            {/* END Lamp */}
          </a>
          <section className="error">
            <a
              href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwjv6PiE0_TrAhWXPXAKHb1JB2o4ChAWMAN6BAgEEAE&url=https%3A%2F%2Fwww.enationalelectronics.com%2F&usg=AOvVaw0CoGpfiGKRton9yE4Q8_Hv"
              target="_blank"
            >
              {/* Content */}
            </a>
            <div className="error__content">
              <a
                href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwjv6PiE0_TrAhWXPXAKHb1JB2o4ChAWMAN6BAgEEAE&url=https%3A%2F%2Fwww.enationalelectronics.com%2F&usg=AOvVaw0CoGpfiGKRton9yE4Q8_Hv"
                target="_blank"
              >
                <div className="error__message message">
                  <h1 className="message__title">Page Not Found</h1>
                  <p className="message__text">
                    We're sorry, the page you were looking for isn't found here.
                    The link you followed may either be broken or no longer
                    exists. Please try again, or take a look at our.
                  </p>
                </div>
              </a>
              <div className="error__nav e-nav">
                <a
                  href="https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&cad=rja&uact=8&ved=2ahUKEwjv6PiE0_TrAhWXPXAKHb1JB2o4ChAWMAN6BAgEEAE&url=https%3A%2F%2Fwww.enationalelectronics.com%2F&usg=AOvVaw0CoGpfiGKRton9yE4Q8_Hv"
                  target="_blank"
                ></a>
                <a
                  href="https://www.enationalelectronics.com/"
                  target="_blanck"
                  className="e-nav__link"
                />
              </div>
            </div>
            {/* END Content */}
          </section>
        </div>
      </div>
    );
  }
}
