using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MaxWallet.scripts.dashboard.utils
{

    public class Result {
        public string status { get; set; }
        public decimal BTC { get; set; }
        public decimal USD { get; set; }
    }
    public static class ConvertBtc
    {
        public static async Task<double> GetValue(string CoinKind, long Value) {
            
            decimal BtcValue = Value / 100_000_000m;

            string url = "https://api.coinconvert.net/convert/btc/"+ CoinKind +"?amount=" + BtcValue.ToString("F8", CultureInfo.InvariantCulture);

            using (HttpClient client = new HttpClient()) {
                
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();
                
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Result>(jsonResponse);

                return Math.Round((double)result.USD, 2);
            }
        }
    }
}