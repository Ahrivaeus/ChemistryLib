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
			periodicTable = (JArray)JObject.Parse(System.Text.Encoding.UTF8.GetString(Chemistry.Properties.Resources.jquery_pte))["PERIODIC_TABLE"]["ATOM"];
		}
	}
}
