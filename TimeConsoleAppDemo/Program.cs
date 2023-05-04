using TimeLib;

namespace TimeConsoleAppDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Time time1 = new Time("7:30:00");
            Time time2 = new Time("21:30:00");

            TimePeriod period1 = new TimePeriod(time1, time2);

            Console.WriteLine(period1);
        }
    }
}