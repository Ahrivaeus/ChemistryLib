using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemistry
{
	public class Measurement
	{
		public double Value { get; set; }
		public string Unit { get; set; }

		public Measurement(double value, string unit)
		{
			Value = value;
			Unit = unit;
		}
	}
}
