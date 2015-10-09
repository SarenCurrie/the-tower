using UnityEngine;
using System.Collections;

namespace Achievements {
	/**
	 * Stores information regaridng achievements
	 */
	public class AchievementHandler {
		private static int kills;
		private static float totalDamage;
		private static int totalBlood;
		private static int roomsVisited;

		public static void AddKill() {
			kills++;
			if (kills > 50) {
				AchievementFactory.KILL_FIFTY_ENEMIES.Achieve();
			}
			else if (kills > 1) {
				AchievementFactory.KILL_ONE_ENEMY.Achieve();
			}
		}

		public static void AddScore() {
			int score = GameManager.GetPlayer().GetComponent<Player>().score;

			if (score > 10000) {
				AchievementFactory.SCORE_TEN_THOUSAND.Achieve();
			}
		}
	}
}