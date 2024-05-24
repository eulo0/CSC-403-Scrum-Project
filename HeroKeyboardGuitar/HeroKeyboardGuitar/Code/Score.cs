using System.Windows.Forms;

namespace HeroKeyboardGuitar;

/// <summary>
/// Holds the current score and streak of the player
/// </summary>
public class Score {
    /// <summary>
    /// Amount of notes successfully hit
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Current streak, i.e. consecutive hit notes without a miss
    /// </summary>
    public int Streak { get; private set; }

    /// <summary>
    /// initializes both amount and streak to 0
    /// </summary>
    public Score() {
        Amount = 0;
        Streak = 0;
    }

    /// <summary>
    /// Used to check if the player has reached the point of receiving a reward.
    /// Currently not implemented
    /// </summary>
    public void CheckReward() {
        // TODO: possibly make this a dictionary mapping genres to reward maps
        //Game.GetRewardMap()
    }

    /// <summary>
    /// Add to the current score
    /// </summary>
    /// <param name="amount">Amount to add</param>
    public void Add(int amount) {
        Amount += amount;
        Streak++;
    }

    /// <summary>
    /// Resets streak back to 0
    /// </summary>
    public int misses = 0;
    public void Miss()
    {
        misses += 1;
        CheckFailure(misses);
        Streak = 0;
    }

    public void CheckFailure(int someint)
    {
        if (someint == 5)
        {
            FailScreen failScreen = new FailScreen();
            failScreen.Show();
        }
    }
}
