using UnityEngine;
using System.Collections;
using HighScores;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///
/// This class implements the HighScoreConsumers getting callback,
/// ensuring the UI is updated after a get.
/// 
/// </summary>
public class HighScoreGetter : HighScoreConsumer
{
	private HighScoreScreen parent;

	public void setParent(HighScoreScreen myParent){
		parent = myParent;
	}
	
	override protected void HighScoreGetCallback(List<HighScore> scores){
		parent.UpdateHighScores(scores);
	}
	
	override protected void HighScorePostCallback(string data){
		// Do nothing
	}
	
}
