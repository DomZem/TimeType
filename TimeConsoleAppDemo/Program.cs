using TimeLib;

namespace TimeConsoleAppDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimePeriod timePeriod1 = new ("29:58:12");
            TimePeriod timePeriod2 = new ("12:25:23");

            Console.WriteLine(timePeriod1 - timePeriod2);
        }
    }
}