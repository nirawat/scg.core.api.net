namespace Core.Api.Net.Business.Models.Configs
{
    public class EnvironmentModel
    {
        public GoogleMapApi GoogleMapApi { get; set; }
    }
    public class GoogleMapApi
    {
        public string BaseUrl { get; set; }
        public string BaseUrlPhoto { get; set; }
        public string BaseQuery { get; set; }
        public string ApiKey { get; set; }
        public string Type { get; set; }
    }

}