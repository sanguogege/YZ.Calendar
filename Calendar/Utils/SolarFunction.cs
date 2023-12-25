
namespace YZ.Calendar
{
    public static class SolarFunction
    {
        /// <summary>
        /// 返回公历(!)y年m月的天数
        /// </summary>
        /// <returns>(28、29、30、31)</returns>
        public static int SolarDays(int year,int month)
        {
            if (month == 2)
            {
                //2月份的闰平规律测算后确认返回28或29
                return (year % 4 == 0 && year % 100 != 0) || year % 400 == 0 ? 29 : 28;
            }
            else
            {
                return CalendarData.SolarMonth[month-1];
            }
        }

        /// <summary>
        /// 返回公历(!)y年m月的第一天是星期几
        /// </summary>
        /// <returns>(1-7)</returns>
        public static int SolarFirstWeek(int year,int month)
        {
            DateTime solarDate = new(year, month, 1, 0, 0, 0, 0);
            return (int)solarDate.DayOfWeek;
        }

        /// <summary>
        /// 公历月、日判断所属星座
        /// </summary>
        /// <returns>十二星座</returns>
        public static string ToAstro(int month,int day)
        {
            string s = "\u9b54\u7faf\u6c34\u74f6\u53cc\u9c7c\u767d\u7f8a\u91d1\u725b\u53cc\u5b50\u5de8\u87f9\u72ee\u5b50\u5904\u5973\u5929\u79e4\u5929\u874e\u5c04\u624b\u9b54\u7faf";
            int[] arr = [20, 19, 21, 21, 21, 22, 23, 23, 23, 23, 22, 22];
            return string.Concat(s.AsSpan(month * 2 - (day < arr[month - 1] ? 2 : 0), 2), "\u5ea7");
        }

        /// <summary>
        /// 传入公历(!)y年获得该年第n个节气的公历日期
        /// </summary>
        /// <returns>该月的day</returns>
        public static int GetTerm(int year,int month)
        {
            string _table = CalendarData.TermInfo[year - 1900];
            string[] _info = [
                Convert.ToInt32(_table.Substring(0,5),16).ToString(),
                Convert.ToInt32(_table.Substring(5,5),16).ToString(),
                Convert.ToInt32(_table.Substring(10, 5),16).ToString(),
                Convert.ToInt32(_table.Substring(15, 5),16).ToString(),
                Convert.ToInt32(_table.Substring(20, 5),16).ToString(),
                Convert.ToInt32(_table.Substring(25, 5),16).ToString(),
            ];
            string[] _calday = [
                _info[0][..1],
                _info[0].Substring(1, 2),
                _info[0].Substring(3, 1),
                _info[0].Substring(4, 2),

                _info[1].Substring(0, 1),
                _info[1].Substring(1, 2),
                _info[1].Substring(3, 1),
                _info[1].Substring(4, 2),

                _info[2].Substring(0, 1),
                _info[2].Substring(1, 2),
                _info[2].Substring(3, 1),
                _info[2].Substring(4, 2),

                _info[3].Substring(0, 1),
                _info[3].Substring(1, 2),
                _info[3].Substring(3, 1),
                _info[3].Substring(4, 2),

                _info[4].Substring(0, 1),
                _info[4].Substring(1, 2),
                _info[4].Substring(3, 1),
                _info[4].Substring(4, 2),

                _info[5].Substring(0, 1),
                _info[5].Substring(1, 2),
                _info[5].Substring(3, 1),
                _info[5].Substring(4, 2),
            ];
            return int.Parse(_calday[month - 1]);
        }

        /// <summary>
        /// 年份转生肖[!仅能大致转换]  精确划分生肖分界线是“立春”
        /// </summary>
        /// <returns>"鼠","牛","虎","兔","龙","蛇","马","羊","猴","鸡","狗","猪"</returns>
        public static string GetAnimal(int year)
        {
            return CalendarData.Animals[(year - 4) % 12];
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <returns>'日','一','二','三','四','五','六','七','八','九','十'</returns>
        public static string ToChinaNum(int num)
        {
            return CalendarData.nStr1[num];
        }

        /// <summary>
        /// 获取24节气的方法
        /// </summary>
        /// <returns>24节气</returns>
        internal static string GetSolarTerm(int num)
        {
            return CalendarData.SolarTerm[num];
        }
    }
}
