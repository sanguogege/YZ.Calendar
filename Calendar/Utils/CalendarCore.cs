
namespace YZ.Calendar
{
    internal static class CalendarCore
    {
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
                Astro = Astro
            };
        }

        private static long DateTimeOffset(int year, int month, int day)
        {
            DateTime utcDateTime = DateTime.SpecifyKind(new DateTime(year, month, day), DateTimeKind.Utc);
            DateTimeOffset utcDateTimeOffset = new(utcDateTime);
            return utcDateTimeOffset.ToUnixTimeMilliseconds();
        }

    }
}
