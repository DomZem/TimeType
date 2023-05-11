using TimeLib;

namespace TimeConsoleAppDemo
{
    public class Sprinter
    {
        public string FirstName { get; }    

        public string LastName { get; }

        public TimePeriod ScoreTime { get; private set; }   

        public Sprinter(string firstName, string lastName, string scoreTime) 
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(scoreTime))
                throw new ArgumentException();

            FirstName = firstName;
            LastName = lastName;    
            ScoreTime = new TimePeriod(scoreTime);    
        }

        public void AddScoreTime(TimePeriod time) => ScoreTime += time;
       
        public void SubstractScoreTime(TimePeriod time) => ScoreTime -= time;

        public override string ToString() => $"{FirstName} {LastName} | {ScoreTime}"; 
    }
}
