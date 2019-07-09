using System;

namespace LanguageBasicAssingment2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            Console.WriteLine(GetCurrentTime(postTimes, showTimes));
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {   
            int hour, minutes, seconds;
            int hourLowerLimit =0, minuteLowerLimit=0, secondLowerLimit=0;
            int hourUpperLimit=23, minuteUpperLimit=59, secondUpperLimit=59;
            int time_upper= 86399, time=0,t1,t2;
            for (int i =exactPostTime.Length - 1 ; i>=0 ; i--)
            { hour = Int32.Parse(exactPostTime[i].Split(":")[0]);
             minutes = Int32.Parse(exactPostTime[i].Split(":")[1]);
             seconds = Int32.Parse(exactPostTime[i].Split(":")[2]);
                if (showPostTime[i] == "few seconds ago")
                {

                    secondUpperLimit = (seconds + 59) % 60;
                    minuteUpperLimit = (minutes + ((seconds + 59) / 60)) % 60;
                    hourUpperLimit = hour + ((minutes + ((seconds + 59) / 60)) / 60) ;
                   // Console.WriteLine($"Upper Limit = {h2}:{m2}:{s1}");

                    hourLowerLimit = hour;
                    minuteLowerLimit = minutes;
                    secondLowerLimit = seconds;
                    //Console.WriteLine($"Lower Limit = {hh}:{mm}:{ss}");

                }
                if (showPostTime[i].Split(" ")[1][0] == 'm')
                {
//                  Calculating Upper Limit of Time
                    secondUpperLimit = (seconds + 59) % 60;
                    minuteUpperLimit = (minutes+ Int32.Parse(showPostTime[i].Split(" ")[0])+ ((seconds + 59) / 60)) % 60;
                    hourUpperLimit = (hour + ((minutes+ Int32.Parse(showPostTime[i].Split(" ")[0]) + ((seconds + 59) / 60)) / 60));
                    //                  Calculating Lower Limit of Time
                   // Console.WriteLine($"Upper Limit = {h2}:{m2}:{s2}");    
                    minuteLowerLimit = (minutes+ Int32.Parse(showPostTime[i].Split(" ")[0]))% 60;
                     hourLowerLimit = (hour + (minutes + Int32.Parse(showPostTime[i].Split(" ")[0])) / 60) % 24;
                   // Console.WriteLine($"Lower Limit = {hh}:{mm}:{ss}");
                    secondLowerLimit = seconds;
                }
                if (showPostTime[i].Split(" ")[1][0] == 'h')
                {
//                  Calculating Upper Limit of Time
                    secondUpperLimit = (seconds + 59) % 60;
                    minuteUpperLimit = (minutes + 59+((seconds + 59) / 60)) % 60;
                    hourUpperLimit =( hour+ Int32.Parse(showPostTime[i].Split(" ")[0]) + ((minutes + 59 + ((seconds + 59) / 60)) / 60))  ;
 
                    //                  Calculating Lower Limit of Time
                    hourLowerLimit = (hour + Int32.Parse(showPostTime[i].Split(" ")[0])) % 24;
                    minuteLowerLimit = minutes;
                    secondLowerLimit = seconds;
                 //   Console.WriteLine($"Lower Limit = {hh}:{mm}:{ss}");

                }
                 t1 = hourLowerLimit * 3600 + minuteLowerLimit * 60 + secondLowerLimit;
                 t2 = hourUpperLimit * 3600 + minuteUpperLimit * 60 + secondUpperLimit;

 

                //compare upper limit of previously calculate time to lower limit 
                if (t1 > time_upper || t2 < time)
                    return "impossible";
                if (time < t1)
                {
                    time = t1;
                }

                





            }
            string finalHour = ((time / 3600)% 24).ToString();
            string finalMinute = ((time % 3600)/ 60).ToString();
            string finalSecond = (time % 60).ToString();
            //For Adding Zero in single digit time
            if (finalHour.Length == 1)
                finalHour = "0" + finalHour;

            if (finalMinute.Length == 1)
                finalMinute = "0" + finalMinute;

            if (finalSecond.Length == 1)
                finalHour = "0" + finalSecond;
            return $"{finalHour}:{finalMinute}:{finalSecond}";

            throw new NotImplementedException();
        }
     }
}
