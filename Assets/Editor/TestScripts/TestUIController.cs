using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class TestUIController
{
    ScriptInstantiator instantiator = new ScriptInstantiator();

    [Test]
    public void TestCreateUIController()
    {

        UIController uiController = instantiator.InstantiateScript<UIController>();
        Assert.IsNotNull(uiController);

    }

}
