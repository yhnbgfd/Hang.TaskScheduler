using System;
using System.Collections.Generic;

namespace Hang.TaskScheduler.Base
{
    public class Cron
    {
        private string _cron;
        public List<int> Minute { get; set; }
        public List<int> Hour { get; set; }
        public List<int> DayOfMonth { get; set; }
        public List<int> Month { get; set; }
        public List<int> DayOfWeek { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cron"></param>
        public Cron(string cron)
        {
            _cron = cron;

            var spl = _cron.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (spl.Length == 5)
            {
                ParseMinute(spl[0]);
                ParseHour(spl[1]);
                ParseDayOfMonth(spl[2]);
                ParseMonth(spl[3]);
                ParseDayOfWeek(spl[4]);
            }
            else
            {
                throw new ArgumentException("Cron Err");
            }
        }

        /// <summary>
        /// 配置时间是否符合Cron
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool IsMatch(DateTime dateTime)
        {
            if (Minute.Contains(dateTime.Minute)
               && Hour.Contains(dateTime.Hour)
               && DayOfMonth.Contains(dateTime.Day)
               && Month.Contains(dateTime.Month)
               && DayOfWeek.Contains((int)dateTime.DayOfWeek))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ParseMinute(string minuteCron)
        {
            Minute = new List<int>();
            foreach (var item in minuteCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        Minute.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 60; i++)
                    {
                        if (i % divisor == 0)
                        {
                            Minute.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 60; i++)
                    {
                        Minute.Add(i);
                    }
                }
                else
                {
                    Minute.Add(int.Parse(item));
                }
            }
        }
        private void ParseHour(string hourCron)
        {
            Hour = new List<int>();
            foreach (var item in hourCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        Hour.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 24; i++)
                    {
                        if (i % divisor == 0)
                        {
                            Hour.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 24; i++)
                    {
                        Hour.Add(i);
                    }
                }
                else
                {
                    Hour.Add(int.Parse(item));
                }
            }
        }
        private void ParseDayOfMonth(string dayOfMonthCron)
        {
            DayOfMonth = new List<int>();
            foreach (var item in dayOfMonthCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        DayOfMonth.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i % divisor == 0)
                        {
                            DayOfMonth.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        DayOfMonth.Add(i);
                    }
                }
                else
                {
                    DayOfMonth.Add(int.Parse(item));
                }
            }
        }
        private void ParseMonth(string monthCron)
        {
            Month = new List<int>();
            foreach (var item in monthCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        Month.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 12; i++)
                    {
                        if (i % divisor == 0)
                        {
                            Month.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Month.Add(i);
                    }
                }
                else
                {
                    Month.Add(int.Parse(item));
                }
            }
        }
        private void ParseDayOfWeek(string dayOfWeekCron)
        {
            DayOfWeek = new List<int>();
            foreach (var item in dayOfWeekCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        DayOfWeek.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 7; i++)
                    {
                        if (i % divisor == 0)
                        {
                            DayOfWeek.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        DayOfWeek.Add(i);
                    }
                }
                else
                {
                    DayOfWeek.Add(int.Parse(item));
                }
            }
        }
    }
}
