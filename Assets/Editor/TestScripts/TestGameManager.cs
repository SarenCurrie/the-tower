using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

/// <summary>
/// 
/// Tests the GameManager class.
/// 
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
