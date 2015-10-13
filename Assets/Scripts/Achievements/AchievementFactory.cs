using UnityEngine;
using System.Collections;

namespace Achievements {
	public class AchievementFactory {
		// Enemy killing
		public Achievement KILL_ONE_ENEMY = new Achievement("Entry Level Murderer", "Killed your first enemy.");
		public Achievement KILL_FIFTY_ENEMIES = new Achievement("Hobbyist Murderer", "Killed your fifieth enemy.");
		public Achievement KILL_ONE_HUNDRED_ENEMIES = new Achievement("Bloodbath", "Killed your hundredth enemy.");

		public Achievement FINISH_TUTORIAL = new Achievement("Sticky Elevator", "Started your journey.");
		public Achievement FINISH_FIRST_FLOOR = new Achievement("Slightly Less Sticky Elevator", "Finished the first floor.");
		public Achievement FINISH_SECOND_FLOOR = new Achievement("Gold Trimmed Elevator", "Finished the second floor.");
		public Achievement FINISH_GAME = new Achievement("Diamond-Buttoned Elevator", "Finished the game.");

		// Score
		public Achievement SCORE_TEN_THOUSAND = new Achievement("Thousandaire", "Scored ten thousand points.");
		public Achievement SCORE_ONE_HUNDRED_THOUSAND = new Achievement("Raining Points", "Scored one hundred thousand points.");
	}
}
