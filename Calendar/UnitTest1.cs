using YZ.Calendar;

namespace Calendar.Tests
{
    [TestClass]
    public class SolarToLunarTests
    {
        [TestMethod]
        public void SolarToLunarFun_ShouldReturnLunarCalendar()
        {
            // ׼����������
            int year = 2023;
            int month = 11;
            int day = 11;

            // ���� SolarToLunar ʵ��
            CalendarSystem SolarToLunar = new CalendarSystem();

            // ���÷������в���
            //CalendarCY result = SolarToLunar.SolarToLunarFun(year, month, day);
            CalendarCY result = SolarToLunar.LunartoSolarFun(year, month, day);
            Console.WriteLine(result);

            // ���Խ���Ƿ����Ԥ��
            Assert.IsNotNull(result);
            Assert.AreEqual(2023, result.LYear);
            Assert.AreEqual(11, result.LMonth);
            Assert.AreEqual(11, result.LDay);
            Assert.AreEqual(2023, result.CYear);
            Assert.AreEqual(12, result.CMonth);
            Assert.AreEqual(23, result.CDay);
            Assert.AreEqual("��", result.Animal);
            Assert.AreEqual("����", result.LMonthCn);
            Assert.AreEqual("ʮһ", result.LDayCn);
            Assert.AreEqual("ħ����", result.Astro);
            Assert.AreEqual("��î", result.GzDay);
            Assert.AreEqual("����", result.GzMonth);
            Assert.AreEqual("��î", result.GzYear);
            Assert.AreEqual(false, result.IsToday);
            Assert.AreEqual(6, result.NWeek);
            Assert.AreEqual("������", result.NcWeek);

        }
    }
}