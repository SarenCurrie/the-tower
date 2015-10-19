using UnityEngine;
using System.Collections.Generic;

namespace Achievements
{
	public class AchievementFactory
	{
		private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

		// Boss killing
		private Achievement KILL_FIRST_BOSS = new Achievement("Tank Tipping", "Defeated the first boss.");
		private Achievement KILL_SECOND_BOSS = new Achievement("Spin Wash", "Defeated the second boss.");
		private Achievement KILL_FINAL_BOSS = new Achievement("SWEET VICTORY!", "Beat the game.");

		// Weapon Making
		private Achievement MAKE_FIRST_WEAPON = new Achievement("Blacksmith", "Make a weapon.");

		// Sucking
		private Achievement SCORE_NONE = new Achievement("Null Pointer", "Score zero points.");

		// Enemy killing
		private Achievement KILL_ONE_ENEMY = new Achievement("Entry Level Murderer", "Killed your first enemy.");
		private Achievement KILL_THIRTY_ENEMIES = new Achievement("Hobbyist Murderer", "Killed your thirtieth enemy.");
		private Achievement KILL_FIFTY_ENEMIES = new Achievement("Assassin", "Killed your fifieth enemy.");

		// Score
		private Achievement SCORE_TEN_THOUSAND = new Achievement("Thousandaire", "Scored ten thousand points");
		private Achievement SCORE_HUNDRED_THOUSAND = new Achievement("Pointy", "Scored one hundred thousand points");

		public AchievementFactory()
		{
			achievements.Add("KILL_FIRST_BOSS", KILL_FIRST_BOSS);
			achievements.Add("KILL_SECOND_BOSS", KILL_SECOND_BOSS);
			achievements.Add("KILL_FINAL_BOSS", KILL_FINAL_BOSS);
			achievements.Add("MAKE_FIRST_WEAPON", MAKE_FIRST_WEAPON);
			achievements.Add("SCORE_NONE", SCORE_NONE);
			achievements.Add("KILL_ONE", KILL_ONE_ENEMY);
			achievements.Add("KILL_THIRTY", KILL_THIRTY_ENEMIES);
			achievements.Add("KILL_FIFTY", KILL_FIFTY_ENEMIES);
			achievements.Add("SCORE_TEN_THOUSAND", SCORE_TEN_THOUSAND);
			achievements.Add("SCORE_HUNDRED_THOUSAND", SCORE_HUNDRED_THOUSAND);
		}

		public Dictionary<string, Achievement> GetAchievements()
		{
			return achievements;
		}
	}
}
