using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace mxm.api.Models
{
    public class StudentProfileDto
    {
        public StudentProfileDto()
        {
            Documents = new List<DocumentDto>();
        }

        public int UserId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        [JsonIgnore]
        public DateTime DateofBirth { get; set; }
        [JsonIgnore]
        public string intAgeage { get; set; }
        //[JsonIgnore]
        public string age
        {
            get => intAgeage = (DateTime.Today.AddTicks(-DateofBirth.Ticks).Year - 1).ToString() + " AÑOS";
            //set => dateofBirth = DateofBirth.ToString("dd/MM/yyyy");
            set => intAgeage = (DateTime.Today.AddTicks(-DateofBirth.Ticks).Year - 1).ToString() + " AÑOS";
        }

        public UserProfileDto User { get; set; }
        public IList<DocumentDto> Documents { get; set; }

    }
}
