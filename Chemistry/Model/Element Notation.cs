using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemistry.Atomic
{
	public class ElementNotation
	{
		private int atomicNumber;
		private string symbol;
		private string name;

		public int AtomicNumber
		{
			get { return atomicNumber; }
			private set { atomicNumber = value; }
		}

		public string Symbol
		{
			get { return symbol; }
			private set { symbol = value; }
		}

		public string Name
		{
			get { return name; }
			private set { name = value; }
		}

		public ElementNotation(int atomicNumber, string symbol, string name)
		{
			AtomicNumber = atomicNumber;
			Symbol = symbol;
			Name = name;
		}

	}
}
