using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPB_Framework.Workflows
{
    public class DummyData
    {
        public static string FirstName { get { return RandomWord(3); } }
        public static string LastName { get { return RandomWord(3); } }
        public static string MiddleName { get { return RandomWord(2); } }
        public static string Suffix { get { return RandomWord(1); } }
        public static string OrganizationNameExisting { get { return RandomOrganization(); } }
        public static string OrganizationNameNotExisting { get { return RandomWord(4); } }
        public static string Department { get { return RandomDepartment(); } }


        public static string WorkPhone { get { return RandomNumber(10); } }
        public static string WorkPhone2 { get { return RandomNumber(10); } }
        public static string MobilePhone { get { return RandomNumber(10); } }
        public static string MobilePhone2 { get { return RandomNumber(10); } }
        public static string HomePhone { get { return RandomNumber(10); } }
        public static string HomePhone2 { get { return RandomNumber(10); } }
        public static string HomeFax { get { return RandomNumber(10); } }
        public static string WorkFax { get { return RandomNumber(10); } }
        public static string OtherPhone { get { return RandomNumber(10); } }


        public static string Email { get { return RandomWord(4); } }
        public static string PersonalEmail { get { return RandomWord(4); } }
        public static string OtherEmail { get { return RandomWord(4); } }


        public static string WorkStreet { get { return $"{RandomWord(3)} {RandomNumber(1)}"; } }
        public static string WorkCity { get { return RandomWord(3); } }
        public static string WorkState { get { return RandomWord(2); } }
        public static string WorkPostalCode { get { return RandomNumber(5); } }
        public static string WorkCountry { get { return RandomCountry(); } }

        public static string HomeStreet { get { return $"{RandomWord(3)} {RandomNumber(1)}"; } }
        public static string HomeCity { get { return RandomWord(3); ; } }
        public static string HomeState { get { return RandomWord(2); ; } }
        public static string HomePostalCode { get { return RandomNumber(5); } }
        public static string HomeCountry { get { return RandomCountry(); } }

        public static string OtherStreet { get { return $"{RandomWord(3)} {RandomNumber(1)}"; } }
        public static string OtherCity { get { return RandomWord(3); ; } }
        public static string OtherState { get { return RandomWord(2); ; } }
        public static string OtherPostalCode { get { return RandomNumber(5); } }
        public static string OtherCountry { get { return RandomCountry(); } }


        public static string Salutation { get { return RandomWord(3); } }
        public static string Nickname { get { return RandomWord(3); } }
        public static string JobTitle { get { return RandomWord(3); } }
        public static string Website { get { return RandomWord(3); } }
        public static string Religion { get { return RandomWord(3); } }
        public static string Birthdate { get { return RandomDate(); } }
        public static string Gender { get { return RandomGender(); } }
        public static string Comments { get { return RandomWord(10); } }


        public static string AllowSMS { get { return RandomBoolean(); } }
        public static string AllowPhones { get { return RandomBoolean(); } }
        public static string AllowEmails { get { return RandomBoolean(); } }

        public static string OverflowValue { get { return "qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm"; } }
        public static string NonsenseValue { get { return "!@#123qweQWEασδΑΣΔ"; } }

        private static string RandomWord(int size)
        {
            var word = new StringBuilder();
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            for (var i = 0; i < size; i++)
            {
                word.Append(Words[random.Next(Words.Length)]);
            }

            return word.ToString();
        }

        private static string RandomNumber(int digits)
        {
            var word = new StringBuilder();
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            for (var i = 0; i < digits; i++)
            {
                word.Append(Numbers[random.Next(Numbers.Length)]);
            }

            return word.ToString();
        }

        private static string RandomDepartment()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Departments[random.Next(Departments.Length)];
        }

        private static string RandomOrganization()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Organizations[random.Next(Organizations.Length)];
        }

        private static string RandomDate()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Dates[random.Next(Dates.Length)];
        }

        private static string RandomGender()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Genders[random.Next(Genders.Length)];
        }

        private static string RandomBoolean()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Booleans[random.Next(Booleans.Length)];
        }

        private static string RandomCountry()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Countries[random.Next(Countries.Length)];
        }

        private static string[] Words =
        {
            "alpha", "beta","gama","delta","epsilon","zeta","eta","theta","yiota","kapa","lamba","mi","ni","ksi","omikron","pi",
            "ro","sigma","taf","ipsilon","phi","chi","psi","omega"
        };

        private static string[] Numbers =
        {
            "0","1","2","3","4","5","6","7","8","9"
        };

        private static string[] Dates =
        {
            "01-08-2004","04-01-1985","21-06-1967","30-09-1950","25-12-1945",
            "02-09-2012","05-04-1980","22-07-1960","27-01-1990","03-02-1999",
            "03-10-1995","06-05-1972","29-08-2000"
        };

        private static string[] Genders =
        {
            "Male", "Female", "Other"
        };

        private static string[] Booleans =
        {
            "True", "False"

        };

        private static string[] Countries =
        {
            "Greece","Australia","Japan","France","Namibia","Pakistan",
        };

        private static string[] Departments =
        {
            "Administration", "Human Resources", "Consulting", "Logistics", "Research and Development", "Sales",
            "Production", "I.T. (Information Technology)",
        };

        private static string[] Organizations =
        {
            "KONICA MINOLTA","LUCA PRODUCTION SRL","KOSMOCAR","KNOW HOW CONSULTING",
            "MARATHON DATA SYSTEMS","MARITECH","LAWNET"
        };
    }
}
