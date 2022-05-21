using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.HolooEntity
{
    public class HolooSndList
    {
        public HolooSndList(int sanad_Code, string col_Code, string moien_Code, string tafzili_Code, double? bed, double? bes, string comment_Line)
        {
            Sanad_Code = sanad_Code;
            Col_Code = col_Code ;
            Moien_Code = moien_Code;
            Tafzili_Code = tafzili_Code;
            Bed = bed;
            Bes = bes;
            Comment_Line = comment_Line ;
            Show_Daftar = true;
            Joze = false;
            Actions = 0;
            OldSCode = 0;
            Bed_Arz = 0;
            Bes_Arz = 0;
            ArzId = 1;
            Money_Price = 1;
            Money_change = 1;
        }

        public int Sanad_Code { get; set; }
        public int Index { get; set; }
        public string Col_Code { get; set; }
        public string Moien_Code { get; set; }
        public string Tafzili_Code { get; set; }
        public Nullable<double> Bed { get; set; }
        public Nullable<double> Bes { get; set; }
        public Nullable<bool> Show_Daftar { get; set; }
        public Nullable<bool> Joze { get; set; }
        public Nullable<byte> Actions { get; set; }
        public string Comment_Line { get; set; }
        public int OldSCode { get; set; }
        public double Bed_Arz { get; set; }
        public double Bes_Arz { get; set; }
        public int ArzId { get; set; }
        public double Money_Price { get; set; }
        public double Money_change { get; set; }
    }
}
