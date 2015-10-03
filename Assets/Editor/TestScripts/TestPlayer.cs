using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using theTower;

[TestFixture]
public class TestPlayer
{
    ScriptInstantiator instantiator = new ScriptInstantiator();


    [Test]
    public void TestCreatePlayer()
    {
        Player player = instantiator.InstantiateScript<Player>();

        Assert.IsNotNull(player);

    }

    [Test]
    public void TestPlayerDefaultStats()
    {
        Player player = instantiator.InstantiateScript<Player>();
        int strength = player.GetStrength();
        int intelligence = player.GetIntelligence();
        int dexterity = player.GetDexterity();

        Assert.AreEqual(1, strength);
        Assert.AreEqual(1, intelligence);
        Assert.AreEqual(1, dexterity);

    }


    [Test]
    public void TestPlayerStartsWithWeapon()
    {
        //TODO
        Player player = instantiator.InstantiateScript<Player>();
        GameObject weapon1Object = player.weapon2 ;
        //Weapon weapon1 = weapon1Object.GetComponent<Weapon>();
        //Assert.IsNotNull(weapon1Object);

    }

    public void TestPlayerStartingMovementSpeed()
    {
        //TODO
        Player player = instantiator.InstantiateScript<Player>();
        GameObject weapon1Object = player.weapon2;
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
