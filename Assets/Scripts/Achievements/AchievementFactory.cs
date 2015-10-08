using UnityEngine;
using System.Collections;

namespace Achievements {
	public class AchievementFactory : MonoBehaviour {
		public static Achievement KILL_ONE_ENEMY = new Achievement("Entry Level Murderer", "Killed your first enemy.");
		public static Achievement KILL_FIFTY_ENEMIES = new Achievement("Hobbyist Murderer", "Killed your fifieth enemy.");
	}
}
