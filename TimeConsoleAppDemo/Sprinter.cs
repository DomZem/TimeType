using TimeLib;

namespace TimeConsoleAppDemo
{
    public class Sprinter
    {
        public string FirstName { get; }    

        public string LastName { get; }

        public TimePeriod RunningTime { get; }   

        public Sprinter(string firstName, string lastName, string score) 
        { 
            FirstName = firstName;
            LastName = lastName;    
            RunningTime = new TimePeriod(score);    
        }

        public override string ToString() => $"{FirstName} {LastName} | {RunningTime}"; 
    }
}
