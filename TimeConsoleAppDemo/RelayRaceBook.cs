using System.Reflection;
using TimeLib;

namespace TimeConsoleAppDemo
{
    public class RelayRaceBook
    {
        public List<Sprinter> Sprinters { get; set; } = new List<Sprinter>();

        public void DisplayAllSprinters()
        {
            Console.WriteLine("List of sprinters");

            if (Sprinters.Count < 0)
                Console.WriteLine("No sprinters, try to add someone");
            else
                foreach (Sprinter sprinter in Sprinters)
                    Console.WriteLine(sprinter);
        }

        public void DisplaySprintersWithLessOrEqualScoreTime(string time)
        {
            try
            {
                TimePeriod scoreTime = new TimePeriod(time);
                var tempSprinters = Sprinters.Where(sprinter => sprinter.ScoreTime <= scoreTime).ToList();

                if (tempSprinters.Count < 0)
                    Console.WriteLine("No matching sprinters");
                else
                    tempSprinters.ForEach(sprinter => Console.WriteLine(sprinter));

            } catch { 
                DisplayErrorMessage();
            }
        }

        public void AddTimeToSprinterScoreTimeFoundByName(string firstName, string lastName, string time)
        {
            try
            {
                var sprinter = Sprinters.FirstOrDefault(sprinter => sprinter.FirstName == firstName && sprinter.LastName == lastName);

                if (sprinter == null)
                    Console.WriteLine("No matching sprinter");
                else
                {
                    TimePeriod scoreTime = new TimePeriod(time);
                    sprinter.AddScoreTime(scoreTime);
                }
            } catch {
                DisplayErrorMessage();
            }
        }

        public void SubstractTimeFromSprinterScoreTimeFoundByName(string firstName, string lastName, string time)
        {
            try
            {
                var sprinter = Sprinters.FirstOrDefault(sprinter => sprinter.FirstName == firstName && sprinter.LastName == lastName);

                if (sprinter == null)
                    Console.WriteLine("No matching sprinter");
                else
                {
                    TimePeriod scoreTime = new TimePeriod(time);
                    sprinter.SubstractScoreTime(scoreTime);
                }
            } catch {
                DisplayErrorMessage();
            }
        }

        public void DisplaySprintersWithOverOrEqualScoreTime(string time)
        {
            try
            {
                TimePeriod scoreTime = new TimePeriod(time); 
                var tempSprinters = Sprinters.Where(sprinter => sprinter.ScoreTime >= scoreTime).ToList();

                if (tempSprinters.Count < 0)
                    Console.WriteLine("No matching sprinters");
                else
                    tempSprinters.ForEach(sprinter => Console.WriteLine(sprinter));
            } catch {
                DisplayErrorMessage();
            }
        }

        public void AddSprinter(string firstName, string lastName, string time) 
        {
            try
            {
                Sprinter sprinter = new Sprinter(firstName, lastName, time);
                Sprinters.Add(sprinter);
            } catch {
                DisplayErrorMessage();
            }
        }

        public void DispalyTheBestSprinter()
        {
            if (Sprinters.Count < 0)
            {
                Console.WriteLine("No sprinters, try to add someone");
                return;
            }
           
            var bestSprinter = Sprinters.OrderBy(sprinter => sprinter.ScoreTime).First();
            Console.WriteLine(bestSprinter);    
        }

        private void DisplayErrorMessage() => Console.WriteLine("Incorrect data has been entered");
    }
}
