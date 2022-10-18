using System;
using System.Collections.Generic;
using System.Text;


namespace HomeGaming
{
	public class SmartGame: Game 
	{
		public List<int> ReachedLevelSeries { get; set; }
		public override GameType Type
		{
			get
			{
				return GameType.Smarthra;
			}
		}


		public SmartGame(string name, int recommendedAge, int averageLength, int maxLevel, decimal price, string evaluation) : base(name, recommendedAge, averageLength, maxLevel, price, evaluation)
		{
			//MinNumberOfPlayers = 1;
			//MaxNumberOfPlayers = 1; 
			//Distribution = "MINDOK"; 
			//Category = GameCategory.Abstraktni; 
			ReachedLevelSeries = new List<int>();
		}

		public override string ToString()
		{
			return String.Format("{0} hra - {1}, {2}, věk: {3}+, délka: {4} minut, cena: {5} Kč, hodnocení: {6} ",
				this.Type, this.Name, this.Category, this.RecommendedAge, this.AverageLength, this.Price, this.Evaluation);
		}
		
		public override void AddGameStatistics()
		{
			base.AddGameStatistics();

			ReachedLevelSeries.Add(Validation.CheckMaxLevelSmartGame("Jakého levelu bylo dosaženo?",this.MaxLevel));

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
				stringBuilder.Append("Průměrný dosažený level".PadRight(40, '.') + GameStatistics.StatisticsAverageReachedLevel(this) + "\n");
				stringBuilder.Append("Nejvyšší dosažený level".PadRight(40, '.') + GameStatistics.StatisticsMaxReachedLevel(this) + "\n");
			}
			return stringBuilder.ToString();

		}

	}
}
