using System;
using System.Collections.Generic;
using Core.Api.Net.Business.Models;

namespace Core.Api.Net.Business.Repository.Business
{
    public interface IGoogleMapApiRepository
    {
        GoogleMapApiModels GetRestaurants(string Key1, string Key2, string Key3);
    }
}