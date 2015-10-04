using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class TestScoreManager
{
    ScriptInstantiator instantiator = new ScriptInstantiator();

    [Test]
    public void TestCreateScoreManager()
    {

        ScoreManager scoreManager = instantiator.InstantiateScript<ScoreManager>();

        Assert.IsNotNull(scoreManager);

    }






}