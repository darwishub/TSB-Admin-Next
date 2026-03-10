using Newtonsoft.Json;
using System.Net;

namespace TheStartupBuddyV3.Models
{
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; } = null!;

        [JsonProperty("hostname")]
        public string Hostname { get; set; } = null!;

        [JsonProperty("city")]
        public string City { get; set; } = null!;

        [JsonProperty("region")]
        public string Region { get; set; } = null!;

        [JsonProperty("country_name")]
        public string Country { get; set; } = null!;

        [JsonProperty("loc")]
        public string Loc { get; set; } = null!;

        [JsonProperty("organisation")]
        public string Org { get; set; } = null!;

        [JsonProperty("postal")]
        public string Postal { get; set; } = null!;
    }

    public class IP2Country
    {
        public static IpInfo? GetUserCountryByIp(string? ip)
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string? info = new WebClient().DownloadString("https://api.ipdata.co/" + ip + "?api-key=f69baf599ad2adaecba0ed74a0f523ac51cea990f5e7d875aba61c14");
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                //RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                //ipInfo.Country = myRI1.EnglishName;
            }
            catch
            {
                ipInfo.Country = null;
            }

            return ipInfo;
        }
    }
}
