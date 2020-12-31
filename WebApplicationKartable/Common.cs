using System;
using System.Globalization;
using System.Text;

namespace WebApplicationKartable
{
    public class Common
    {
        public string persian_date_view()
        {
            StringBuilder strb = new StringBuilder();
            PersianCalendar pDate = new PersianCalendar();
            strb.Append(pDate.GetYear(DateTime.Now));
            strb.Append("/");
            strb.Append(pDate.GetMonth(DateTime.Now));
            strb.Append("/");
            strb.Append(pDate.GetDayOfMonth(DateTime.Now));
            strb.Append("   ");
            strb.Append(pDate.GetHour(DateTime.Now));
            strb.Append(":");
            strb.Append(pDate.GetMinute(DateTime.Now));
            return strb.ToString();
        }
        public string persian_date()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt2 = DateTime.Now;
            string year1 = pc.GetYear(dt2).ToString();
            string month1 = pc.GetMonth(dt2).ToString("d2");
            string day1 = pc.GetDayOfMonth(dt2).ToString("d2");
            return string.Concat(year1, "/", month1, "/", day1);
        }
        public string persian_date2()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt2 = DateTime.Now;
            string year1 = pc.GetYear(dt2).ToString();
            string month1 = pc.GetMonth(dt2).ToString("d2");
            string day1 = pc.GetDayOfMonth(dt2).ToString("d2");
            return string.Concat(day1, "/", month1, "/", year1);
        }
        public string persian_date2(string date)
        {
            if (string.IsNullOrEmpty(date)) return string.Empty;
            string year1 = date.Substring(0, 4);
            string month1 = date.Substring(5, 2);
            string day1 = date.Substring(8, 2);
            return string.Concat(day1, "/", month1, "/", year1);
        }
        public string year_ago()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt2 = DateTime.Now;
            string year1 = pc.GetYear(DateTime.Now.AddYears(-1)).ToString();
            string month1 = pc.GetMonth(dt2).ToString("d2");
            string day1 = pc.GetDayOfMonth(dt2).ToString("d2");
            return string.Concat(year1, "/", month1, "/", day1);
        }
        public string remove_cama(string inputString)
        {
            string find_cama = "";
            try
            {
                string[] Split;
                if (inputString.Contains("،"))
                    Split = inputString.Split(new Char[] { '،' });
                else
                    Split = inputString.Split(new Char[] { ',' });
                for (int i = 0; i < Split.Length; i++)
                    find_cama += Convert.ToString(Split[i]);
                return find_cama;
            }
            catch { return find_cama; }
        }
        public string first_this_year()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt2 = DateTime.Now;
            string year1 = pc.GetYear(dt2).ToString();
            string month1 = "01";
            string day1 = "01";
            return string.Concat(year1, "/", month1, "/", day1);
        }
        public string last_this_year()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt2 = DateTime.Now;
            string year1 = pc.GetYear(dt2).ToString();
            string month1 = "12";
            string day1 = "29";
            return string.Concat(year1, "/", month1, "/", day1);
        }
        public string _str;
        public string str
        {
            get
            {
                return _str;
            }
            set
            {
                int b = (value.Length) % 3;
                for (int i = value.Length; i >= b; i -= 3)
                {
                    try
                    {
                        if (i <= 3)
                            break;
                        value = value.Insert(i - 3, ",");
                    }
                    catch { }
                }
                _str = value;
            }
        }
    }
    public class ComboBox
    {
        public int Value { get; set; }
        public int Display { get; set; }
    }
}