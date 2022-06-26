using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiService.Model
{
    public class City
    {
        [Key]
        public int cityId { get; set; }
        public string cityName { get; set; }

        public DateTime AddTime { get; set; }
    }
}
