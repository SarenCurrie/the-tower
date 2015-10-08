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
	}
}