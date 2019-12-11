using System;
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
                name: "Delete category",
                url: "category/delete/{id}",
                defaults: new { controller = "Category", action = "Delete" }
            );

            routes.MapRoute(
                name: "Edit subject",
                url: "subject/edit/{id}",
                defaults: new { controller = "Subject", action = "Edit" }
            );

            routes.MapRoute(
                name: "Show subject",
                url: "subject/show/{id}",
                defaults: new { controller = "Subject", action = "Show" }
            );

            routes.MapRoute(
                name: "Add message",
                url: "subject/addmessage/{subjectId}",
                defaults: new { controller = "Subject", action = "AddMessage" }
            );

            routes.MapRoute(
                name: "Delete subject",
                url: "subject/delete/{id}",
                defaults: new { controller = "Subject", action = "Delete" }
            );

            routes.MapRoute(
                name: "Show message",
                url: "message/show/{id}",
                defaults: new { controller = "Message", action = "Show" }
            );

            routes.MapRoute(
                name: "Edit message",
                url: "message/edit/{id}",
                defaults: new { controller = "Message", action = "Edit" }
            );

            routes.MapRoute(
                name: "Delete message",
                url: "message/delete/{id}",
                defaults: new { controller = "Message", action = "Delete" }
            );

            routes.MapRoute(
                name: "New profile",
                url: "profile/new/{userId}",
                defaults: new { controller = "Profile", action = "New" }
            );

            routes.MapRoute(
                name: "Show profile",
                url: "profile/show/{id}",
                defaults: new { controller = "Profile", action = "Show" }
            );

            routes.MapRoute(
                name: "Edit profile",
                url: "profile/edit/{id}",
                defaults: new { controller = "Profile", action = "Edit" }
            );

            routes.MapRoute(
                name: "Find term",
                url: "search/find/{searchedTerm}",
                defaults: new { controller = "Search", action = "Find", searchedTerm = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
