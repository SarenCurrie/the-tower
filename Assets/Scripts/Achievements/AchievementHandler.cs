using UnityEngine;
using System.Collections;

namespace Achievements {
	/**
	 * Stores information regaridng achievements, persists across games.
	 */
	public class AchievementHandler : MonoBehaviour {
		private static AchievementHandler instance = null;
		public static AchievementHandler Instance {
			get { return instance; }
		}
		private int kills;
		private float totalDamage;
		private int totalBlood;
		private int roomsVisited;
		public AchievementFactory achievementFactory;

		void Awake() {
			kills = 0;
			totalDamage = 0;
			totalBlood = 0;
			roomsVisited = 0;
			achievementFactory = new AchievementFactory();
			if (instance != null && instance != this) {
				Destroy(this.gameObject);
				return;
			} else {
				instance = this;
			}
			DontDestroyOnLoad(this.gameObject);
		}

		public void AddKill() {
			kills++;

			if (kills >= 50) {
				achievementFactory.GetAchievements()["KILL_FIFTY"].Achieve();
			}
			else if (kills >= 1) {
				achievementFactory.GetAchievements()["KILL_ONE"].Achieve();
			}
		}

		public void AddScore() {
			int score = UIController.GetUI().GetScoreManager().score;

			if (score > 10000) {
				achievementFactory.GetAchievements()["SCORE_TEN_THOUSAND"].Achieve();
			}
		}
	}
}