

using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MaxWallet.scripts.PosLog;

namespace MaxWallet.scripts.dashboard.utils
{
    public class AddressesInfo {

        [JsonPropertyName("address")]
        public string WalletAddress { get; set; }

        [JsonPropertyName("final_balance")]
        public long FinalBalance { get; set; }

        [JsonPropertyName("txs")]
        public List<Transation> Transations { get; set; } 
    }

    public class Transation {

        [JsonPropertyName("hash")]
        public string TransationHash { get; set;}

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("out")]
        public List<TransationOut> Out { get; set; }

        [JsonPropertyName("result")]
        public long Result { get; set; }
    }

    public class TransationOut {

        [JsonPropertyName("value")]
        public int Value { get; set;}

        [JsonPropertyName("addr")]
        public string Address { get; set; }
    }

    public static class GetBlockchainInfos
    {
        public static async Task<AddressesInfo> GetInfos(Wallet WalletData){

            string url = "https://blockchain.info/rawaddr/"+ WalletData.Address + "?limit=10";

            using (HttpClient client = new HttpClient()) {

                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<AddressesInfo>(jsonResponse);

                return result;

            }
        }
    }
}