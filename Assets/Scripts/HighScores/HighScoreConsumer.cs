using UnityEngine;
using System.Collections;

namespace HighScores
{
	public class HighScoreConsumer : MonoBehaviour
	{
		private string highScoresJson;
		private string URL = "http://128.199.101.210:3000/v1/scores";

		void Start()
		{
			Debug.Log("Getting high score data");
			StartCoroutine(GetHighScores());
		}

		IEnumerator GetHighScores() {
			Debug.Log(URL);
			WWW www = new WWW("http://test.sarencurrie.com/v1/scores");
			yield return www;
			highScoresJson = www.text;
			Debug.Log(highScoresJson);
			JSONObject obj = new JSONObject(highScoresJson);
			accessData(obj);
		}

		void accessData(JSONObject obj){
			switch(obj.type){
				case JSONObject.Type.OBJECT:
					for(int i = 0; i < obj.list.Count; i++){
						string key = (string)obj.keys[i];
						JSONObject j = (JSONObject)obj.list[i];
						Debug.Log(key);
						accessData(j);
					}
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
	}
}