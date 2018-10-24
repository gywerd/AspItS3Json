using As3jBizz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace As3jServices
{
    public class ExchangeAPI
    {
        #region Fields
        private const string APP_ID = "4e3fd7f40537fe7b10f305007684b7a3"; //Emil Api
        private const int MAX_FORECAST_DAYS = 5;
        private HttpClient client;
        private Label weather;
        #endregion

        #region Constructors
        public ExchangeAPI(Label weatherName)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://openexchangerates.org/api/");
            weather = weatherName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public async void GetCityNameAsync()
        {
            List<string> tempLoc = new List<string>();
            tempLoc.Add("Vejle");
            while (true)
            {
                try
                {
                    foreach (string location in tempLoc)
                    {
                        string query = $"weather?q={location}&mode=json&appid={APP_ID}";
                        HttpResponseMessage responseMessage = await client.GetAsync(query);

                        string responseStream = await responseMessage.Content.ReadAsStringAsync();

                        GettingExchangeRates.Root weatherRoot = JsonConvert.DeserializeObject<GettingExchangeRates.Root>(responseStream);
                        double temp = weatherRoot.Main.Temp - 273;
                        weather.Content = $"{location} Temperatur er lige nu {temp} °C";
                        await Task.Delay(11000);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        #endregion
    }
}
