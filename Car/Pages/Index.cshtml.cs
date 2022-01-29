using System;
using System.Collections.Generic;

using System.IO;

using System.Net.Http;
using System.Net.Http.Headers;

using CarWebApp.Models;
using DataAccess.Utilities;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using BE = BusinessEntities;


namespace Car.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<BE.Car> lstCars = new List<BE.Car>();
        private readonly IOptions<SettingsModel> _appSettings;
        public IndexModel(ILogger<IndexModel> logger, IOptions<SettingsModel> app)
        {
            _logger = logger;
            _appSettings = app;
        }
        public void OnGet()
        {
            try
            {
                var filePath = Utilities.GetPath(@"Data\VehicleVins.txt");
                using (StreamReader r = new StreamReader(filePath))
                {
                    string vehicleVins = r.ReadToEnd();
                    var vins = vehicleVins.Split(',');
                    foreach (var vin in vins)
                    {
                        string url = _appSettings.Value.Url + vin + "?format=json";
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var tmp = client.GetAsync(url).Result;
                        if (tmp.IsSuccessStatusCode)
                        {
                            var response = tmp.Content.ReadAsStringAsync().Result;
                            dynamic obj = JsonConvert.DeserializeObject<dynamic>(response);
                            foreach (var res in ((IEnumerable<dynamic>)obj.Results))
                            {
                                var car = new BE.Car
                                {
                                    Manufacturer = res.Manufacturer,
                                    Make = res.Make,
                                    Model = res.Model,
                                    ModelYear = res.ModelYear
                                };
                                lstCars.Add(car);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //log and handle the error
            }
        }
    }
}
