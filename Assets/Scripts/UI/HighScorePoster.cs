﻿using UnityEngine;
using System.Collections;
using HighScores;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///
/// This class implements the HighScoreConsumers posting callback,
/// ensuring the UI is updated after a post.
/// 
/// </summary>
public class HighScorePoster : HighScoreConsumer
{
	
	override protected void HighScoreGetCallback(List<HighScore> scores){
		// Do nothing
	}
	
	override protected void HighScorePostCallback(string data){
		UIController.GetUI().donePosting();
	}
	
}
