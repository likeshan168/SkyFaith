using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DPDModel.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData("http://localhost:8080/trackIndex.htm");
            string result = Encoding.UTF8.GetString(bytes);

            IEnumerable<TMSTrackInfo> infos = JsonConvert.DeserializeObject<IEnumerable<TMSTrackInfo>>(result);

            foreach (TMSTrackInfo item in infos)
            {
                Console.WriteLine(item.ack);
                foreach (var data in item.data)
                {
                    Console.WriteLine(data.trackingNumber);
                    foreach (var detail in data.trackDetails)
                    {
                        Console.WriteLine(detail.track_location);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
