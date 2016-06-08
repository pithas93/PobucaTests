using System;
using System.Text;

namespace JPB_Framework.Workflows
{
    public class DummyData
    {

        /// <summary>
        /// Get a random, medium length word. Characters are small latin letters.
        /// </summary>
        public static string SimpleWord => RandomWord(3);

        /// <summary>
        /// Get a random simple text. Characters are small latin letters.
        /// </summary>
        public static string SimpleText => RandomWord(30);

        /// <summary>
        /// Get a random, 5-digit numeric value 
        /// </summary>
        public static string NumericValue => RandomNumber(5);

        /// <summary>
        /// Get a random 55-character sequence consisting of greek, 
        /// latin letters, numerical digits and special characters like !, @, #, etc
        /// </summary>
        public static string OverflowWordValue => RandomString(55);

        public static string OverflowTextValue => RandomWord(100);

        /// <summary>
        /// Get a random 15-character sequence consisting of greek, 
        /// latin letters, numerical digits and special characters like !, @, #, etc
        /// </summary>
        public static string NonsenseValue => RandomString(15);

        /// <summary>
        /// Get a random value between "True" and "False"
        /// </summary>
        public static string BooleanValue => RandomBoolean();

        /// <summary>
        /// Get a random value between "Male", "Female" and "Other"
        /// </summary>
        public static string GenderValue => RandomGender();

        /// <summary>
        /// Get a random date
        /// </summary>
        public static string DateValue => RandomDate();

        /// <summary>
        /// Get a random country
        /// </summary>
        public static string CountryValue => RandomCountry();

        /// <summary>
        /// Get an random, address-like character sequence. 
        /// Sequence begins with a word, continues with a space, 
        /// ends up with a number and looks like "blablabla 2"
        /// </summary>
        public static string AddressValue => $"{RandomWord(3)} {RandomNumber(1)}";

        /// <summary>
        /// Get a random valid format email
        /// </summary>
        public static string EmailValue => RandomEmail();

        /// <summary>
        /// Get a random 10-digit numeric value 
        /// </summary>
        public static string PhoneValue => RandomNumber(10);
        
        /// <summary>
        /// Get a random organization name. Organization name exists in organization list
        /// </summary>
        public static string OrganizationValue => RandomOrganization();

        /// <summary>
        /// Get a random department name. Department name exists in department list
        /// </summary>
        public static string DepartmentValue => RandomDepartment();

        /// <summary>
        /// Get a random account type. Account type exists in account type list
        /// </summary>
        public static string AccountTypeValue => RandomAccountType();

        /// <summary>
        /// Get a random industry. Industry exists in industry list
        /// </summary>
        public static string IndustryValue => RandomIndustry();

        /// <summary>
        /// Get a random word consisting of latin alphabet characters. Words are the names of the greek alphabet letters like alpha, beta, etc.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string RandomWord(int size)
        {
            var word = new StringBuilder();
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            for (var i = 0; i < size; i++)
            {
                word.Append(Words[random.Next(Words.Length)]);
                if (i>0 && i%3 == 0) word.Append(' ');
            }

            return word.ToString();
        }

        /// <summary>
        /// Get a random sequence of numerical characters
        /// </summary>
        /// <param name="digits">Character sequence length</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a random character sequence. Characters belong to greek or latin alphabets, numerical digits and any special character from the keyboard
        /// </summary>
        /// <param name="digits">Character sequence length</param>
        /// <returns></returns>
        private static string RandomString(int digits)
        {
            var word = new StringBuilder();
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            for (var i = 0; i < digits; i++)
            {
                word.Append(Characters[random.Next(Characters.Length)]);
            }

            return word.ToString();
        }

        /// <summary>
        /// Get a random existing department. The department exists in department list
        /// </summary>
        /// <returns></returns>
        private static string RandomDepartment()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Departments[random.Next(Departments.Length)];
        }

        /// <summary>
        /// Get a random existing organization name. The organization exists in organizations page
        /// </summary>
        /// <returns></returns>
        private static string RandomOrganization()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Organizations[random.Next(Organizations.Length)];
        }

        /// <summary>
        /// Get a random valid date.
        /// </summary>
        /// <returns></returns>
        private static string RandomDate()
        {
            var word = new StringBuilder();
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            var day = random.Next(1,28);
            if (day < 10) word.Append('0');
            word.Append(day);
            word.Append('-');
            var month = random.Next(1, 12);
            if (month < 10) word.Append('0');
            word.Append(month);
            word.Append('-');
            word.Append(random.Next(1926, 2016));
            return word.ToString();
        }

        /// <summary>
        /// Get a random value between male, female and other
        /// </summary>
        /// <returns></returns>
        private static string RandomGender()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Genders[random.Next(Genders.Length)];
        }

        /// <summary>
        /// Get randomly either True or False as a value
        /// </summary>
        /// <returns></returns>
        private static string RandomBoolean()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Booleans[random.Next(Booleans.Length)];
        }

        /// <summary>
        /// Get a random existing country
        /// </summary>
        /// <returns></returns>
        private static string RandomCountry()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Countries[random.Next(Countries.Length)];
        }

        /// <summary>
        /// Get a random email of valid form
        /// </summary>
        /// <returns></returns>
        private static string RandomEmail()
        {
            var word = new StringBuilder();
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            word.Append(Words[random.Next(Words.Length)]);
            word.Append('@');
            word.Append(Words[random.Next(Words.Length)]);
            word.Append('.');
            for (var i=0;i<3;i++) word.Append(LatinAlphabet[random.Next(LatinAlphabet.Length)]);
            return word.ToString();
        }

        /// <summary>
        /// Get a random account type. The account type exists in account type list
        /// </summary>
        /// <returns></returns>
        private static string RandomAccountType()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return AccountType[random.Next(AccountType.Length)];
        }

        /// <summary>
        /// Get a random industry. The industry exists in industry list
        /// </summary>
        /// <returns></returns>
        private static string RandomIndustry()
        {
            var random = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            return Industry[random.Next(Industry.Length)];
        }

        private static string[] Words =
        {
            "alpha", "beta","gama","delta","epsilon","zeta","eta","theta","yiota","kapa","lamda","mi","ni","ksi","omikron","pi",
            "ro","sigma","taf","ipsilon","phi","chi","psi","omega"
        };

        private static char[] Numbers =
        {
            '0','1','2','3','4','5','6','7','8','9'
        };

        private static char[] LatinAlphabet =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        private static char[] Characters =
        {
            '0','1','2','3','4','5','6','7','8','9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'α', 'β', 'γ', 'δ', 'ε', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω',
            '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '`', '~', '=', '[', ']', '{', '}', '|', ';', ',', '.', '/', '<', '>', '?'
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
            "Production", "I.T. (Information Technology)"
        };

        private static string[] Organizations =
        {
            "KONICA MINOLTA","LUCA PRODUCTION SRL","KOSMOCAR","KNOW HOW CONSULTING",
            "MARATHON DATA SYSTEMS","MARITECH","LAWNET"
        };

        private static string[] Industry =
        {
            "Advertising & Media Services", "Agriculture", "Apparel", "Consumer Services", "Engineering Services", "Food & Beverage", "Machinery & Equipment",
            "Office Supplies & Equipment", "Telecommunications", "Real Estate", "Other", "Medical & Healthcare", "Transportation & Logistics", "Wholesale"
        };

        private static string[] AccountType =
        {
            "Consultant", "Customer", "Influencer", "Partner", "Investor", "Prospect", "Reseller", "Supplier"
        };

    }
}
