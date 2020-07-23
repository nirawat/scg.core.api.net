using System;
using System.Collections.Generic;
using Core.Api.Net.Business.Models;
using Core.Api.Net.Business.Repository.Business;
namespace Core.Api.Net.Business.Services.Business
{
    public class GoogleMapApiService : IGoogleMapApiService
    {
        private readonly IGoogleMapApiRepository _IGoogleMapApiRepository;
        public GoogleMapApiService(IGoogleMapApiRepository IGoogleMapApiRepository)
        {
            _IGoogleMapApiRepository = IGoogleMapApiRepository;
        }

        public GoogleMapApiModels GetRestaurants(string Key1, string Key2, string Key3)
        {
            GoogleMapApiModels resp = new GoogleMapApiModels();
            try
            {
                resp = _IGoogleMapApiRepository.GetRestaurants(Key1, Key2, Key3);

                //Do Something Logic....

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