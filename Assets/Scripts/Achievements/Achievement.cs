using UnityEngine;
using System.Collections;

namespace Achievements {
	public class Achievement : MonoBehaviour {
		public string name;
		public string text;
		public bool hasAchieved = false;

		public Achievement(string n, string t) {
			name = n;
			text = t;
		}

		public void Achieve() {
			AchievementManager.DoAchievement(this);
		}
	}
}