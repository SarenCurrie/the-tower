using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;


/// <summary>
/// 
/// Tests the Player class.
///
/// @author Harry
/// </summary>
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
    public void TestPlayerStartingMovementSpeed()
    {
        float desiredMovementSpeed = 20.0f;
        Player player = instantiator.InstantiateScript<Player>();
        float movementSpeed = player.movementSpeed;
        Assert.AreEqual(desiredMovementSpeed,movementSpeed);

    }

    [Test]
    public void TestPlayerNoStartingArmourSet()
    {
        Player player = instantiator.InstantiateScript<Player>();
        GameObject helm = player.helm;
        GameObject chest = player.chest;
        GameObject gloves = player.gloves;
        GameObject boots = player.boots;

        Assert.Null(helm);
        Assert.Null(chest);
        Assert.Null(gloves);
        Assert.Null(boots);

    }


    



}
