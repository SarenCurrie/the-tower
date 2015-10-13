using UnityEngine;
using System.Collections;
using Achievements;

public class AchievementManager : MonoBehaviour {

	/**
	 * Achieves the achievemnt
	 */
	public static void DoAchievement(Achievement a) {
		if (!a.hasAchieved) {
			a.hasAchieved = true;
			ShowAchievement(a);
		}
	}

	/**
	 * Displays achievemnt on the screen.
	 */
	private static void ShowAchievement(Achievement a) {
		UIController.GetUI().ShowDialog(string.Format("Achievemnt Unlocked!\n{0}: {1}", a.name, a.text), 5f);
	}
}