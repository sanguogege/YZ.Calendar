
using Newtonsoft.Json;

namespace YZ.Calendar
{
    internal static class CalendarCore
    {
        /// <summary>
        /// 转换核心
        /// </summary>
        /// <returns>CalendarYZ类型</returns>
        internal static CalendarYZ SolarToLunarCore(DateTime objData)
        {
            int i, leap, temp = 0, y, m, d;
            bool isToday = false;

            //修正ymd参数

            y = objData.Year;
            m = objData.Month;
            d = objData.Day;

            long offset = (DateTimeOffset(y, m, d) - DateTimeOffset(1900, 1, 31)) / 86400000;
            for (i = 1900; i < 2101 && offset > 0; i++)
            {
                temp = LunarFunction.LYearDays(i);
                offset -= temp;
            }

            if (offset < 0)
            {
                offset += temp;
                i--;
            }

            //是否今天
            DateTime isTodayObj = DateTime.Now;
            if (isTodayObj.Year == y && isTodayObj.Month == m && isTodayObj.Day == d)
            {
                isToday = true;
            }

            //星期几
            int nWeek = (int)objData.DayOfWeek;
            string cWeek = SolarFunction.ToChinaNum(nWeek);

            //数字表示周几顺应天朝周一开始的惯例
            if (nWeek == 0)
            {
                nWeek = 7;
            }

            //农历年
            int year = i;

            leap = LunarFunction.LeapMonth(i);
            bool isLeap = false;

            //效验闰月

            for (i = 1; i < 13 && offset > 0; i++)
            {
                //闰月
                if (leap > 0 && i == (leap + 1) && isLeap == false)
                {
                    --i;
                    isLeap = true;
                    temp = LunarFunction.LeapDays(year); //计算农历闰月天数
                }
                else
                {
                    temp = LunarFunction.LMonthDays(year, i); //计算农历普通月天数
                }
                //解除闰月
                if (isLeap == true && i == leap + 1)
                {
                    isLeap = false;
                }
                offset -= temp;
            }

            // 闰月导致数组下标重叠取反
            if (offset == 0 && leap > 0 && i == leap + 1)
            {
                if (isLeap)
                {
                    isLeap = false;
                }
                else
                {
                    isLeap = true;
                    --i;
                }
            }
            if (offset < 0)
            {
                offset += temp;
                --i;
            }

            //农历月
            int month = i;

            //农历日
            int day = (int)(offset + 1);
            //天干地支处理
            string gzY = LunarFunction.ToGanZhiYear(year);

            // 当月的两个节气
            int firstNode = SolarFunction.GetTerm(y, m * 2 - 1);//返回当月「节」为几日开始
            int secondNode = SolarFunction.GetTerm(y, m * 2);//返回当月「节」为几日开始

            // 依据12节气修正干支月
            string gzM = LunarFunction.ToGanZhi((y - 1900) * 12 + m + 11);
            if (d >= firstNode)
            {
                gzM = LunarFunction.ToGanZhi((y - 1900) * 12 + m + 12);
            }

            //传入的日期的节气与否,有则值，没有则为null
            string? Term = null;
            if (firstNode == d)
            {
                Term = SolarFunction.GetSolarTerm(m * 2 - 2);
            }
            if (secondNode == d)
            {
                Term = SolarFunction.GetSolarTerm(m * 2 - 1);
            }

            //日柱 当月一日与 1900/1/1 相差天数
            int dayCyclical = (int)(DateTimeOffset(y, m, 1) / 86400000 + 25567 + 10);
            string gzD = LunarFunction.ToGanZhi(dayCyclical + d - 1);

            // 二十四小时对应的天干地支
            string[] gzH = LunarFunction.ToHourGanZhi(dayCyclical + d - 1);

            //该日期所属的星座
            string Astro = SolarFunction.ToAstro(m, d);

            //获取节日
            string[]? SolarFest = null;
            string[]? LunatFest = null;
            if (CalendarSystem.FastivalPath!= "")
            {
                SolarFest = GetFastival(m,d, CalendarSystem.FastivalPath);
                LunatFest = GetFastival(month, day, CalendarSystem.FastivalPath,false);
            }

            //获取当日是否休息
            bool IsXiu = false;
            bool IsBan = false;
            if (CalendarSystem.RestPath!= "")
            {
                ResBox ResBox = GetBanXiu(y, m, d, CalendarSystem.RestPath);
                IsXiu = ResBox.IsXiu;
                IsBan = ResBox.IsBan;
            }


            return new CalendarYZ()
            {
                LYear = year,
                LMonth = month,
                LDay = day,
                Animal = SolarFunction.GetAnimal(year),
                LMonthCn = (isLeap ? "\u95f0" : "") + LunarFunction.ToChinaMonth(month),
                LDayCn = LunarFunction.ToChinaDay(day),
                CYear = y,
                CMonth = m,
                CDay = d,
                GzYear = gzY,
                GzMonth = gzM,
                GzDay = gzD,
                GzHour = gzH,
                IsToday = isToday,
                IsLeap = isLeap,
                NWeek = nWeek,
                NcWeek = "\u661f\u671f" + cWeek,
                Term = Term,
                Astro = Astro,
                SolarFestival = SolarFest,
                LunatFestival = LunatFest,
                IsBan = IsBan,
                IsXiu = IsXiu
            };
        }

        /// <summary>
        /// 获取指定时间的Utc毫秒值。
        /// </summary>
        /// <returns>long型整数</returns>
        private static long DateTimeOffset(int year, int month, int day)
        {
            DateTime utcDateTime = DateTime.SpecifyKind(new DateTime(year, month, day), DateTimeKind.Utc);
            DateTimeOffset utcDateTimeOffset = new(utcDateTime);
            return utcDateTimeOffset.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 获取阳历节日和农历节日，IsSolar默认为true,表示阳历，false表示农历
        /// </summary>
        /// <returns>字符串数组</returns>
        private static string[]? GetFastival( int month, int day, string jsonPath, bool IsSolar = true)
        {
            string _target = AddZero(month) + AddZero(day);
            string jsonContent = File.ReadAllText(jsonPath);
            var fastivalData = JsonConvert.DeserializeObject<dynamic>(jsonContent);

            if (IsSolar&& fastivalData?.sFtv.ContainsKey(_target))
            {
                return fastivalData?.sFtv[_target].ToObject<string[]>();
            }
            else if (!IsSolar && fastivalData?.lFtv.ContainsKey(_target))
            {
                return fastivalData?.lFtv[_target].ToObject<string[]>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取休息日和调休日
        /// </summary>
        /// <returns>ResBox对象returns>
        private static ResBox GetBanXiu(int year, int month, int day, string jsonPath)
        {
            string _year = year.ToString();
            string _target = AddZero(month) + AddZero(day);

            string jsonContent = File.ReadAllText(jsonPath);
            var restData = JsonConvert.DeserializeObject<dynamic>(jsonContent);

            if (restData?.ContainsKey(_year))
            {
                var xiuBox = restData?[_year].xiu.ToString();
                var banBox = restData?[_year].ban.ToString();
                return new ResBox
                {
                    IsXiu = xiuBox?.Contains(_target),
                    IsBan = banBox?.Contains(_target)
                };
            }

            return new ResBox
            {
                IsXiu = false,
                IsBan = false
            };

        }

        /// <summary>
        /// 为小于10的数前面加0
        /// </summary>
        /// <returns>(01、29、30)</returns>
        private static string AddZero(int num)
        {
            return num < 10 ? "0" + num.ToString() : num.ToString();
        }
    }
}
