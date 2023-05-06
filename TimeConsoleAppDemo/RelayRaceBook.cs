namespace TimeConsoleAppDemo
{
    public class RelayRaceBook
    {
        public List<Sprinter> Sprinters { get; set; } = new List<Sprinter>();

        public void DisplayAllSprinters ()
        {
            Console.WriteLine("List of sprinters");

            if (Sprinters.Count < 0)
                Console.WriteLine("No sprinters yet, try to add some");
            else
                foreach (Sprinter sprinter in Sprinters)  
                    Console.WriteLine(sprinter);
        }
            
        public void DisplaySprintersWithLessOrEqualTime(string time)
        {
            var tempSprinters = Sprinters.Where(sprinter => sprinter.RunningTime <= new TimeLib.TimePeriod(time)).ToList();

            if (tempSprinters.Count < 0)
                Console.WriteLine("No matching sprinters");
            else
                tempSprinters.ForEach(sprinter => Console.WriteLine(sprinter));
        }


        public void DisplaySprintersWithOverOrEqualTime(string time)
        {
            var tempSprinters = Sprinters.Where(sprinter => sprinter.RunningTime >= new TimeLib.TimePeriod(time)).ToList();

            if (tempSprinters.Count < 0)
                Console.WriteLine("No matching sprinters");
            else
                tempSprinters.ForEach(sprinter => Console.WriteLine(sprinter));
        }

        public void AddSprinter(Sprinter sprinter) => Sprinters.Add(sprinter);
    }
}
