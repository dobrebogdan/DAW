﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAW {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Categories",
                url: "categories",
                defaults: new { controller = "Category", action = "Index"}
            );

            routes.MapRoute(
                name: "New category",
                url: "category/new",
                defaults: new { controller = "Category", action = "New" }
            );

            routes.MapRoute(
                name: "Categories index",
                url: "category/index",
                defaults: new { controller = "Category", action = "Index" }
            );

            routes.MapRoute(
                name: "Edit category",
                url: "category/edit/{id}",
                defaults: new { controller = "Category", action = "Edit" }
            );

            routes.MapRoute(
                name: "Categories index by subjects",
                url: "category/indexbysubjects",
                defaults: new { controller = "Category", action = "IndexBySubjects" }
            );

            routes.MapRoute(
                name: "Category",
                url: "category/show/{id}",
                defaults: new { controller = "Category", action = "Show" }
            );

            routes.MapRoute(
                name: "Add subject",
                url: "category/addsubject/{categoryId}",
                defaults: new { controller = "Category", action = "AddSubject" }
            );

            routes.MapRoute(
                name: "Edit subject",
                url: "subject/edit/{id}",
                defaults: new { controller = "Subject", action = "Edit" }
            );

            routes.MapRoute(
                name: "Show subject",
                url: "subject/show/{id}",
                defaults: new { controller = "Subject", action = "Show"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
