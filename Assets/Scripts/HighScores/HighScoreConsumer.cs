using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HighScores
{
	/**
	 * Consumer for the High Score API
	 */
	public abstract class HighScoreConsumer : MonoBehaviour
	{
		private List<HighScore> highScores = new List<HighScore>();
		private string name;
		private int score;

		public void Refresh()
		{
			StartCoroutine(UpdateHighScores());
		}

		/**
		 * Asyncronously gets highscores from the server and updates the list.
		 */
		IEnumerator UpdateHighScores()
		{
			string highScoresJson;

			WWW www = new WWW("http://test.sarencurrie.com/v1/scores");
			yield return www;
			highScoresJson = www.text;
			JSONObject obj = new JSONObject(highScoresJson);
			highScores = new List<HighScore>(10);
			accessData(obj);
			HighScoreGetCallback(highScores);
		}

		/**
		 * Reads JSONObject into a list of HighScores.
		 */
		private void accessData(JSONObject obj)
		{
			switch(obj.type){
				case JSONObject.Type.OBJECT:
					bool isValue = false; // used to avoid adding the first level of data
					string n = null;
					int s = 0;
					for(int i = 0; i < obj.list.Count; i++){
						string key = (string)obj.keys[i];
						JSONObject j = (JSONObject)obj.list[i];
						if (key == "data")
							accessData(j);
						else if (key == "name")
						{
							n = j.str;
							isValue = true;
						}
						else if (key == "score")
						{
							s = (int) j.n;
							isValue = true;
						}
					}
					if (isValue)
						highScores.Add(new HighScore(n, s));
					break;
				case JSONObject.Type.ARRAY:
					foreach(JSONObject j in obj.list){
						accessData(j);
					}
					break;
				case JSONObject.Type.STRING:
					Debug.Log(obj.str);
					break;
				case JSONObject.Type.NUMBER:
					Debug.Log(obj.n);
					break;
				case JSONObject.Type.BOOL:
					Debug.Log(obj.b);
					break;
				case JSONObject.Type.NULL:
					Debug.Log("NULL");
					break;

			}
		}

		public void NewHighScore(string n, int s)
		{
			name = n;
			score = s;
			StartCoroutine(PostHighScore());
		}

		IEnumerator PostHighScore()
		{
			string ourPostData = "{\"name\":\"" + name + "\", \"score\": " + score + "}";

			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers.Add("Content-Type", "application/json");

			byte[] pData = Encoding.UTF8.GetBytes(ourPostData.ToCharArray());

			WWW www = new WWW("http://test.sarencurrie.com/v1/scores", pData, headers);
			yield return www;
			HighScorePostCallback(www.data);
		}

		protected abstract void HighScoreGetCallback(List<HighScore> scores);
		protected abstract void HighScorePostCallback(string data);
	}
}
