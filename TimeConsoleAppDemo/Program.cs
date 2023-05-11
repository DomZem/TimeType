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
            Console.WriteLine("5 Add time to the sprinter's score found by name");
            Console.WriteLine("6 Substract time from the sprinter's score found by name");
            Console.WriteLine("7 Display the best sprinter");
            Console.WriteLine("To exit insert 'x'");

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

                        Console.WriteLine($"Insert sprinter's score time in format: h:mm:ss");
                        string scoreTime = Console.ReadLine();

                        relayRaceBook.AddSprinter(firstName, lastName, scoreTime);

                        break;
                    case "2":
                    case "3":
                        Console.WriteLine("Insert sprinter's score time in format: h:mm:ss");
                        string comparerTime = Console.ReadLine();

                        if(userInput == "2")
                            relayRaceBook.DisplaySprintersWithLessOrEqualScoreTime(comparerTime);
                        else
                            relayRaceBook.DisplaySprintersWithOverOrEqualScoreTime(comparerTime);
                        break;
                    case "4":
                        relayRaceBook.DisplayAllSprinters();
                        break;
                    case "5":
                    case "6":
                        Console.WriteLine("Insert sprinter first name:");
                        string sprinterFirstName = Console.ReadLine();

                        Console.WriteLine("Insert sprinter last name:");
                        string sprinterLastName = Console.ReadLine();

                        if(userInput == "5")
                        {
                            Console.WriteLine("Insert time to add sprinter's score time in format: h:mm:ss");
                            string time = Console.ReadLine();
                            relayRaceBook.AddTimeToSprinterScoreTimeFoundByName(sprinterFirstName, sprinterLastName, time);
                        }
                        else
                        {
                            Console.WriteLine("Insert time to substract sprinter's score time in format: h:mm:ss");
                            string time = Console.ReadLine();
                            relayRaceBook.SubstractTimeFromSprinterScoreTimeFoundByName(sprinterFirstName, sprinterLastName, time);
                        }
                        break;
                    case "7":
                        relayRaceBook.DispalyTheBestSprinter();    
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