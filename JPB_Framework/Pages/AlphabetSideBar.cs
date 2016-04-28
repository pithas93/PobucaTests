using System;
using JPB_Framework.Selenium;
using OpenQA.Selenium;

namespace JPB_Framework.Pages
{
    public class AlphabetSideBar
    {
        /// <summary>
        /// Selects a letter from the given alphabet at the alphabet side bar.
        /// </summary>
        /// <param name="character">The character which will be selected from the side bar, i.e.: LatinAlphabet.A or GreekAlphabet.Φ e.t.c.</param>
        public static void SelectLetter(object character)
        {
            SelectAlphabet(character.GetType().Name);

            SelectLetter(character.ToString());

        }


        /// <summary>
        /// Clicks the button that matches to the character given
        /// </summary>
        /// <param name="character"></param>
        private static void SelectLetter(string character)
        {
            var alphabetLetters = Driver.Instance.FindElements(By.CssSelector(".letter.ng-binding.ng-scope"));
            foreach (var letter in alphabetLetters)
            {
                if (letter.Text == character)
                {
                    letter.Click();
                    return;
                }
            }
            alphabetLetters[1].Click();
        }

        /// <summary>
        /// Selects the alphabet that contains the character provided
        /// </summary>
        /// <param name="alphabet"></param>
        private static void SelectAlphabet(string alphabet)
        {
            string lang;
            switch (alphabet)
            {
                case "LatinAlphabet":
                    {
                        lang = "Latin";
                        break;
                    }
                case "GreekAlphabet":
                    {
                        lang = "Greek";
                        break;
                    }
                default:
                    {
                        lang = "Latin";
                        break;
                    }
            }
            Driver.Instance.FindElement(By.CssSelector("img[src='img/langs.png']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(1));

            var languageOptions = Driver.Instance.FindElements(By.CssSelector("li[ng-repeat='alpha in alphabets'] .ng-binding"));
            foreach (var languageOption in languageOptions)
            {
                if (languageOption.Text == lang) languageOption.Click();
            }
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        

    }


    /// <summary>
    /// The characters of the latin alphabet from the alphabet side bar. RestChars stands for the '#' character
    /// </summary>
    public enum LatinAlphabet
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, RestChars, All
    }

    /// <summary>
    /// The characters of the greek alphabet from the alphabet side bar. RestChars stands for the '#' character
    /// </summary>
    public enum GreekAlphabet
    {
        Α, Β, Γ, Δ, Ε, Ζ, Η, Θ, Ι, Κ, Λ, Μ, Ν, Ξ, Ο, Π, Ρ, Σ, Τ, Υ, Φ, Χ, Ψ, Ω, RestChars, All
    }
}
