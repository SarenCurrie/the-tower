using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;


/// <summary>
/// 
/// Tests the Enemy class.
///
/// </summary>
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
