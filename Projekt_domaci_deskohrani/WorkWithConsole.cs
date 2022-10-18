using System;


namespace HomeGaming
{
	public static class WorkWithConsole
	{
		public static void BigSeparator() 
		{
			string bigSeparator = new('*', 60);
			Console.WriteLine(bigSeparator);

		}
		public static void ProgramEndNote()
		{
			Console.WriteLine("\nProgram můžeš kdykoliv ukončit zadáním x".ToUpperInvariant());
			
		}
		public static int MainInput()
		{
			int choice = Validation.MainInputCheck(4, "\nCo bys chtěl/a dělat? Vyber ze 4 možností (stačí napsat číslo 1 nebo 2 nebo 3 nebo 4). \n 1. Spravovat sbírku\n 2. Prohlédnout si sbírku \n 3. Zadat statistiku pro hru \n 4. Podívat se na statistiku ke hře");
			BigSeparator();
			return choice;

		}
		public static int GamesAtHomeManagement()
		{
			Console.WriteLine("Vítej v části programu pro Správu sbírky".ToUpperInvariant());
			int choice = Validation.MainInputCheck(3, "\nCo bys chtěl/a dělat? Vyber ze 3 možností (stačí napsat číslo 1 nebo 2 nebo 3). \n 1. Přidat novou hru do sbírky \n 2. Editovat hru ve sbírce \n 3. Smazat hru ze sbírky");
			BigSeparator();
			return choice;
		}
		public static void GamesAtHomeAdd(GamesAtHome gamesAtHome, string filePath)
		{
			Console.WriteLine("\nZadej následující informace o hře:".ToUpperInvariant());
			int newGtype = Validation.MainInputCheck(3, "\nO jakou hru se jedná (stačí napsat číslo 1 nebo 2 nebo 3):\n 1. Kompetitivní\n 2. Kooperativní\n 3.SMARThru");
			Game newGame = gamesAtHome.CreateNewGame(newGtype);
			gamesAtHome.AddGameToHome(newGame);
			gamesAtHome.SaveGamesToJson(filePath);
		}
		public static void GamesAtHomeEdit(GamesAtHome gamesAtHome, string filePath)
		{
			gamesAtHome.EditGameAtHome(gamesAtHome.ChooseGameFromGames("\nJakou hru chceš editovat? Napiš jméno z tohoto seznamu:\n"));
			gamesAtHome.SaveGamesToJson(filePath);
		}
		public static void GamesAtHomeDelete(GamesAtHome gamesAtHome, string filePath)
		{
			gamesAtHome.DeleteGameAtHome(gamesAtHome.ChooseGameFromGames("\nJakou hru chceš smazat? Napiš jméno z tohoto seznamu:\n"));
			gamesAtHome.SaveGamesToJson(filePath);
		}
		public static void GamesAtHomeShow(GamesAtHome gamesAtHome, string filePath)
		{
			gamesAtHome.ShowGamesAtHome(true);
			WorkWithConsole.BigSeparator();
		}
		public static void GamesAtHomeAddStatistics(GamesAtHome gamesAtHome, string filePath)
		{
			Console.WriteLine("\nVítej v části programu pro Zadávání statistiky pro hru".ToUpperInvariant());
			gamesAtHome.ChooseGameFromGames("\nJaká hra byla odehrána pro přidání statistiky? Napiš jméno z tohoto seznamu:").AddGameStatistics();
			gamesAtHome.SaveGamesToJson(filePath);
			WorkWithConsole.BigSeparator();
		}
		public static void GamesAtHomeShowStatistics(GamesAtHome gamesAtHome, string filePath)
		{
			Console.WriteLine("\nVítej v části programu pro Sledování statistiky pro hru".ToUpperInvariant());
			Console.WriteLine(gamesAtHome.ChooseGameFromGames("\nPro jakou hru má být zobrazena statistika? Napiš jméno z tohoto seznamu:").GameStatisticsOverview());
			WorkWithConsole.BigSeparator();
		}
		public static int GamesAtHomeWithoutLoading()
		{
			int choice = Validation.MainInputCheck(2, $"\nChceš přidat do sbírky novou hru? Stačí zadat číslo 1 pro ANO " +
									$"nebo číslo 2 pro NE (POZOR program se ukončí)");
			BigSeparator();
			return choice;
		}

	}
}
