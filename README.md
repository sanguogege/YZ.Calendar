# 万年历农历阳历转换插件 YZ.Calendar
[![NuGet version (YZ.Calendar)](https://img.shields.io/nuget/v/YZ.Calendar.svg?style=flat-square)](https://www.nuget.org/packages/YZ.Calendar/)

- A plugin that can convert Chinese lunar and solar calendars. 
- 一个能转换中国农历和阳历的插件。


```
using YZ.Calendar 
```

 ```C#
 /// <summary>
/// 设置节假日的json文件地址，推荐绝对路径，优先设置。
/// 不填返回null
/// </summary>
CalendarSystem.SetFastivalPath

/// <summary>
/// 设置休息日与调休日的json文件地址，推荐绝对路径，优先设置。
/// 不填返回false
/// </summary>
CalendarSystem.SetRestPath

 //阳历转农历，阳历年月日
CalendarYZ result2 = CalendarSystem.SolarToLunarFun(int year, int month, int day);
 //阳历转农历，农历年月日，isLeapMonth表示是否润月，默认为false
CalendarYZ result2 = CalendarSystem.LunartoSolarFun(int LYear, int LMonth, int LDay, bool isLeapMonth);


 ```

#### 节日格式 json
```
{
    "sFtv": {
        "1001": [
            "国庆节",
            "国际音乐日",
            "世界微笑日",
            "国际老人节"
        ],
    },
    "lFtv":{
        "0101": [
            "春节"
        ]
    }
}
```
#### 休息日与调休日 json
```
{
    "2020": {
        "xiu": ["0101","0124","0125"],
        "ban": [ "0119","0201","0426"]
    },
    "2021":{
        "xiu": ["0101","0124","0125"],
        "ban": [ "0119","0201","0426"]
    }
}
```


 ## 其他方法
- 阳历的方法

 ```C#
/// <summary>
/// 返回公历(!)y年m月的天数
/// </summary>
/// <returns>(28、29、30、31)</returns>
SolarFunction.SolarDays(int year,int month)

/// <summary>
/// 返回公历(!)y年m月的第一天是星期几
/// </summary>
/// <returns>(1-7)</returns>
SolarFunction.SolarFirstWeek(int year,int month)

/// <summary>
/// 公历月、日判断所属星座
/// </summary>
/// <returns>十二星座</returns>
SolarFunction.ToAstro(int month,int day)

/// <summary>
/// 传入公历(!)y年获得该年第n个节气的公历日期
/// </summary>
/// <returns>该月的day</returns>
SolarFunction.GetTerm(int year,int month)

/// <summary>
/// 年份转生肖[!仅能大致转换]  精确划分生肖分界线是“立春”
/// </summary>
/// <returns>"鼠","牛","虎","兔","龙","蛇","马","羊","猴","鸡","狗","猪"</returns>
SolarFunction.GetAnimal(int year)

 /// <summary>
 /// 数字转中文(1-10)0表示日
 /// </summary>
 /// <returns>'日','一','二','三','四','五','六','七','八','九','十'</returns>
SolarFunction.ToChinaNum(int num)

 ```

 - 农历方法 

 ```C#
 /// <summary>
/// 返回农历y年一整年的总天数
/// </summary>
/// <returns>一整年的总天数</returns>
 LunarFunction. LYearDays(int year)

 /// <summary>
/// 返回农历y年闰月是哪个月；若y年没有闰月 则返回0
/// </summary>
/// <returns>(0-12)</returns>
 LunarFunction.LeapMonth(int year)

 /// <summary>
/// 返回农历y年闰月的天数 若该年没有闰月则返回0
/// </summary>
/// <returns>(0、29、30)</returns>
 LunarFunction.LeapDays(int year)

   /// <summary>
  /// 返回农历y年m月（非闰月）的总天数，计算m为闰月时的天数请使用leapDays方法
  /// </summary>
  /// <returns>(29、30)</returns>
 LunarFunction.LMonthDays(int year,int month)

/// <summary>
/// 传入农历数字月份返回汉语通俗表示法（1-12）
/// </summary>
/// <returns>'正','二','三','四','五','六','七','八','九','十','冬','腊'</returns>
 LunarFunction.ToChinaMonth(int month)

   /// <summary>
  /// 传入农历日期数字返回汉字表示法
  /// </summary>
  /// <returns>汉字中文日期：初一，初十','廿','卅'</returns>
 LunarFunction.ToChinaDay(int day)
 ```



