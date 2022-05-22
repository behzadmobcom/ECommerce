using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.HolooEntity
{
    public class HolooSanad
    {
        public HolooSanad(string comment)
        {
            Comment = comment;
            Sanad_Date = DateTime.Now;
            Sanad_Time = DateTime.Now;
            Sanad_Type = 1;
            DateUser = DateTime.Now.ToShortDateString();
            TimeUser = DateTime.Now.ToString("HH:mm");
            Endeditdate = DateTime.Now;
            UserCodeInc = 1;
            sanad_state = 0;
            Transfer_Recive_Snd = false;
            Transfer_Send_Snd = false;
            FCode_ChangeGold = 0;
            Sanad_Code_C2 = 0;
        }

        public int Sanad_Code { get; set; }
        public Nullable<int> Sanad_Code_C { get; set; }
        public Nullable<int> Sanad_Code_C2 { get; set; }
        public Nullable<System.DateTime> Sanad_Date { get; set; }
        public Nullable<System.DateTime> Sanad_Time { get; set; }
        public string Comment { get; set; }
        public Nullable<short> Sanad_Type { get; set; }
        public string DateUser { get; set; }
        public string TimeUser { get; set; }
        public Nullable<int> UserCodeInc { get; set; }
        public Nullable<System.DateTime> Endeditdate { get; set; }
        public short sanad_state { get; set; }
        public bool Transfer_Recive_Snd { get; set; }
        public bool Transfer_Send_Snd { get; set; }
        public int FCode_ChangeGold { get; set; }
    }
}
