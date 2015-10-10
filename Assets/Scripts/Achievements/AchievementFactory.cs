using UnityEngine;
using System.Collections;

namespace Achievements {
	public class AchievementFactory {
		// Enemy killing
		public Achievement KILL_ONE_ENEMY = new Achievement("Entry Level Murderer", "Killed your first enemy.");
		public Achievement KILL_FIFTY_ENEMIES = new Achievement("Hobbyist Murderer", "Killed your fifieth enemy.");

		// Score
		public Achievement SCORE_TEN_THOUSAND = new Achievement("Thousandaire", "Scored ten thousand points");
	}
}
