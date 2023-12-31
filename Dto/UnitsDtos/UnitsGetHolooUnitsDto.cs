﻿using Ecommerce.Entities;
using System.ComponentModel.DataAnnotations;

namespace Dto.UnitsDtos
{
    public class UnitsGetHolooUnitsDto
    {
        [Key] public int Unit_Code { get; set; }

        public string Unit_Name { get; set; }
        public double Unit_Few { get; set; }
        public double Ayar { get; set; }
        public short Vahed_Vazn { get; set; }

        public static implicit operator Unit(UnitsGetHolooUnitsDto holooUnit)
        {
            return new Unit
            {
                Name = holooUnit.Unit_Name,
                Few = holooUnit.Unit_Few,
                UnitCode = holooUnit.Unit_Code,
                UnitWeight = holooUnit.Vahed_Vazn,
                assay = holooUnit.Ayar
            };
        }
    }
}
