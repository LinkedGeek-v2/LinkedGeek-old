using System.Web.Optimization;

namespace GroupProject
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/AddressComp").Include(
                       "~/Scripts/ProfilePageGN/addressService.js",
                        "~/Scripts/ProfilePageGN/detailsService.js",
                    "~/Scripts/ProfilePageGN/addressPostController.js",
                    "~/Scripts/ProfilePageGN/detailsPostController.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/underscore.min.js",
            "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/bootbox.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/datatables/css/dataTables.bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/typeahead.css",
                      "~/Content/toastr.css",
                      "~/Content/font-awesome-4.7.0/css/font-awesome.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/ProfilePageGN").Include(
                  "~/Scripts/ProfilePageGN/experienceService.js",
                  "~/Scripts/ProfilePageGN/educationService.js",
                  "~/Scripts/ProfilePageGN/addressService.js",
                  "~/Scripts/ProfilePageGN/detailsService.js",
                  "~/Scripts/ProfilePageGN/experiencePostController.js",
                  "~/Scripts/ProfilePageGN/educationPostController.js",
                  "~/Scripts/ProfilePageGN/addressPostController.js",
                  "~/Scripts/ProfilePageGN/detailsPostController.js",
                  "~/Scripts/ProfilePageGN/deleteDialog.js",
                  "~/Scripts/ProfilePageGN/ajaxActionLink.js",
                  "~/Scripts/ProfilePageGN/educationTemplate.js",
                  "~/Scripts/ProfilePageGN/experienceTemplate.js",
                  "~/Scripts/ProfilePageGN/deleteListener.js",
                  "~/Scripts/ProfilePageGN/skillController.js",
                  "~/Scripts/ProfilePageGN/skillTemplate.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/HomePageAC").Include(
                "~/Scripts/HomePageAC/Services/networkService.js",
                "~/Scripts/HomePageAC/VariousClasses/FeedHubSubscriber.js",
                "~/Scripts/HomePageAC/Services/PostService2.js",
                "~/Scripts/HomePageAC/Controllers/textareacontroller.js",
                "~/Scripts/HomePageAC/Controllers/imageSelectionController.js",
                "~/Scripts/HomePageAC/Controllers/submitPostController.js",
                "~/Scripts/HomePageAC/Controllers/feedController.js",
                "~/Scripts/HomePageAC/Controllers/UserCardsController.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/cityCountry").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/ProfilePageGN/getCountriesAndCities.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/devProfileCompany").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/ProfilePageGN/getCompanies.js"
                ));
                bundles.Add(new ScriptBundle("~/bundles/GD").Include(
                "~/Scripts/GD/JobsPageController.js",
                "~/Scripts/GD/CompanyProfilePageController.js",
                "~/Scripts/GD/GetCountriesAndCities.js"
                ));
        }
    }
}