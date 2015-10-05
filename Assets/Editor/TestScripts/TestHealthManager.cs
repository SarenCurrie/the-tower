using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class TestHealthManager
{
    ScriptInstantiator instantiator = new ScriptInstantiator();

    [Test]
    public void TestCreateHealthManager()
    {

        healthManager healthManager = instantiator.InstantiateScript<healthManager>();

        Assert.IsNotNull(healthManager);

    }






}