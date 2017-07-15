using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Chemistry.Atomic;

namespace Chemistry.Control
{
	internal static class TableHandler
	{
		//These are the 'databases' in which I store the information I need, and from which I pull that information.
		internal static List<ElementNotation> elementNotations = new List<ElementNotation>();
		internal static JArray periodicTable;

		static TableHandler()
		{
			BuildResources();
		}

		private static void BuildResources()
		{
			BuildNotations();
			BuildTable();
		}
		private static void BuildNotations()
		{
			//The elementlist is a .csv file, so I used string splitting to convert it into a more managable data format. There may have been a better way, but this seems okay.
			string[] notationLines = Chemistry.Properties.Resources.elementlist.Split('\n');
			for (int i = 0; i < notationLines.Length; i++)
			{
				string[] values = notationLines[i].Split(',');
				int atomicNumber = int.Parse(values[0]);
				string atomicSymbol = values[1];
				string atomicName = values[2].Replace('\r', ' ').Trim();
				ElementNotation newElement = new ElementNotation(atomicNumber, atomicSymbol, atomicName);
				elementNotations.Add(newElement);
			}
		}
		private static void BuildTable()
		{
			//This was the hardest part of the project for me so far. I'd never worked with JSON or Newtonsoft's library before.
			//This line is essentially going into the JSON object, going in a few steps to get to the meat of the data, and then extracting it into a JArray.
			//Once I have a JArray, I can query it with C#'s LINQ library, which is super handy.
			periodicTable = (JArray)JObject.Parse(System.Text.Encoding.UTF8.GetString(Chemistry.Properties.Resources.jquery_pte))["PERIODIC_TABLE"]["ATOM"];
		}
	}
}
