using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeersAPI.DTOs
{
    public class UpdateBeerDto
    {
        public string Title { get; set; }
        public decimal Volume { get; set; }
        public bool NonAlcohol { get; set; }
        public int DrankQuantity { get; set; }
    }
}
