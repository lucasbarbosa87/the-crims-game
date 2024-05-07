using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Entities
{
    public class EstablishmentEntity : AuditableEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public double Earnings { get; set; }
        public double SellPrice { get; set; }
        public double RentValue { get; set; }
        public int Limit { get; set; }
    }
}
