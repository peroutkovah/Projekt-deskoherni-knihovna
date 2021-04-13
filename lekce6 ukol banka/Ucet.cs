using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekce6_ukol_banka
{
	class Ucet : IUcet
	{
		public int Zustatek { get; set; }


		string vlastnik;

		public string Vlastnik
		{
			get 
			{ 
				return this.vlastnik; 
			}
			set 
			{ 
				this.vlastnik = value; 
			}
		}
		

		public Ucet(string novyVlastnik, int pocatecniZustatek)
		{
			Zustatek=pocatecniZustatek;
			Vlastnik= novyVlastnik;
		}

		public Ucet(string novyVlastnik)
		{
			
			Vlastnik = novyVlastnik;
		}






	}
}
