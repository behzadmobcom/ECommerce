using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto.UnitsDtos
{
    public class UnitsGetDto
    {
        public string Name { get; set; }

       
        public double Few { get; set; }

        public int? UnitCode { get; set; }

        public double? assay { get; set; }

        public short? UnitWeight { get; set; }

        //ForeignKey
        [JsonIgnore] public virtual ICollection<Price>? Prices { get; set; }

        public int? HolooCompanyId { get; set; } = 1;

        public virtual HolooCompany? HolooCompany { get; set; }
    }
}
