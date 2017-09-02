using System;
using System.Collections.Generic;

namespace Hang.TaskScheduler.Base
{
    public class Cron
    {
        private List<int> _minute;
        private List<int> _hour;
        private List<int> _dayOfMonth;
        private List<int> _month;
        private List<int> _dayOfWeek;

        /// <summary>
        /// Create new Cron with Cron String
        /// </summary>
        /// <param name="cron"></param>
        public Cron(string cron)
        {
            var spl = cron.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
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
        /// 配置传入的时间是否符合Cron
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool IsMatch(DateTime dateTime)
        {
            if (_minute.Contains(dateTime.Minute)
               && _hour.Contains(dateTime.Hour)
               && _month.Contains(dateTime.Month)
               && (_dayOfMonth.Contains(dateTime.Day) || _dayOfWeek.Contains((int)dateTime.DayOfWeek)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 解析分钟（0 - 59）
        /// </summary>
        /// <param name="minuteCron"></param>
        private void ParseMinute(string minuteCron)
        {
            _minute = new List<int>();
            foreach (var item in minuteCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        _minute.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 60; i++)
                    {
                        if (i % divisor == 0)
                        {
                            _minute.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 60; i++)
                    {
                        _minute.Add(i);
                    }
                }
                else
                {
                    _minute.Add(int.Parse(item));
                }
            }
        }
        /// <summary>
        /// 解析小时（0 - 23）
        /// </summary>
        /// <param name="hourCron"></param>
        private void ParseHour(string hourCron)
        {
            _hour = new List<int>();
            foreach (var item in hourCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        _hour.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 24; i++)
                    {
                        if (i % divisor == 0)
                        {
                            _hour.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 24; i++)
                    {
                        _hour.Add(i);
                    }
                }
                else
                {
                    _hour.Add(int.Parse(item));
                }
            }
        }
        /// <summary>
        /// 解析日（1 - 31）
        /// </summary>
        /// <param name="dayOfMonthCron"></param>
        private void ParseDayOfMonth(string dayOfMonthCron)
        {
            _dayOfMonth = new List<int>();
            foreach (var item in dayOfMonthCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        _dayOfMonth.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i % divisor == 0)
                        {
                            _dayOfMonth.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        _dayOfMonth.Add(i);
                    }
                }
                else
                {
                    _dayOfMonth.Add(int.Parse(item));
                }
            }
        }
        /// <summary>
        /// 解析月（1 - 12）
        /// </summary>
        /// <param name="monthCron"></param>
        private void ParseMonth(string monthCron)
        {
            _month = new List<int>();
            foreach (var item in monthCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        _month.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 1; i <= 12; i++)
                    {
                        if (i % divisor == 0)
                        {
                            _month.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        _month.Add(i);
                    }
                }
                else
                {
                    _month.Add(int.Parse(item));
                }
            }
        }
        /// <summary>
        /// 解析星期（0 - 6，星期日=0）
        /// </summary>
        /// <param name="dayOfWeekCron"></param>
        private void ParseDayOfWeek(string dayOfWeekCron)
        {
            _dayOfWeek = new List<int>();
            foreach (var item in dayOfWeekCron.Split(','))
            {
                if (item.Contains("-"))
                {
                    var start = int.Parse(item.Split('-')[0]);
                    var end = int.Parse(item.Split('-')[1]);
                    for (int i = start; i <= end; i++)
                    {
                        _dayOfWeek.Add(i);
                    }
                }
                else if (item.StartsWith("*/"))
                {
                    var divisor = int.Parse(item.Substring(2));
                    for (int i = 0; i < 7; i++)
                    {
                        if (i % divisor == 0)
                        {
                            _dayOfWeek.Add(i);
                        }
                    }
                }
                else if (item == "*")
                {
                    for (int i = 0; i < 7; i++)
                    {
                        _dayOfWeek.Add(i);
                    }
                }
                else
                {
                    _dayOfWeek.Add(int.Parse(item));
                }
            }
        }
    }
}
