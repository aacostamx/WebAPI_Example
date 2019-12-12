using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string Source { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [JsonIgnore]
        public DateTime Date { get; set; }
        public string date { get; set; }
        [JsonIgnore]
        public string strDate
        {
            get => date;
            set => date = Date.ToString(new CultureInfo("es-MX"));
        }
    }
}
