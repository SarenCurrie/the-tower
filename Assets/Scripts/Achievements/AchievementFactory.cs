using UnityEngine;
using System.Collections;

namespace Achievements {
	public class AchievementFactory : MonoBehaviour {
		public static Achievement TEST_ACHIEVEMENT = new Achievement("Testing Achievemnts", "Killed your first enemy.");
	}
}
