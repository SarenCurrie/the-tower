using UnityEngine;
using System.Collections.Generic;

namespace Achievements
{
	public class AchievementFactory
	{
		private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

		// Enemy killing
		private Achievement KILL_ONE_ENEMY = new Achievement("Entry Level Murderer", "Killed your first enemy.");
		private Achievement KILL_FIFTY_ENEMIES = new Achievement("Hobbyist Murderer", "Killed your fifieth enemy.");

		// Score
		private Achievement SCORE_TEN_THOUSAND = new Achievement("Thousandaire", "Scored ten thousand points");

		public AchievementFactory()
		{
			achievements.Add("KILL_ONE", KILL_ONE_ENEMY);
			achievements.Add("KILL_FIFTY", KILL_FIFTY_ENEMIES);
			achievements.Add("SCORE_TEN_THOUSAND", SCORE_TEN_THOUSAND);
		}

		public Dictionary<string, Achievement> GetAchievements()
		{
			return achievements;
		}
	}
}
