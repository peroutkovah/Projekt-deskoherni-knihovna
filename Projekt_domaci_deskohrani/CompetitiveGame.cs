using System;
using System.Collections.Generic;
using System.Text;

namespace HomeGaming
{
	public class CompetitiveGame: Game
	{

		public List<int> NumberOfPlayersSeires { get; set; }

		public List<Winner> WinnerSeries { get; set; }

		public override GameType Type
		{
			get {
				return GameType.Kompetitivni;
			}
		}

		public CompetitiveGame(string name, int recommendedAge, int minNumberOfPlayers, int maxNumberOfPlayers, int averageLength, string distribution, GameCategory category, decimal price, string evaluation) : base(name, recommendedAge, minNumberOfPlayers, maxNumberOfPlayers, averageLength, distribution, category, price, evaluation)
		{
			NumberOfPlayersSeires = new List<int>();
			WinnerSeries = new List<Winner>();

		}

		public override void AddGameStatistics()
		{
			base.AddGameStatistics();

			NumberOfPlayersSeires.Add(Validation.CheckNumberOfPlayers("Kolik hráčů hrálo?", this));

			WinnerSeries.Add(Validation.CheckWinner("Kdo vyhrál? (stačí napsat číslo mezi 1-5):\n" +
				" 1. Táta\n 2. Máma\n 3.Anežka\n 4.Vojta\n 5.Někdo jiný"));
		
			Console.WriteLine($"Statistika ke hře {this.Name} byla přidána.");
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
				stringBuilder.Append("Nejčastější výherce hry".PadRight(40, '.') + GameStatistics.StatisticsMostOftenWinner(this) + "\n");
				stringBuilder.Append("Hry podle vítěze\n".ToUpperInvariant());
				stringBuilder.Append(GameStatistics.StatisticsWinnerFrequency(this) + "\n");
			}
			return stringBuilder.ToString();

		}
	}
}
