using System;
using System.Collections.Generic;
using System.Text;

namespace HomeGaming
{
	public enum GameCategory
	{
		Nedefinovano = 0,
		Abstraktni = 1, 
		Rodinna = 2,
		Strategicka = 3,
		Znalostni = 4,
		Party = 5,
		Legacy = 6,
	}

	public enum GameType
	{
		Nedefinovano = 0,
		Kompetitivni = 1,
		Kooperativni = 2,
		Smarthra = 3
	}

	public enum Winner
	{
		Nedefinovano = 0,
		Tata =1,
		Mama=2,
		Anezka=3,
		Vojta=4,
		NekdoJiny=5
	}

	public abstract class Game
	{
		public abstract  GameType Type { get; }
		public string Name { get; }
		public int RecommendedAge { get; set; } 
		public int MinNumberOfPlayers { get; set; } 
        public int MaxNumberOfPlayers { get; set; } 
		public int AverageLength { get; set; }
		public string Distribution { get; set; }
		public GameCategory Category { get; set; }
		public int MaxLevel { get; set; }
		public decimal Price { get; set; }
		public string Evaluation { get; set; }
		public List<DateTime> DateOfGameSeries { get; set; }
		public int NumberOfPlays { get; set; } = 0;

		public Game (string name, int recommendedAge, int minNumberOfPlayers, int maxNumberOfPlayers, int averageLength, string distribution, GameCategory category, decimal price, string evaluation)
		{
			Name =name;
			RecommendedAge= recommendedAge;
			MinNumberOfPlayers= minNumberOfPlayers;
			MaxNumberOfPlayers= maxNumberOfPlayers;
			AverageLength= averageLength;
			Distribution= distribution;
			Category= category;
			Price= price;
			Evaluation= evaluation;
			DateOfGameSeries = new List<DateTime>();
		}

		public Game(string name, int recommendedAge, int averageLength, int maxLevel, decimal price, string evaluation)
		{
			Name = name;
			RecommendedAge = recommendedAge;			
			AverageLength = averageLength;
			MaxLevel= maxLevel;
			Price = price;
			Evaluation = evaluation;
			DateOfGameSeries=new List<DateTime>();
		}

		public override string ToString()
		{
			return String.Format("{0} hra - {1}, {2}, pro {3} - {4} hráčů, věk: {5}+, délka: {6} minut, vydalo: {7}, cena: {8} Kč, hodnocení: {9} ",  
				Type, Name, Category, MinNumberOfPlayers, MaxNumberOfPlayers, RecommendedAge, AverageLength, Distribution, Price, Evaluation );
		}

		public string ShortList()
		{
			return String.Format("{0} hra - {1}",
				Type, Name);
		}

		public virtual void  AddGameStatistics()
		{
			Console.WriteLine($"------ Statistika pro hru {this.Name}  -----");

			DateOfGameSeries.Add(Validation.Gamedate($"Kdy byla hra {this.Name} odehrána (napiš datum ve formátu DD.MM.YYYY)"));

			NumberOfPlays = ++NumberOfPlays;

		}

		public virtual string GameStatisticsOverview()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (NumberOfPlays == 0)
			{
				stringBuilder.Append($"\nPro hru {Name} nebyla odehrána ještě ani jedna partie, proto není možné zobrazit její statistiku!\n");
			}
			else
			{
				stringBuilder.Append($"\n----- Statistika pro hru {Name} -----\n".ToUpperInvariant());
				stringBuilder.Append("Počet odehraných her celkem".PadRight(40, '.') + GameStatistics.StatisticsNumberOfPlays(this)+"\n");
				stringBuilder.Append("Hra byla poprvé odehrána:".PadRight(40, '.') + GameStatistics.StatisticsFirstDayofPlay(this) + "\n");
				stringBuilder.Append("Hra byla naposledy odehrána:".PadRight(40, '.') + GameStatistics.StatisticsLastDayofPlay(this) + "\n\n");
				stringBuilder.Append("Počet odehraných her v jednotlivých letech:" + "\n");
				stringBuilder.Append(GameStatistics.StatisticsNumberOfPlaysPerYear(this) + "\n");
				stringBuilder.Append("Počet odehraných her v jednotlivých měsícíh (jednoduchý graf):" + "\n");
				stringBuilder.Append(GameStatistics.StatisticsNumberOfPlaysPerMonth(this) + "\n");			
			}
			return String.Format(stringBuilder.ToString());
		}
	}
}
