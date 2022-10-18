using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;



namespace HomeGaming
{
	public static class  GameStatistics 
	{
		
		public static int StatisticsNumberOfPlays (Game game)
		{
			return game.NumberOfPlays;
		}

		public static string StatisticsNumberOfPlaysPerYear(Game game)
		{


			var numberOfPlaysPerYear = game.DateOfGameSeries.GroupBy(d => d.Year).ToDictionary(d => d.Key, d => d.Count());
			
			StringBuilder str=new StringBuilder();
			foreach(var n in numberOfPlaysPerYear)
			{
				str.Append($"rok {n.Key} - {n.Value}\n");
			}
						
			return str.ToString();
		}

		public static string StatisticsNumberOfPlaysPerMonth(Game game)
		{

			Dictionary<int, string> MonthDict = new Dictionary<int, string>
			{
			   { 1 , "Leden" },
			   {2 , "Únor" },
			   {3 , "Březen" },
			   {4 , "Duben"},
			   {5 , "Květen"},
			   {6 , "Červen"},
			   {7 , "Červenec"},
			   {8 , "Srpen"},
			   {9 , "Září"},
			   {10 , "Říjen"},
			   {11 , "Listopad"},
			   {12 , "Prosinec"}
			};
			
			var numberOfPlaysPerMonth = game.DateOfGameSeries.GroupBy(d => d.Year + " " + MonthDict[d.Month]).ToDictionary(d => d.Key, d => d.Count());

			StringBuilder str = new StringBuilder();
			foreach (var n in numberOfPlaysPerMonth)
			{
				string line = new string('#', n.Value);
				str.Append($"{n.Key.PadRight(15)} | {line}\n");
			}

			return str.ToString();
		}

		public static string StatisticsFirstDayofPlay(Game game)
		{
			var culture = new CultureInfo("cs-cz");
			return game.DateOfGameSeries.OrderBy(d => d.Date).First().ToString("dd.MM.yyyy", culture);

		}

		public static string StatisticsLastDayofPlay(Game game)
		{
			var culture = new CultureInfo("cs-cz");
			return game.DateOfGameSeries.OrderByDescending(d =>d.Date).First().ToString("dd.MM.yyyy", culture);

		}

		//pro kompetitivni hru
		public static string StatisticsAverageNumberOfPlayers(CompetitiveGame competitiveGame)
		{
			return Math.Round(competitiveGame.NumberOfPlayersSeires.Average(),2).ToString();
		}

		public static string StatisticsNumberOfPlayersFrequency(CompetitiveGame competitiveGame)
		{
			var numberOfPlayersFrequency= competitiveGame.NumberOfPlayersSeires.GroupBy(p => p.ToString()).ToDictionary(p => p.Key, p => p.Count());

			StringBuilder str = new StringBuilder();
			str.Append("počet hráčů".PadRight(15) + ": "+ "počet her (podíl v %)".PadLeft(15)+"\n");
			str.Append("------------------------------------------\n");

			double sumPlayes= numberOfPlayersFrequency.Values.Sum();

			foreach (var p in numberOfPlayersFrequency)
			{
				double percentage = Math.Round((p.Value / sumPlayes) * 100,1);
				str.Append($"{(p.Key.PadLeft(8)).PadRight(15)}: {p.Value.ToString().PadLeft(8)} ({percentage.ToString()}%) \n");
			}

			return str.ToString();
		}
		public static string StatisticsWinnerFrequency(CompetitiveGame competitiveGame)
		{
			var winnerFrequency = competitiveGame.WinnerSeries.GroupBy(w => w.ToString()).OrderByDescending(w => w.Count()).ToDictionary(w => w.Key, w => w.Count());

			StringBuilder str = new StringBuilder();
			str.Append("výherce".PadRight(15) + ": " + "počet her (podíl v %)".PadLeft(15) + "\n");
			str.Append("------------------------------------------\n");

			double sumPlayes = winnerFrequency.Values.Sum();

			foreach (var w in winnerFrequency)
			{
				double percentage = Math.Round((w.Value / sumPlayes) * 100, 1);
				str.Append($"{w.Key.PadRight(15)}: {w.Value.ToString().PadLeft(8)} ({percentage.ToString()}%) \n");
			}

			return str.ToString();
		}
		public static string StatisticsMostOftenWinner(CompetitiveGame competitiveGame)
		{
			var mostOftenWinner = competitiveGame.WinnerSeries.GroupBy(w => w.ToString()).OrderByDescending(w => w.Count()).ToDictionary(w => w.Key, w => w.Count()).First();

			StringBuilder str = new StringBuilder();
			str.Append($"{mostOftenWinner.Key}: {mostOftenWinner.Value} vítězství \n");

			return str.ToString();
		}

		//kooperativni hry
		public static string StatisticsAverageNumberOfPlayers(CooperativeGame cooperativeGame)
		{
			return Math.Round(cooperativeGame.NumberOfPlayersSeires.Average(), 2).ToString();
		}

		public static string StatisticsNumberOfPlayersFrequency(CooperativeGame cooperativeGame)
		{
			var numberOfPlayersFrequency = cooperativeGame.NumberOfPlayersSeires.GroupBy(p => p.ToString()).ToDictionary(p => p.Key, p => p.Count());

			StringBuilder str = new StringBuilder();
			str.Append("počet hráčů".PadRight(15) + ": " + "počet her (podíl v %)".PadLeft(15) + "\n");
			str.Append("------------------------------------------\n");

			double sumPlayes = numberOfPlayersFrequency.Values.Sum();

			foreach (var p in numberOfPlayersFrequency)
			{
				double percentage = Math.Round((p.Value / sumPlayes) * 100, 1);
				str.Append($"{(p.Key.PadLeft(8)).PadRight(15)}: {p.Value.ToString().PadLeft(8)} ({percentage.ToString()}%) \n");
			}

			return str.ToString();
		}

		public static string StatisticsResultOfCooperativeGame(CooperativeGame cooperativeGame)
		{
			var winnerFrequency = cooperativeGame.ResultSeries.GroupBy(w => w).OrderByDescending(w => w.Count()).ToDictionary(w => w.Key, w => w.Count());

			StringBuilder str = new StringBuilder();
			str.Append("výsledek".PadRight(15) + ": " + "počet her (podíl v %)".PadLeft(15) + "\n");
			str.Append("------------------------------------------\n");

			double sumPlayes = winnerFrequency.Values.Sum();

			
			foreach (var w in winnerFrequency)
			{
				string label="";
				if (w.Key==true)
				{
					label="Výhra";
				}
				else
				{
					label = "Prohra";
				}
				double percentage = Math.Round((w.Value / sumPlayes) * 100, 1);
				str.Append($"{label.PadRight(15)}: {w.Value.ToString().PadLeft(8)} ({percentage.ToString()}%) \n");
			}

			return str.ToString();
		}

		//Smart hry
		public static string StatisticsAverageReachedLevel(SmartGame smartGame)
		{
			return Math.Round(smartGame.ReachedLevelSeries.Average(), 0).ToString();
		}

		public static string StatisticsMaxReachedLevel(SmartGame smartGame)
		{
			return smartGame.ReachedLevelSeries.Max().ToString();
		}

	}
}