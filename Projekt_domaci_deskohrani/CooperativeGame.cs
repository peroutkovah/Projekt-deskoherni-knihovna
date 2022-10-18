using System;
using System.Collections.Generic;
using System.Text;

namespace HomeGaming
{
	public class CooperativeGame: Game
	{
		public List<int> NumberOfPlayersSeires { get; set; }

		public List<bool> ResultSeries { get; set; }

		public override GameType Type
		{
			get {
				return GameType.Kooperativni;
			}
		}

		public CooperativeGame(string name, int recommendedAge, int minNumberOfPlayers, int maxNumberOfPlayers, int averageLength, string distribution, GameCategory category, decimal price, string evaluation) : base(name, recommendedAge, minNumberOfPlayers, maxNumberOfPlayers, averageLength, distribution, category, price, evaluation)
		{
			NumberOfPlayersSeires = new List<int>();
			ResultSeries = new List<bool>();
		}

		public override void AddGameStatistics()
		{
			base.AddGameStatistics();

			NumberOfPlayersSeires.Add(Validation.CheckNumberOfPlayers("Kolik hráčů hrálo?", this));

			ResultSeries.Add(Validation.CheckCooperativeGameResult("Vyhráli jste hru (pro ANO zvolte číslo 1 a pro NE zvolte číslo 0)"));

			Console.WriteLine($"Statistika ke hře {Name} byla přidána.");
		}

		public override string GameStatisticsOverview()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (NumberOfPlays == 0)
			{
				stringBuilder.Append($"\nPro hru {Name} nebyla odehrána ještě ani jedna partie, proto není možné zobrazit její statistiku!\n");
			}
			else
			{
				stringBuilder.Append(base.GameStatisticsOverview());
				stringBuilder.Append("Průměrný počet hráčů".PadRight(40, '.') + GameStatistics.StatisticsAverageNumberOfPlayers(this) + "\n" + "\n");
				stringBuilder.Append("Hry podle počtu hráčů\n".ToUpperInvariant());
				stringBuilder.Append(GameStatistics.StatisticsNumberOfPlayersFrequency(this) + "\n");
				stringBuilder.Append("Výsledky her\n".ToUpperInvariant());
				stringBuilder.Append(GameStatistics.StatisticsResultOfCooperativeGame(this) + "\n");
			}

			return stringBuilder.ToString();

		}
	}
}
