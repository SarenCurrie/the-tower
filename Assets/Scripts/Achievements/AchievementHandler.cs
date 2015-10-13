using UnityEngine;
using System.Collections;

namespace Achievements
{
	/**
	 * Stores information regaridng achievements
	 */
	public class AchievementHandler
	{
		private int kills;
		private float totalDamage;
		private int totalBlood;
		private int roomsVisited;
		private AchievementFactory achievementFactory;

		public AchievementHandler()
		{
			kills = 0;
			totalDamage = 0;
			totalBlood = 0;
			roomsVisited = 0;
			achievementFactory = new AchievementFactory();
		}

		public void AddKill()
		{
			kills++;
			if (kills >= 100)
			{
				achievementFactory.KILL_ONE_HUNDRED_ENEMIES.Achieve();
			}
			else if (kills >= 50)
			{
				achievementFactory.KILL_FIFTY_ENEMIES.Achieve();
			}
			else if (kills >= 1)
			{
				achievementFactory.KILL_ONE_ENEMY.Achieve();
			}
		}

		public void FinishFloor(int floor)
		{
			switch(floor)
			{
				case 0:
					achievementFactory.FINISH_TUTORIAL.Achieve();
					break;
				case 1:
					achievementFactory.FINISH_FIRST_FLOOR.Achieve();
					break;
				case 2:
					achievementFactory.FINISH_SECOND_FLOOR.Achieve();
					break;
				case 3:
					achievementFactory.FINISH_GAME.Achieve();
					break;
			}
		}

		public void AddScore()
		{
			int score = Canvas.FindObjectOfType<ScoreManager>().score;

			if (score >= 10000)
			{
				achievementFactory.SCORE_TEN_THOUSAND.Achieve();
			}
			else if (score >= 100000)
			{
				achievementFactory.SCORE_ONE_HUNDRED_THOUSAND.Achieve();
			}
		}
	}
}