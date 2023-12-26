namespace YZ.Calendar
{
    public class CalendarSystem
    {
        internal static string RestPath = "";
        internal static string FastivalPath = "";

        /// <summary>
        /// 设置休息日与调休日的json文件地址，推荐绝对路径
        /// </summary>
        public static string SetRestPath
        {
            set { RestPath = value; }
        }

        /// <summary>
        /// 设置节假日的json文件地址，推荐绝对路径
        /// </summary>
        public static string SetFastivalPath
        {
            set { FastivalPath = value; }
        }

        /// <summary>
        /// 阳历转农历无参数
        /// </summary>
        /// <returns>CalendarYZ今日的数据</returns>
        public static CalendarYZ SolarToLunarFun()
        {
            return CalendarCore.SolarToLunarCore(DateTime.Now);
        }

        /// <summary>
        /// 阳历转农历指定日期
        /// </summary>
        /// <returns>CalendarYZ今日的数据</returns>
        public static CalendarYZ SolarToLunarFun(int year,int month,int day)
        {
            return CalendarCore.SolarToLunarCore(new DateTime(year,month,day));
        }

        /// <summary>
        /// 农历转阳历指定日期，isLeapMonth表示是否闰月，默认false，可不填。
        /// </summary>
        /// <returns>CalendarYZ今日的数据</returns>
        public static CalendarYZ LunartoSolarFun(int y,int m,int d , bool isLeapMonth = false)
        {
            //参数区间1900.1.31~2100.12.1
           
            int day = LunarFunction.LMonthDays(y, m);

            //计算农历的时间差
            int offset = 0;

            for (int i = 1900; i < y; i++)
            {
                offset += LunarFunction.LYearDays(i);
            }
            bool isAdd = false;

            for (int i = 1; i < m; i++)
            {
                int leap = LunarFunction.LeapMonth(y);
                if (!isAdd)
                {
                    //处理闰月
                    if (leap <= i && leap > 0)
                    {
                        offset += LunarFunction.LeapDays(y);
                        isAdd = true;
                    }
                }
                offset += LunarFunction.LMonthDays(y, i);
            }
            //转换闰月农历 需补充该年闰月的前一个月的时差
            if (isLeapMonth)
            {
                offset += day;
            }

            //1900年农历正月一日的公历时间为1900年1月30日0时0分0秒(该时间也是本农历的最开始起始点)
            long stmap = -2203804800000;
            long ticks = (offset + d - 31) ;
            long tick2 = stmap / 86400000;
            long tick3 = (ticks + tick2)* 86400000;
            DateTime calObj = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                 .AddMilliseconds(tick3);
            int cY = calObj.Year;
            int cM = calObj.Month;
            int cD = calObj.Day;

            return SolarToLunarFun(cY, cM, cD);
        }
    }
}
