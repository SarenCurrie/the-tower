using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class TestEnemy {
	ScriptInstantiator instantiator = new ScriptInstantiator();


    [Test]
    public void TestCreateEnemy()
    {
        Player enemy = instantiator.InstantiateScript<Player>();

        Assert.IsNotNull(enemy);

    }

   
}
