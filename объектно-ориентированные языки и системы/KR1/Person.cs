using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1
{
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public Person() : this("John", "Doe", new DateTime(2000, 1, 1)) { }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Born: {BirthDate.ToShortDateString()}";
        }
    }
}
