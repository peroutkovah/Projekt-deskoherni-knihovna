using System;
using System.Globalization;

namespace HomeGaming
{
	public static class Validation
	{
        public static void WrongInput(string textConsole)
		{
            Console.WriteLine(textConsole);
            Console.WriteLine("Napiš znovu!");
        }

        public static bool EndProgramCheck(string input)
        {
            bool end = false;
            if (input.Equals("x", StringComparison.OrdinalIgnoreCase))
            {
                end = true;
                Console.WriteLine("Program končí!");
                Environment.Exit(0);

            }
            return end;
        }

		public static int MainInputCheck(int numberOfChoices, string inquiry)
        {
            int number = 0;
            bool parseResult = false;
			bool condition= true;

			while (condition)
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();

				parseResult = int.TryParse(input, out number);
                EndProgramCheck(input);
				condition = !parseResult || number > numberOfChoices || number < 1;
				if (condition)
                {
					WrongInput($"Tohle není správné číslo! Volba musí být číslo mezi 1-{numberOfChoices}!");
                }

            }
            return number;
        }

        public static string StringInputCheck(string inquiry)
        {
            string text = string.Empty; 
             
            while (string.IsNullOrWhiteSpace(text)) 
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
                text = Console.ReadLine();
                EndProgramCheck(text);
            
                if (string.IsNullOrWhiteSpace(text))
                {
                    WrongInput("Zadaný text nesmí být prázdný!");                   
                }

            }
            return text;
        }

        public static int IntegerInputCheck(string inquiry) 
        {
            int number = 0;
            bool parseResult = false;
			bool condition = true;

			while (condition)
            {
                
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                EndProgramCheck(input);
                parseResult = int.TryParse(input, out number);
				condition= parseResult == false || number <= 0;

				if (parseResult == false)
                {
                    WrongInput("Tohle není číslo!");
                }
                else if (number <= 0)
                {
                    WrongInput("Číslo musí být větší než nula!");
                }
            }
            return number;
        }

        public static decimal DecimalInputCheck(string inquiry)
        {
            decimal number = 0;
            bool parseResult = false;
			bool condition = true;

			while (condition)
            {
                
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                EndProgramCheck(input);
                parseResult = decimal.TryParse(input, out number);
				condition = parseResult == false || number <= 0 || number>5000;

				if (parseResult == false)
                {
                    WrongInput("Tohle není číslo!");  
                }

                else if (number <= 0)
                {
                    WrongInput("Číslo musí být větší než nula!");
                }
				else if (number > 5000)
				{
					WrongInput("Není pravděpodobné, že by ceny hry byl větší neež 5 000 Kč!");
				}
			}
            return number;
        }


        public static GameCategory CheckGameCategory(string inquiry)
        {

            int numberGameCategory=0;
            bool parseResult=false;
			bool condition = true;


			while (condition) 
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                EndProgramCheck(input);
                parseResult = int.TryParse(input, out numberGameCategory);
				condition = parseResult == false || !Enum.IsDefined(typeof(GameCategory), numberGameCategory);

				if (condition)
                {
                    WrongInput("Tohle není správná volba!");
                }

            }
            return (GameCategory)numberGameCategory;
        }

        public static DateTime Gamedate(string inquiry)
        {
			var culture = new CultureInfo("cs-cz");
			string[] formats = { "dd.MM.yyyy" };
			DateTime parsedDateTime = DateTime.Now; 
            bool parseResult = false;
			bool condition=true;
            
       
            while (condition)
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                Validation.EndProgramCheck(input);
                parseResult = DateTime.TryParseExact(input, formats, culture,
                                         DateTimeStyles.None, out parsedDateTime);
				condition=!parseResult || parsedDateTime > DateTime.Now;

				if (parseResult == false)
                {
                    WrongInput("Tohle není datum ve správném formátu!");
                }
				else if(parsedDateTime> DateTime.Now)
				{
					WrongInput("Datum hry nemůže bát v budoucnu!");
				}
            }

            return parsedDateTime;
        }

        public static Winner CheckWinner(string inquiry)
        {

            int numberWinner = 0;
            bool parseResult = false;
            bool condition = true; 

            while (condition)
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                EndProgramCheck(input);
                parseResult = int.TryParse(input, out numberWinner);
				condition = !parseResult || !Enum.IsDefined(typeof(Winner), numberWinner);

				if (condition) 
                {
                    WrongInput("Tohle není správná volba!");
                }

            }
            return (Winner)numberWinner;
        }

        public static int CheckNumberOfPlayers(string inquiry, Game gameForStatistics)
        {
            int number = 0;
            bool parseResult = false;
			bool condition = true;

			while (condition)
            {
 
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                EndProgramCheck(input);
                parseResult = int.TryParse(input, out number);
				condition= !parseResult || number > gameForStatistics.MaxNumberOfPlayers || number < gameForStatistics.MinNumberOfPlayers;


				if (!parseResult)
                {
                    WrongInput("Tohle není číslo!");
                }

                else if (parseResult && number > gameForStatistics.MaxNumberOfPlayers)
                {
                    WrongInput($"Počet hráčů musí být nižší nebo roven maximálnímu počtu hráčů ({gameForStatistics.MaxNumberOfPlayers})");
                }

				else if (parseResult && number < gameForStatistics.MinNumberOfPlayers)
                {
                    WrongInput($"Počet hráčů musí být vyšší nebo roven minimálnímu počtu hráčů ({gameForStatistics.MinNumberOfPlayers})");
                }

            }
            return number;
        }

        public static bool CheckCooperativeGameResult(string inquiry)
		{

            int number=2;
            bool parseResult = false;
            bool result=false;
			bool condition = true;

			while (condition)
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
				string input = Console.ReadLine();
                EndProgramCheck(input);
                parseResult = int.TryParse(input, out number);
				condition= !parseResult || (number != 0 && number != 1);

				if (parseResult == false)
                {
                    WrongInput("Tohle není číslo!");
                }
                else if (number != 0 && number != 1)
                {
                    WrongInput("Číslo musí být 0 pro prohru nebo 1 pro výhru!");
                }

                if (number == 1)
                {
                    result = true;
                }
                else if (number == 0)
                {
                    result = false;
                }
            }
            return result;
        }

        public static int CheckMaxLevelSmartGame(string inquiry,int maxLevel)
        {
            int number = 0;
            bool parseResult = false;
			bool condition = true;


			while (condition)
            {
				WorkWithConsole.ProgramEndNote();
				Console.WriteLine(inquiry);
                string input = Console.ReadLine();
				EndProgramCheck(input);
                parseResult = int.TryParse(input, out number);
				condition = !parseResult || number <= 0 || number > maxLevel;

				if (!parseResult)
                {
                    WrongInput("Tohle není číslo!");
                }

                if (number <= 0)
                {
                    WrongInput("Číslo musí být větší než nula!");
                }

                if (number > maxLevel)
                {
                    WrongInput($"Číslo musí být menší než nejvyšší existující level hry ({maxLevel})!");
                }
            }
            return number;
        }
    }
}
