using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Dtos
{
    public class EstablishmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public double Earnings { get; set; }
        public double SellPrice { get; set; }
        public double RentValue { get; set; }
        public int Limit { get; set; }
    }
}
