
namespace YZ.Calendar
{
    public static class LunarFunction
    {
        /// <summary>
        /// 返回农历y年一整年的总天数
        /// </summary>
        /// <returns>一整年的总天数</returns>
        public static int LYearDays(int year)
        {
            int sum = 348;
            for (int i = 0x8000; i > 0x8; i >>= 1)
            {
                sum += (((CalendarData.LunarInfo[year - 1900] & i) == 0) ? 0 : 1);
            }
            return (sum + LeapDays(year));
        }

        /// <summary>
        /// 返回农历y年闰月是哪个月；若y年没有闰月 则返回0
        /// </summary>
        /// <returns>(0-12)</returns>
        public static int LeapMonth(int year)
        {
            return CalendarData.LunarInfo[year - 1900] & 0xf;
        }

        /// <summary>
        /// 返回农历y年闰月的天数 若该年没有闰月则返回0
        /// </summary>
        /// <returns>(0、29、30)</returns>
        public static int LeapDays(int year)
        {
            if (LeapMonth(year) != 0)
            {
                return ((CalendarData.LunarInfo[year - 1900] & 0x10000) != 0) ? 30 : 29;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 返回农历y年m月（非闰月）的总天数，计算m为闰月时的天数请使用leapDays方法
        /// </summary>
        /// <returns>(29、30)</returns>
        public static int LMonthDays(int year, int month)
        {
            return ((CalendarData.LunarInfo[year - 1900] & (0x10000 >> month)) != 0) ? 30 : 29;
        }

        /// <summary>
        /// 传入农历数字月份返回汉语通俗表示法
        /// </summary>
        /// <returns>'正','一','二','三','四','五','六','七','八','九','十','冬','腊'</returns>
        public static string ToChinaMonth(int month)
        {
            string s = CalendarData.nStr3[month - 1];
            s += "\u6708";
            return s;
        }

        /// <summary>
        /// 传入农历日期数字返回汉字表示法
        /// </summary>
        /// <returns>汉字中文日期：初一，初十','廿','卅'</returns>
        public static string ToChinaDay(int day)
        {
            if (day == 10)
            {
                return "\u521d\u5341";
            }
            else if (day == 20)
            {
                return "\u4e8c\u5341";
            }
            else if (day == 30)
            {
                return "\u4e09\u5341";
            }
            else
            {
                return CalendarData.nStr2[(int)Math.Floor((double)(day / 10))] + CalendarData.nStr1[day % 10];
            }
        }

        /// <summary>
        /// 农历年份转换为干支纪年
        /// </summary>
        /// <returns>干支纪年</returns>
        internal static string ToGanZhiYear(int lYear)
        {
            int ganKey = (lYear - 3) % 10;
            int zhiKey = (lYear - 3) % 12;
            if (ganKey == 0) ganKey = 10;
            if (zhiKey == 0) zhiKey = 12;

            return CalendarData.Gan[ganKey - 1] + CalendarData.Zhi[zhiKey - 1];
        }

        /// <summary>
        /// 传入offset偏移量返回干支
        /// </summary>
        /// <returns>干支</returns>
        internal static string ToGanZhi(int offset)
        {
            return CalendarData.Gan[offset % 10] + CalendarData.Zhi[offset % 12];
        }

        /// <summary>
        /// 传入offset偏移量返回时辰干支
        /// </summary>
        /// <returns>十二时辰干支字符串数组</returns>
        internal static string[] ToHourGanZhi(int offset)
        {
            string gzDtr = CalendarData.GanZhiHour[offset % 10];
            return gzDtr.Split("-");
        }


    }
}

