﻿namespace Ecommerce.Entities.HolooEntity
{
    public class HolooSanad
    {
        public HolooSanad(string comment)
        {
            Comment = comment;
            Sanad_Date = DateTime.Now.Date;
            Sanad_Time = new DateTime(1900,1,1, DateTime.Now.ToLocalTime().Hour, DateTime.Now.ToLocalTime().Minute, DateTime.Now.ToLocalTime().Second);
            Sanad_Type = 1;
            DateUser = DateTime.Now.ToString("yyyy/MM/dd");
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
        public Nullable<DateTime> Sanad_Date { get; set; }
        public Nullable<DateTime> Sanad_Time { get; set; }
        public string Comment { get; set; }
        public Nullable<short> Sanad_Type { get; set; }
        public string DateUser { get; set; }
        public string TimeUser { get; set; }
        public Nullable<int> UserCodeInc { get; set; }
        public Nullable<DateTime> Endeditdate { get; set; }
        public short sanad_state { get; set; }
        public bool Transfer_Recive_Snd { get; set; }
        public bool Transfer_Send_Snd { get; set; }
        public int FCode_ChangeGold { get; set; }
    }
}
