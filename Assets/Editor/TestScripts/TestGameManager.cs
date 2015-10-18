using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

/// <summary>
/// 
/// Tests the GameManager class.
/// 
/// @author Harry
/// </summary>
[TestFixture]
public class TestGameManager
{
    ScriptInstantiator instantiator = new ScriptInstantiator();

    [Test]
    public void TestCreateGameManager()
    {

        GameManager gameManager = instantiator.InstantiateScript<GameManager>();
        Assert.IsNotNull(gameManager);

    }

}
