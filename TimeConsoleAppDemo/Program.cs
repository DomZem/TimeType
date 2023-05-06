namespace TimeConsoleAppDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from the RelayRaceBook app");

            Console.WriteLine("1 Add sprinter");
            Console.WriteLine("2 Display sprinters less than or equal to the given time");
            Console.WriteLine("3 Display sprinters over than or equal to the given time");
            Console.WriteLine("4 Display all sprinters");
            Console.WriteLine("To exit insert 'x'");

            Console.WriteLine();

            var relayRaceBook = new RelayRaceBook();
            string userInput = Console.ReadLine();

            while (true)
            {
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Insert first name:");
                        string firstName = Console.ReadLine();

                        Console.WriteLine("Insert last name:");
                        string lastName = Console.ReadLine();
                        
                        Console.WriteLine($"Insert sprint score in format: h:mm:ss");
                        string score = Console.ReadLine();

                        relayRaceBook.AddSprinter(new Sprinter(firstName, lastName, score));

                        break;
                    case "2":
                        Console.WriteLine("Insert sprint time");
                        string lessEqualTime = Console.ReadLine();
                        relayRaceBook.DisplaySprintersWithLessOrEqualTime(lessEqualTime);   

                        break;
                    case "3":
                        Console.WriteLine("Insert sprint time");
                        string overEqualTime = Console.ReadLine();
                        relayRaceBook.DisplaySprintersWithOverOrEqualTime(overEqualTime);

                        break;
                    case "4":
                        relayRaceBook.DisplayAllSprinters();
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }
                Console.WriteLine("Select operation");
                userInput = Console.ReadLine();
            }
        }
    }
}