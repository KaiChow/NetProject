namespace Net6.WebApi
{
    /// <summary>
    /// 天气的类
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
       /// <summary>
       /// 提示
       /// </summary>
        public string? Summary { get; set; }
    }
}