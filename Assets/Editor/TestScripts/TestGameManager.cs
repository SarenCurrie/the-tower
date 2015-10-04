using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

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
