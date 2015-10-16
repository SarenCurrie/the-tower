﻿using UnityEngine;
using System.Collections;
using HighScores;
using System.Collections;
using System.Collections.Generic;

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
