using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

/// <summary>
/// 
/// Tests the UIController class.
///
/// </summary>
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

    [Test]
    public void TestUIFixedResolution()
    {
        UIController uiController = instantiator.InstantiateScript<UIController>();
        Assert.AreEqual(768f,UIController.targetHeight);
        Assert.AreEqual(1024f,UIController.targetWidth);

    }

     [Test]
    public void TestUIGUIMatrix()
    {
        UIController uiController = instantiator.InstantiateScript<UIController>();
        Matrix4x4 matrix = UIController.GetGUIMatrix();
        Assert.IsNotNull(matrix);

    }



}
