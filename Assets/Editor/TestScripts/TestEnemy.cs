using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class TestEnemy {
	ScriptInstantiator instantiator = new ScriptInstantiator();

	/**[Test]
	public void TestMaxHealth() {
		Enemy enemy = instantiator.InstantiateScript<Enemy>();
        Health health = enemy.GetComponent<Health>();
		health.maxHealth = 50;

		Assert.AreEqual(50, health.maxHealth);
	}

	[Test]
	public void TestLosingNegativeHealth() {
		Enemy enemy = instantiator.InstantiateScript<Enemy>();
		enemy.maxHealth = 50;
		enemy.LoseHealth(-100);

		Assert.AreEqual(enemy.maxHealth, enemy.GetHealth());
	}

	[Test]
	public void TestLosingHealth() {
		Enemy enemy = instantiator.InstantiateScript<Enemy>();
		enemy.maxHealth = 50;
		enemy.LoseHealth(-enemy.maxHealth);
		enemy.LoseHealth(5);

		Assert.AreEqual(enemy.maxHealth - 5, enemy.GetHealth());
	}

	[Test]
	public void TestDeath() {
		Enemy enemy = instantiator.InstantiateScript<Enemy>();
		enemy.maxHealth = 50;
		enemy.LoseHealth(-enemy.maxHealth);
		enemy.LoseHealth(50);

		Assert.AreEqual(enemy.GetHealth(), 0);
	}**/
}
