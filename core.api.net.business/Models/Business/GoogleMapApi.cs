using System.Collections.Generic;

namespace Core.Api.Net.Business.Models
{
    public class GoogleMapApiFilterModels
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
        public string key3 { get; set; }
    }

    public class GoogleMapApiModels
    {
        public string[] html_attributions { get; set; }
        public string next_page_token { get; set; }
        public IList<RestaurantModels> results { get; set; }
        public string status { get; set; }
    }
    public class RestaurantModels
    {
        public string business_status { get; set; }
        public string formatted_address { get; set; }
        //public GeometryModels geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        //public OpenHouseModels opening_hours { get; set; }
        public IList<PhotoModels> photos { get; set; }
        // public string place_id { get; set; }
        // public PlusCodeModels plus_code { get; set; }
        // public decimal rating { get; set; }
        // public string reference { get; set; }
        // public string[] types { get; set; }
        // public int user_ratings_total { get; set; }

    }

    public class PlusCodeModels
    {
        public string compound_code { get; set; }
        public string global_code { get; set; }
    }

    public class PhotoModels
    {
        public decimal width { get; set; }
        public decimal height { get; set; }
        //public string[] html_attributions { get; set; }
        public string photo_reference { get; set; }
        public string photo_url { get; set; }

    }

    public class OpenHouseModels
    {
        public bool open_now { get; set; }

    }

    public class GeometryModels
    {
        public GeometryLocationModels location { get; set; }
        public GeometryViewportModels viewport { get; set; }

    }

    public class GeometryLocationModels
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }

    }

    public class GeometryViewportModels
    {
        public GeometryViewportNortheastModels northeast { get; set; }
        public GeometryViewportSouthwestModels southwest { get; set; }
    }

    public class GeometryViewportNortheastModels
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }

    }

    public class GeometryViewportSouthwestModels
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }

    }


}