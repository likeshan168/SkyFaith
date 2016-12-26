using System;
using System.Collections.Generic;

namespace DPDModel.Models
{
    public class TMSTrackInfo
    {
        public bool ack { get; set; }
        public IEnumerable<TrackData> data { get; set; }
    }

    public class TrackData
    {
        public string businessId { get; set; }
        public string consigneeCountry { get; set; }
        public string productKindName { get; set; }
        public string referenceNumber { get; set; }
        public string trackContent { get; set; }
        public string trackDate { get; set; }
        public string trackLocation { get; set; }
        public string trackSignperson { get; set; }
        public string trackingNumber { get; set; }
        public IEnumerable<trackDetail> trackDetails { get; set; }

    }

    public class trackDetail
    {
        public string business_id { get; set; }
        public string system_id { get; set; }
        public string track_content { get; set; }
        public string track_createdate { get; set; }
        public DateTime track_date { get; set; }
        public string track_id { get; set; }
        public string track_kind { get; set; }
        public string track_location { get; set; }
        public string track_signdate { get; set; }
        public string track_signperson { get; set; }

    }
}
