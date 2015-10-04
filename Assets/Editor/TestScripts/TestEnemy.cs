using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using theTower;

[TestFixture]
public class TestEnemy {
	ScriptInstantiator instantiator = new ScriptInstantiator();


    [Test]
    public void TestCreateEnemy()
    {
        Player enemy = instantiator.InstantiateScript<Player>();

        Assert.IsNotNull(enemy);

    }

    /*
    [Test]
    public void TestEnemyrStartingMovementSpeed()
    {
        float desiredMovementSpeed = 20.0f;
        //TODO
        Player player = instantiator.InstantiateScript<Player>();
        float movementSpeed = player.movementSpeed;
        Assert.AreEqual(desiredMovementSpeed, movementSpeed);

    }
    */
    //TODO
    [Test]
    public void TestEnemyStartsWithWeapon()
    {
        //TODO
        Enemy enemy = instantiator.InstantiateScript<Enemy>();
        GameObject weapon= enemy.weapon;
        //Weapon weapon1 = weapon1Object.GetComponent<Weapon>();
        //Assert.IsNotNull(weapon1Object);

    }
	/*[Test]
	public void TestMaxHealth() {
		Enemy enemy = instantiator.InstantiateScript<Enemy>();

        Health health = enemy.gameObject.GetComponent<Health>();

        Assert.AreEqual(100, health.health);
	}
    
	[Test]
	public void TestLosingNegativeHealth() {
		Enemy enemy = instantiator.InstantiateScript<Enemy>();
		enemy.GetComponent<Health>().health = 50;
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
	}*/
}
