using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chemistry.Atomic;

namespace TestHarness
{
	[TestClass]
	public class Tester
	{
		[TestMethod]
		public void Instantiation()
		{
			Atom atom = new Atom("Ac");
			Assert.AreEqual(atom.AtomicNumber, 89);
		}

		[TestMethod]
		public void ValueReturns()
		{
			Atom atom = new Atom(89);
			Assert.AreEqual(atom.AtomicWeight, 227);
			Assert.AreEqual(atom.OxidationStates, 3);
			Assert.AreEqual(3470, atom.BoilingPoint.Value);
			Assert.IsTrue("Kelvin".Equals(atom.BoilingPoint.Unit, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}
