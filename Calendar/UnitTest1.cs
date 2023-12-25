using YZ.Calendar;

namespace Calendar.Tests
{
    [TestClass]
    public class SolarToLunarTests
    {
        [TestMethod]
        public void SolarToLunarFun_ShouldReturnLunarCalendar()
        {
            // 准备测试数据
            int year = 2023;
            int month = 11;
            int day = 11;

            // 创建 SolarToLunar 实例
            CalendarSystem SolarToLunar = new CalendarSystem();

            // 调用方法进行测试
            //CalendarCY result = SolarToLunar.SolarToLunarFun(year, month, day);
            CalendarCY result = SolarToLunar.LunartoSolarFun(year, month, day);
            Console.WriteLine(result);

            // 断言结果是否符合预期
            Assert.IsNotNull(result);
            Assert.AreEqual(2023, result.LYear);
            Assert.AreEqual(11, result.LMonth);
            Assert.AreEqual(11, result.LDay);
            Assert.AreEqual(2023, result.CYear);
            Assert.AreEqual(12, result.CMonth);
            Assert.AreEqual(23, result.CDay);
            Assert.AreEqual("兔", result.Animal);
            Assert.AreEqual("冬月", result.LMonthCn);
            Assert.AreEqual("十一", result.LDayCn);
            Assert.AreEqual("魔羯座", result.Astro);
            Assert.AreEqual("乙卯", result.GzDay);
            Assert.AreEqual("甲子", result.GzMonth);
            Assert.AreEqual("癸卯", result.GzYear);
            Assert.AreEqual(false, result.IsToday);
            Assert.AreEqual(6, result.NWeek);
            Assert.AreEqual("星期六", result.NcWeek);

        }
    }
}