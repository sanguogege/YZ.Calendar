namespace YZ.Calendar;

public class CalendarYZ
{
    /// <summary>
    /// 农历年
    /// </summary>
    public int? LYear { get; set; }
    /// <summary>
    /// 农历月
    /// </summary>
    public int? LMonth { get; set; }
    /// <summary>
    /// 农历日
    /// </summary>
    public int? LDay { get; set; }
    /// <summary>
    /// 生肖
    /// </summary>
    public string? Animal { get; set; }
    /// <summary>
    /// 农历月中文
    /// </summary>
    public string? LMonthCn { get; set; }
    /// <summary>
    /// 农历日中文
    /// </summary>
    public string? LDayCn { get; set; }
    /// <summary>
    /// 阳历年
    /// </summary>
    public int? CYear { get; set; }
    /// <summary>
    /// 阳历月
    /// </summary>
    public int? CMonth { get; set; }
    /// <summary>
    /// 阳历日
    /// </summary>
    public int? CDay { get; set; }
    /// <summary>
    /// 干支年
    /// </summary>
    public string? GzYear { get; set; }
    /// <summary>
    /// 干支月
    /// </summary>
    public string? GzMonth { get; set; }
    /// <summary>
    /// 干支日
    /// </summary>
    public string? GzDay { get; set; }
    /// <summary>
    /// 干支时辰
    /// </summary>
    public string[]? GzHour { get; set; }
    /// <summary>
    /// 是否今日
    /// </summary>
    public bool? IsToday { get; set; }
    /// <summary>
    /// 是否闰月
    /// </summary>
    public bool? IsLeap { get; set; }
    /// <summary>
    /// 周几
    /// </summary>
    public int? NWeek { get; set; }
    /// <summary>
    /// 周几中文
    /// </summary>
    public string? NcWeek { get; set; }
    /// <summary>
    /// 节气
    /// </summary>
    public string? Term { get; set; }
    /// <summary>
    /// 星座
    /// </summary>
    public string? Astro { get; set; }
    /// <summary>
    /// 是否休息
    /// </summary>
    public bool? IsXiu { get; set; } = false;
    /// <summary>
    /// 是否调休上班
    /// </summary>
    public bool? IsBan { get; set; } = false;
    /// <summary>
    /// 今日的阳历节日
    /// </summary>
    public string[]? SolarFestival { get; set; }
    /// <summary>
    /// 今日的农历节日
    /// </summary>
    public string[]? LunatFestival { get; set; }
}

partial class ResBox
{
    /// <summary>
    /// 是否为休息日
    /// </summary>
    public bool IsXiu {  get; set; }
    /// <summary>
    /// 是否为调休日
    /// </summary>
    public bool IsBan {  get; set; }

}
