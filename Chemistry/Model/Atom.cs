using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chemistry.Atomic;
using Chemistry.Control;
using Newtonsoft.Json.Linq;

namespace Chemistry.Atomic
{
	public class Atom
    {
		#region Properties

		private int neutrons;

		public int Neutrons
		{
			get { return neutrons; }
			set { neutrons = value; }
		}

		private int protons;

		public int Protons
		{
			get { return protons; }
			set { protons = value; }
		}

		private int electrons;

		public int Electrons
		{
			get { return electrons; }
			set { electrons = value; }
		}

		private JToken atomicData;

		private ElementNotation notation;

		public ElementNotation Notation
		{
			get { return notation; }
			set
			{
				notation = value;
				atomicData = TableHandler.periodicTable.Where(x => x["NAME"].ToString().ToUpper() == this.Name.ToUpper()).FirstOrDefault();
			}
		}

		public int AtomicNumber
		{
			get { return Notation.AtomicNumber; }
			set
			{
				Notation = TableHandler.elementNotations.Where(x => x.AtomicNumber == value).FirstOrDefault();
			}
		}

		public string Name
		{
			get { return Notation.Name; }
			set
			{
				Notation = TableHandler.elementNotations.Where(x => x.Name == value).FirstOrDefault();
			}
		}

		public string Symbol
		{
			get { return Notation.Symbol; }
			set
			{
				Notation = TableHandler.elementNotations.Where(x => x.Name == value).FirstOrDefault();
			}
		}

		public double AtomicWeight
		{
			get { return double.Parse(QueryTable("ATOMIC_WEIGHT")); }
		}

		public int OxidationStates
		{
			get { return int.Parse(QueryTable("OXIDATION_STATES")); }
		}

		/// <summary>
		/// Returns a Measurement object, containing a double value, and a string representing the unit of measurement.
		/// </summary>
		public Measurement BoilingPoint
		{
			get
			{
				return FetchMeasurement("BOILING_POINT");
			}
		}

		/// <summary>
		/// Returns a Measurement object, containing a double value, and a string representing the unit of measurement.
		/// </summary>
		public Measurement Density
		{
			get
			{
				return FetchMeasurement("DENSITY");
			}
		}

		// TODO Impliment electron orbitals.

		public double Electronegativity
		{
			get { return double.Parse(QueryTable("ELECTRONEGATIVITY")); }
		}

		public Measurement AtomicRadius
		{
			get
			{
				return FetchMeasurement("ATOMIC_RADIUS");
			}
		}

		public Measurement AtomicVolume
		{
			get { return FetchMeasurement("ATOMIC_VOLUME"); }
		}

		public Measurement SpecificHeatCapacity
		{
			get { return FetchMeasurement("SPECIFIC_HEAT_CAPACITY"); }
		}

		public double IonizationPotential
		{
			get { return double.Parse(QueryTable("IONIZATION_POTENTIAL")); }
		}

		public Measurement ThermalConductivity
		{
			get { return FetchMeasurement("THERMAL_CONDUCTIVITY"); }
		}
		#endregion

		#region Property Change Listeners



		#endregion

		#region  Constructors

		public Atom(string symbolOrName)
		{
			string[] symbols = TableHandler.elementNotations.Select(x => x.Symbol).ToArray();
			if (symbols.Contains(symbolOrName))
			{
				Notation = TableHandler.elementNotations.Where(x => x.Symbol == symbolOrName).FirstOrDefault();
			}
			else if (TableHandler.elementNotations.Select(x => x.Name).ToArray().Contains(symbolOrName))
			{
				Notation = TableHandler.elementNotations.Where(x => x.Name == symbolOrName).FirstOrDefault();
			}
			else
			{
				throw new KeyNotFoundException("That element name or symbol doesn't exist.");
			}
		}

		public Atom(int number)
		{
			if (TableHandler.elementNotations.Select(x => x.AtomicNumber).Contains(number))
			{
				Notation = TableHandler.elementNotations.Where(x => x.AtomicNumber == number).FirstOrDefault();
			}
			else
				throw new KeyNotFoundException("That atomic number is invalid!");
		}

		#endregion

		#region Methods

		/// <summary>
		/// Queries the JArray containing element information via LINQ to return the specified information. This method indexes by atomic symbol arbitrarily.
		/// </summary>
		/// <param name="field">The data you wish to retrieve, by key. "ALL_CAPS" by default.</param>
		/// <returns>A string containing the information you are querying for.</returns>
		private string QueryTable(string field)
		{
			return TableHandler.periodicTable.Where(x => x["SYMBOL"].ToString().Equals(Symbol, StringComparison.CurrentCultureIgnoreCase)).Select(x => x[field].ToString()).FirstOrDefault();
		}

		/// <summary>
		/// Queries the periodic table and unpacks the returned string containing multiple kvPairs into a Measurement object, which consists of Value and Unit.
		/// </summary>
		/// <param name="field">The field you want from the periodic table.</param>
		/// <returns>string[unit, value]</returns>
		private Measurement FetchMeasurement(string field)
		{
			string rawData = QueryTable(field);
			string[] splitItems = rawData.Split(',');
			string unit = splitItems[0].Split(':')[1].Replace('"', ' ').Replace('{', ' ').Trim();
			string value = splitItems[1].Split(':')[1].Replace('"', ' ').Replace('}', ' ').Trim();
			return new Measurement(double.Parse(value), unit);
		}
		#endregion

		#region Overrides

		//This is temporary. There's probably a better to-string format I could use for this, but I wanted to be able to print something for testing reasons.
		public override string ToString()
		{
			return ("Element: " + Name +
				"\nSymbol: " + Symbol +
				"\nAtomic Number: " + AtomicNumber +
				"\nAtomic Weight: " + AtomicWeight);
		}

		#endregion
	}
}
 
