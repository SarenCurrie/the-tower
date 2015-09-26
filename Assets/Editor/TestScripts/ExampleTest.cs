using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ExampleTest {
	[Test]
	public void TestSomething() {
		int asdf = 100;

		Assert.AreEqual(100, asdf);
	}
}
