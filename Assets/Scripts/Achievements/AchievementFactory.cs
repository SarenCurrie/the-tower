using UnityEngine;
using System.Collections;

namespace Achievements {
	public class AchievementFactory : MonoBehaviour {
		public static Achievement KILL_ONE_ENEMY = new Achievement("Testing Achievemnts", "Killed your first enemy.");
	}
}
