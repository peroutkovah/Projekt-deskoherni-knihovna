using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace HomeGaming
{
	public class GamesAtHome
	{

		public Dictionary<string, Game> gameDictionary = new Dictionary<string, Game>();

		public GamesAtHome()
		{
		
		}

		//metoda vytvoří hru na základě typu - 1=competitive, 2=cooperative, 3=SMART
		public Game CreateNewGame(int gameType)
		{
			
			GameType newGtype = (GameType)gameType;
			string newGname = Validation.StringInputCheck("Jméno hry:");
			while (GameExistCheck(newGname))
			{
				newGname = Validation.StringInputCheck("Napiš údaj znovu - Jméno hry:");
			}
			int newGrcmAge = Validation.IntegerInputCheck("Doporučený věk:");
			int newGavgLength = Validation.IntegerInputCheck("Průměrná délka hraní (v minutách):");
			int newGminNumPl =0;
			int newGmaxNumPl =0;
			string newGdistribution =string.Empty;
			GameCategory newGcategory = GameCategory.Nedefinovano;
			int newGrcmMaxLevel=0;
			if (gameType == 1 || gameType == 2)
			{
				newGminNumPl = Validation.IntegerInputCheck("Minimální počet hráčů:");
				newGmaxNumPl = Validation.IntegerInputCheck("Maximální počet hráčů:");
				newGdistribution = Validation.StringInputCheck("Distributor hry:");
				newGcategory = Validation.CheckGameCategory("Kategorie hry (zadej pouze číslo požadované kategorie)\n" +
																			"Abstraktni = 1\n" +
																			"Rodinna = 2\n" +
																			"Strategicka = 3\n" +
																			"Znalostni = 4\n" +
																			"Party = 5\n" +
																			"Legacy = 6");
			}
			if (gameType == 3)
			{
				newGrcmMaxLevel = Validation.IntegerInputCheck("Nejvyšší level hry:");
			}
			decimal newGprice = Validation.DecimalInputCheck("Cena v Kč:");
			string newGevaluation = Validation.StringInputCheck("Hodnocení hry (pokud zatím nevíš, tak napiš Nevim):");
			
				if (gameType == 1)
				{
					Game newGame = new CompetitiveGame(newGname, newGrcmAge, newGminNumPl, newGmaxNumPl, newGavgLength, newGdistribution, newGcategory, newGprice, newGevaluation);
					return newGame;
				}
				else if (gameType == 2)
				{
					Game newGame = new CooperativeGame(newGname, newGrcmAge, newGminNumPl, newGmaxNumPl, newGavgLength, newGdistribution, newGcategory, newGprice, newGevaluation);
					return newGame;
				}
				else 
				{
				Game newGame = new SmartGame(newGname, newGrcmAge, newGavgLength, newGrcmMaxLevel, newGprice, newGevaluation);
				return newGame;
			}
		}
			

		//metoda pro kontrolu, zda hra jiz existuje
		public bool GameExistCheck(string name)
		{
			bool keyExists = gameDictionary.ContainsKey(name);
			if (keyExists)
			{
				Console.WriteLine($"Tato hra se jmenem {name} již ve sbírce existuje!");
			}
			return keyExists;
		}

		//metoda přidá hru do sbírky
		public void AddGameToHome(Game newGame)
			{
			    if (!GameExistCheck(newGame.Name))
				{

				gameDictionary.Add(newGame.Name, newGame);
				Console.WriteLine($"Do sbírky byla přidána hra {newGame.Name}");
				}
				else 
				{
				Console.WriteLine($"Hru {newGame.Name} nebylo možné přidat do sbírky, protože v ní již existuje.");
				}				
			}

		//metoda prochází hry ve sbírce a vypisuje je na konzoli
		public void ShowGamesAtHome(bool longList)
		{
			if (gameDictionary.Any())
			{
				Console.WriteLine("\nTady je sbírka tvých her:");
				foreach (var game in gameDictionary)
				{
					//pokud bude argument true, tak se vypise dlouhy seznam her i s jejich popisem
					if (longList==true)
					{
						Console.WriteLine(game.Value.ToString()+"\n");
					}
					//pokud bude argument false, tak se vypisi jen jmena her a jejich typ
					else
					{
						Console.WriteLine(game.Value.ShortList());
					}
				}
			}
			else
			{
				Console.WriteLine("Ve tvé sbírce není žádná hra.");
			}
			
		}

		//metoda pro vybrani hry k editaci nebo na smazani nebo pro zadani statistiky
		public Game ChooseGameFromGames(string inquiry)
		{
			ShowGamesAtHome(false);
			string input = "";
						
			if (gameDictionary.Any())
			{
				while (!gameDictionary.ContainsKey(input))
				{
					Console.WriteLine(inquiry);
					input = Console.ReadLine();
					Validation.EndProgramCheck(input);
					if (!gameDictionary.ContainsKey(input))
					{
						Console.WriteLine($"Hra s názvem {input} ve sbírce her není.");
					}											
				}
			}
			
			Game chosenGame = gameDictionary[input];
			return chosenGame;

		}
		//metoda pro smazani hry
		public void DeleteGameAtHome(Game gameToDelete)
		{
			if (gameDictionary.ContainsKey(gameToDelete.Name))
			{
				gameDictionary.Remove(gameToDelete.Name);
				Console.WriteLine($"Ze sbírky byla odstraněna hra {gameToDelete.Name}");
			}
			else
			{
				Console.WriteLine($"Hru {gameToDelete.Name} nebylo možné ze sbírky smazat, protože v ní již existuje.");
			}
		}
		//metoda pro editaci hry
		public void EditGameAtHome(Game gameToEdit)
		{
			if (gameDictionary.ContainsKey(gameToEdit.Name))
			{
				if (gameToEdit.Type == GameType.Kooperativni || gameToEdit.Type == GameType.Kompetitivni)
				{
					int property = Validation.MainInputCheck(8, "Jakou vlastnost hry bys chtěl editovat? Stačí zadat číslo vlastnosti.\n" +
					"1. Doporučený věk\n2. Minimální počet hráčů\n3. Maximální počet hráčů\n4. Průměrná délka hraní (v minutách)\n5. Distributor hry\n6. Kategorie hry\n7. Cenu\n8. Hodnocení hry");
					switch (property)
					{
						case 1:
							{
								Console.WriteLine($"Původní doporučený věk: {gameToEdit.RecommendedAge}");
								gameToEdit.RecommendedAge = Validation.IntegerInputCheck("Změnit na: ");
								Console.WriteLine($"Doporučený věk u hry {gameToEdit.Name} byl změněn na: {gameToEdit.RecommendedAge}");
								break;
							}
						case 2:
							{
								Console.WriteLine($"Původní minimální počet hráčů: {gameToEdit.MinNumberOfPlayers}");
								gameToEdit.MinNumberOfPlayers = Validation.IntegerInputCheck("Změnit na: ");
								Console.WriteLine($"Minimální počet hráčů u hry {gameToEdit.Name} byl změněn na: {gameToEdit.MinNumberOfPlayers}");
								break;
							}
						case 3:
							{
								Console.WriteLine($"Původní maximální počet hráčů: {gameToEdit.MaxNumberOfPlayers}");
								gameToEdit.MaxNumberOfPlayers = Validation.IntegerInputCheck("Změnit na: ");
								Console.WriteLine($"Maximální počet hráčů u hry {gameToEdit.Name} byl změněn na: {gameToEdit.MaxNumberOfPlayers}");
								break;
							}
						case 4:
							{
								Console.WriteLine($"Původní průměrná délka hraní (v minutách): {gameToEdit.AverageLength}");
								gameToEdit.AverageLength = Validation.IntegerInputCheck("Změnit na: ");
								Console.WriteLine($"Průměrná délka hraní (v minutách) u hry {gameToEdit.Name} byla změněn na: {gameToEdit.AverageLength}");
								break;
							}
						case 5:
							{
								Console.WriteLine($"Původní distributor: {gameToEdit.Distribution}");
								gameToEdit.Distribution = Validation.StringInputCheck("Změnit na: ");
								Console.WriteLine($"Distributor u hry {gameToEdit.Name} byla změněn na: {gameToEdit.Distribution}");
								break;
							}
						case 6:
							{
								Console.WriteLine($"Původní kategorie hry: {gameToEdit.Category}");
								gameToEdit.Category = Validation.CheckGameCategory("Změnit na: (zadej pouze číslo požadované kategorie)\n" +
																							"Abstraktni = 1," +
																							"Rodinna = 2, " +
																							"Strategicka = 3, " +
																							"Znalostni = 4, " +
																							"Party = 5, " +
																							"Legacy = 6)");
								Console.WriteLine($"Kategorie u hry {gameToEdit.Name} byla změněn na: {gameToEdit.Category}");
								break;
							}
						case 7:
							{
								Console.WriteLine($"Původní cena: {gameToEdit.Price}");
								gameToEdit.Price = Validation.DecimalInputCheck("Změnit na: ");
								Console.WriteLine($"Cena u hry {gameToEdit.Name} byla změněn na: {gameToEdit.Price}");
								break;
							}
						case 8:
							{
								Console.WriteLine($"Původní hodnocení hry: {gameToEdit.Evaluation}");
								gameToEdit.Evaluation = Validation.StringInputCheck("Změnit na: ");
								Console.WriteLine($"Hodnocení u hry {gameToEdit.Name} byla změněn na: {gameToEdit.Evaluation}");
								break;
							}
					}
						
				}

				else
				{

					int property = Validation.MainInputCheck(4, "Jakou vlastnost hry bys chtěl editovat? Stačí zadat číslo vlastnosti.\n " +
					"1. Doporučený věk\n 2. Průměrná délka hraní (v minutách)\n 3. Cenu\n 4. Hodnocení hry");
					switch (property)
					{
						case 1:
							{
								Console.WriteLine($"Původní doporučený věk: {gameToEdit.RecommendedAge}");
								gameToEdit.RecommendedAge = Validation.IntegerInputCheck("Změnit na: ");
								Console.WriteLine($"Doporučený věk u hry {gameToEdit.Name} byl změněn na: {gameToEdit.RecommendedAge}");
								break;
							}
						
						case 2:
							{
								Console.WriteLine($"Původní průměrná délka hraní (v minutách): {gameToEdit.AverageLength}");
								gameToEdit.AverageLength = Validation.IntegerInputCheck("Změnit na: ");
								Console.WriteLine($"Průměrná délka hraní (v minutách) u hry {gameToEdit.Name} byla změněn na: {gameToEdit.AverageLength}");
								break;
							}
						
						case 3:
							{
								Console.WriteLine($"Původní cena: {gameToEdit.Price}");
								gameToEdit.Price = Validation.DecimalInputCheck("Změnit na: ");
								Console.WriteLine($"Cena u hry {gameToEdit.Name} byla změněn na: {gameToEdit.Price}");
								break;
							}
						case 4:
							{
								Console.WriteLine($"Původní hodnocení hry: {gameToEdit.Evaluation}");
								gameToEdit.Evaluation = Validation.StringInputCheck("Změnit na: ");
								Console.WriteLine($"Hodnocení u hry {gameToEdit.Name} byla změněn na: {gameToEdit.Evaluation}");
								break;
							}
					}
				}
					

			}
		}
		//metoda uloží hry do souboru
		public void SaveGamesToJson(string filePath)
		{

			string directoryPath = Path.GetDirectoryName(filePath);
			WorkWithFiles.CreateDirectory(directoryPath);

			if (gameDictionary.Any())
			{
				
				JsonSerializerSettings settings = new JsonSerializerSettings();
				settings.TypeNameHandling = TypeNameHandling.All;
				settings.Formatting = Newtonsoft.Json.Formatting.Indented;

				string gameDictionaryJson = JsonConvert.SerializeObject(gameDictionary, settings);
				File.WriteAllText(filePath, gameDictionaryJson);
			}
		}
		//metoda nahraje hry ze souboru
		public void LoadGamesFromJson(string filePath)
		{
			if (File.Exists(filePath))
			{
				try 
				{
					var gameDictionaryJson = File.ReadAllText(filePath);
					JsonSerializerSettings settings = new JsonSerializerSettings();
					settings.TypeNameHandling = TypeNameHandling.All;
					settings.Formatting = Newtonsoft.Json.Formatting.Indented;

					gameDictionary = JsonConvert.DeserializeObject<Dictionary<string, Game>>(gameDictionaryJson, settings);
					if (gameDictionary.Any())
					{
						Console.WriteLine($"Proběhlo načtení tvých her v knihovně:\n Hry byly úspěšně načteny ze souboru {filePath}.");
						this.ShowGamesAtHome(false);
					}
					
				}
				catch
				{
					LoadDemoGames(filePath);
				}
				
			}

			else
			{
				LoadDemoGames(filePath);
			}
		}
		//metoda nahraje vytvoří demo hry pro hraní si s programem
		public void LoadDemoGames(string filePath)
		{
			int choice = Validation.MainInputCheck(2, $"Proběhl pokus o načtení tvých her v knihovně, který se nezdařil.\nSoubor s hrami {filePath} buď neexistuje nebo není uložen ve správném formátu." +
					$"Pro 'cvičné hraní si s programem' je ale možné vytvořit několik demo her. Má se tak stát? Pokud ANO zadej číslo 1, pokud NE zadej číslo 2: ");
			if (choice == 1)
			{
				var culture = new CultureInfo("cs-cz");
				this.gameDictionary=new Dictionary<string, Game>();
				CompetitiveGame hra1 = new CompetitiveGame("Ubungo", 8, 2, 5, 35, "Mindok", GameCategory.Abstraktni, 890, "Krása");
				CooperativeGame hra2 = new CooperativeGame("Smaragdový poklad", 5, 2, 4, 15, "Blackfire", GameCategory.Rodinna, 450, "Pro Anežku");
				SmartGame hra3 = new SmartGame("Zámecké schody", 4, 10, 99, 890, "Krása");
				this.AddGameToHome(hra1);
				this.AddGameToHome(hra2);
				this.AddGameToHome(hra3);
				hra1.NumberOfPlays = 7;
				hra1.DateOfGameSeries = new List<DateTime>() { Convert.ToDateTime("01.02.2020",culture), Convert.ToDateTime("17.04.2020",culture), Convert.ToDateTime("30.04.2020",culture),
					 Convert.ToDateTime("21.07.2020",culture), Convert.ToDateTime("28.08.2020",culture), Convert.ToDateTime("21.02.2021",culture), Convert.ToDateTime("17.04.2021",culture) };
				hra1.NumberOfPlayersSeires = new List<int>() { 2, 3, 4, 5, 2, 3, 4 };
				hra1.WinnerSeries = new List<Winner>() { Winner.Anezka, Winner.Anezka, Winner.Anezka, Winner.Mama, Winner.NekdoJiny, Winner.Mama, Winner.Vojta };

				hra2.NumberOfPlays = 10;
				hra2.DateOfGameSeries = new List<DateTime>() { Convert.ToDateTime("01.03.2019",culture), Convert.ToDateTime("18.05.2020",culture), Convert.ToDateTime("12.09.2020",culture),
					Convert.ToDateTime("21.07.2020",culture), Convert.ToDateTime("28.07.2020",culture), Convert.ToDateTime("21.07.2020",culture), Convert.ToDateTime("17.02.2021",culture), Convert.ToDateTime("11.02.2021",culture)
					, Convert.ToDateTime("11.02.2021",culture), Convert.ToDateTime("11.02.2021",culture)};
				hra2.NumberOfPlayersSeires = new List<int>() { 2, 2, 4, 4, 2, 2, 2, 3, 2, 4 };
				hra2.ResultSeries = new List<bool>() { true, true, true, true, true, true, true, true, true, false };

				hra3.NumberOfPlays = 12;
				hra3.DateOfGameSeries = new List<DateTime>() { Convert.ToDateTime("01.02.2020",culture), Convert.ToDateTime("07.08.2020",culture), Convert.ToDateTime("05.08.2020",culture),
					Convert.ToDateTime("02.08.2020",culture), Convert.ToDateTime("28.08.2020",culture), Convert.ToDateTime("21.04.2020",culture), Convert.ToDateTime("06.07.2020",culture), Convert.ToDateTime("11.06.2020",culture)
					, Convert.ToDateTime("11.07.2020",culture), Convert.ToDateTime("11.12.2020",culture), Convert.ToDateTime("02.12.2020",culture)};
				hra3.ReachedLevelSeries = new List<int>() { 56, 26, 11, 63, 70, 89, 90, 25, 12, 90, 96, 23 };
			}
		}
	}
}
