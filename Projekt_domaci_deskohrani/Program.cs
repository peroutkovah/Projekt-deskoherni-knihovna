using System;
using System.IO;
using System.Linq;

namespace HomeGaming
{

	class Program
	{
		static void Main(string[] args)
		{

			try
			{

				GamesAtHome gamesAtHome = new GamesAtHome();
				string separator = new string('-', 10);

				string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HomeGaming", "games.json");

				Console.WriteLine($"{separator} VÍTEJ V DOMÁCÍM DESKOHRANÍ  {separator}");

				gamesAtHome.LoadGamesFromJson(filePath);

				var input = string.Empty; 
				while (!Validation.EndProgramCheck(input)) 
				{
						if (gamesAtHome.gameDictionary.Any())
						{
							int mainChoice = WorkWithConsole.MainInput();
							switch (mainChoice)
							{case 1:
									int choice = WorkWithConsole.GamesAtHomeManagement();
									switch (choice)
									{
										case 1:
											WorkWithConsole.GamesAtHomeAdd(gamesAtHome, filePath);
											break;

										case 2:
											WorkWithConsole.GamesAtHomeEdit(gamesAtHome, filePath);
											break;

										case 3:
											WorkWithConsole.GamesAtHomeDelete(gamesAtHome, filePath);
											break;
									}
									WorkWithConsole.BigSeparator();
									break;

								case 2:
									WorkWithConsole.GamesAtHomeShow(gamesAtHome, filePath);
									break;

								case 3:
									WorkWithConsole.GamesAtHomeAddStatistics(gamesAtHome, filePath);
									break;

								case 4:
									WorkWithConsole.GamesAtHomeShowStatistics(gamesAtHome, filePath);
									break;
								default:
									Console.WriteLine($"Neočekávaná hodnota od uživatele ({mainChoice})");
								break;
							}
						}
						else
						{
							int choice = WorkWithConsole.GamesAtHomeWithoutLoading();
							switch (choice)
							{
								case 1:
									WorkWithConsole.GamesAtHomeAdd(gamesAtHome, filePath);
									break;


								case 2:
									Validation.EndProgramCheck("x");
									break;
								default:
									Console.WriteLine($"Neočekávaná hodnota od uživatele ({choice})");
									break;
						}
							WorkWithConsole.BigSeparator();
						}
				}
				gamesAtHome.SaveGamesToJson(filePath);
				return;
			}

			catch (Exception ex)

			{
				Console.WriteLine($"Došlo k neočekávané chybě {ex.Message}. Kontaktujte správce programu!");
				string fileError = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HomeGaming", "gameslog.json"); //Moc pekne, mas i logovani! :) Palec nahoru :)
				WorkWithFiles.CreateDirectory(Path.GetDirectoryName(fileError));

				using (StreamWriter writer = new StreamWriter(fileError, append: true))
				{
					writer.WriteLine("Neočekávaná chyba:");
					writer.WriteLine(DateTime.Now.ToString());
					writer.WriteLine(ex.Message);
				}

			}
		}

	}
}
