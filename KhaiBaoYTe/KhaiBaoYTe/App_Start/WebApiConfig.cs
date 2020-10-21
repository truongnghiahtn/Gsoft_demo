using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KhaiBaoYTe
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CauHoiApi",
                routeTemplate: "api/CauHoi/{idTemplate}",
                defaults: new { controller = "ApiCauHoi", action = "GetCauHois" }
            );
            config.Routes.MapHttpRoute(
                name : "TemplateApi",
                routeTemplate : "api/Templates/{idChuDe}",
                defaults: new { controller = "ApiTemplate", action = "GetTemplates" }
             );
            config.Routes.MapHttpRoute(
                name: "TinhDiemApi",
                routeTemplate: "api/TinhDiem/{idCauTraLoi}/{idTemplate}",
                defaults: new { controller = "ApiTinhDiem", action = "GetTongSoDiem" }
                );
            
               
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
