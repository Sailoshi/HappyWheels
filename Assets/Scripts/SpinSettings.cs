using UnityEngine;

/// <summary>
/// Provides settings for the game.
/// </summary>
public class SpinSettings : MonoBehaviour
{
    /// <summary>
    /// Set the total round to spin.
    /// </summary>
    public int Rounds;

    /// <summary>
    /// The maximum moving speed.
    /// </summary>
    public float Speed;

    /// <summary>
    /// The delay time for a group of <c>SpinBlock</c> when the spin should start.
    /// </summary>
    public float DelayTime;
    
    private int _counter = 0;
    private float _movedRange = 0;

    /// <summary>
    /// Resets the settings data.
    /// </summary>
    public void Reset()
    {
        _counter = 0;
        _movedRange = 0;
    }

    /// <summary>
    /// Increases the counter for the total round.
    /// </summary>
    /// <param name="count">The number of count.</param>
    public void IncreaseRoundCounter(int count)
    {
        _counter += count;
    }

    /// <summary>
    /// Increased the moved range.
    /// </summary>
    /// <param name="range">The number of moved range.</param>
    public void IncreaseMovedRange(float range)
    {
        _movedRange += range;
    }

    public int Counter { get
        {
            return _counter;
        }
    }

    public float MovedRange
    {
        get
        {
            return _movedRange;
        }
    }
}