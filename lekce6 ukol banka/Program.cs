using System;



//1. Vytvořte interface IUcet, který bude mít property Zustatek s get (bez set) a property Vlastnik s get (také bez set)
//2. Vytvořte třídu Ucet, která bude dědit interface IUcet, implementovat properties Zustatek (get i set) a Vlastník, který bude nastaven jen z konstruktoru.
//        Vytvořte konstruktor Uctu, který bude brát jako parametry jméno vlastníka a počáteční zůstatek.
//3. Vytvořte třídu Banka, která bude mít funkci ZalozUcet s parametry počátečního zůstatku a jména vlastníka.
//        Vytvořte bance funkci NajdiUcet, která vrátí IUcet podle jména vlastníka.
//        Vytvořte bance funkci UložPeníze, která přidá do účtu vlastníka odpovídající obnos.
//4. Vytvořte program, který bude mít instanci Banky, vytvoří 3 účty (nemusíte dělat načítání z konzole, klidně v kodu vytvořit)
//5. Vypište aktuální stav účtů v Bance
//6. Uložte do jednoho účtu další peníze a opět vypište aktuální stav účtů
//7. Získejte jeden účet z Banky do proměnné typu IUcet pomocí NajdiUcet.Nesmí mu jít změnit Zustatek, musí jít jedině přes příkaz v bance :)



namespace lekce6_ukol_banka
{
	class Program
	{
		static void Main(string[] args)
		{
			Banka mojeBanka=new Banka();
	

			mojeBanka.ZalozUcet("Fifinka", 1000);
			mojeBanka.ZalozUcet("Bobik", 2000);
			mojeBanka.ZalozUcet("Pinda", 10000);

			foreach (IUcet ucet in mojeBanka.uctyBanky)
			{
				Console.WriteLine($"Účet vlastníka {ucet.Vlastnik} se zůstatkem {ucet.Zustatek}");
			}

			Console.WriteLine("Fifince pridam na ucet 500000");
			mojeBanka.UlozPenize(mojeBanka.NajdiUcet("Fifinka") as Ucet, 50000);
			Console.WriteLine("Vypis po pridani:");
			foreach (Ucet ucet in mojeBanka.uctyBanky)
			{
				Console.WriteLine($"Účet vlastníka {ucet.Vlastnik} se zůstatkem {ucet.Zustatek}");
			}

			IUcet hledanyUcet= mojeBanka.NajdiUcet("Fifinka");
			hledanyUcet.Zustatek = 100;
			





		}
	}
}
