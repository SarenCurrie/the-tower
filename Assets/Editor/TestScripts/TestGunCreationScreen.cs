using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class TestGunCreationScreen
{
    ScriptInstantiator instantiator = new ScriptInstantiator();

    [Test]
    public void TestCreateGunCreationScreen()
    {

        GunCreationScreen gunCreationScreen = instantiator.InstantiateScript<GunCreationScreen>();
        Assert.IsNotNull(gunCreationScreen);
    }

}
