using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Core.Api.Net.Business.Helpers.Environment;
using Core.Api.Net.Business.Models.Configs;
using Core.Api.Net.Business.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Text;

namespace Core.Api.Net.Business.Repository.Business
{
    public class GoogleMapApiRepository : IGoogleMapApiRepository
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private readonly IEnvironmentConfigs _IEnvironmentConfigs;
        private EnvironmentModel _EnvironmentModel;
        public GoogleMapApiRepository(IHttpContextAccessor IHttpContextAccessor, IEnvironmentConfigs IEnvironmentConfigs)
        {
            _IEnvironmentConfigs = IEnvironmentConfigs;
            _EnvironmentModel = _IEnvironmentConfigs.GetEnvironmentSetting();
        }


        public GoogleMapApiModels GetRestaurants(string Key1, string Key2, string Key3)
        {
            try
            {
                string _baseQuery = Encoding.UTF8.GetString(Convert.FromBase64String(_EnvironmentModel.GoogleMapApi.BaseQuery));

                string _filter_map = string.Format(_baseQuery, Key1, Key2, Key3);

                var client = new RestClient(string.Format(_EnvironmentModel.GoogleMapApi.BaseUrl, _filter_map));

                client.Timeout = -1;

                var request = new RestRequest(Method.GET);

                IRestResponse response = client.Execute(request);

                GoogleMapApiModels resp = JsonConvert.DeserializeObject<GoogleMapApiModels>(response.Content);

                if (resp.results != null && resp.results.Count > 0)
                {
                    string _baseUrlPhoto = Encoding.UTF8.GetString(Convert.FromBase64String(_EnvironmentModel.GoogleMapApi.BaseUrlPhoto));

                    foreach (var i in resp.results)
                    {
                        if (i.photos != null && i.photos.Count > 0)
                        {
                            foreach (var ii in i.photos)
                            {
                                string _urlPhoto = string.Format(_baseUrlPhoto, ii.photo_reference, _EnvironmentModel.GoogleMapApi.ApiKey);
                                ii.photo_url = _urlPhoto;
                            }

                        }
                    }
                }


                return resp;
            }
            catch (Exception ex)
            {
                //Can Write Logs.
                Console.WriteLine(ex.Message);
            }
            return null;
        }


    }
}