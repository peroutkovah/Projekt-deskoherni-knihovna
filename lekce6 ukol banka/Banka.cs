using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekce6_ukol_banka
{
	class Banka
	{
		public List<Ucet> uctyBanky=new List<Ucet>();
		public void ZalozUcet(string jmenoVlastnik,int pocatecniZustatek)
		{
			IUcet novyUcet= new Ucet(jmenoVlastnik, pocatecniZustatek);
			uctyBanky.Add(novyUcet as Ucet);
		}

		//Vytvořte bance funkci NajdiUcet, která vrátí IUcet podle jména vlastníka.
		public IUcet NajdiUcet(string jmenoVlastnika)
		{
			Ucet hledanyUcet = uctyBanky.Find(ucet => ucet.Vlastnik.Equals(jmenoVlastnika));
			return hledanyUcet;
		}

		// Vytvořte bance funkci UložPeníze, která přidá do účtu vlastníka odpovídající obnos.
		public void UlozPenize(Ucet ucetProUlozeni, int novePenize)
		{
			ucetProUlozeni.Zustatek = ucetProUlozeni.Zustatek + novePenize;
		}

	}
}
