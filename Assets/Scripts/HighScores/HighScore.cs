namespace HighScores
{
	/// <summary>
	///
	/// POCSO representing a high score
	///
	/// @Author Saren
	/// </summary>
	public class HighScore
	{
		private string name;
		private int score;

		public HighScore(string n, int s)
		{
			name = n;
			score = s;
		}

		public string GetName()
		{
			return name;
		}

		public int GetScore()
		{
			return score;
		}
	}
}
