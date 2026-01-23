using System;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    // lijst met geraakte bumper tags
    private List<string> bumperTags = new List<string>();

    // huidige multiplier
    private int scoreMultiplier = 1;

    // Action Event voor score + multiplier (UI)
    public static event Action<int, int> OnScoreChange;

    private void Start()
    {
        // luisteren naar bumper hits
        BumperHit.onBumperHit += CheckForCombo;
    }

    private void OnDisable()
    {
        BumperHit.onBumperHit -= CheckForCombo;
    }

    
    private void CheckForCombo(Transform transform, int bumperValue)
    {
        
        bumperTags.Add(transform.gameObject.tag);

        if (bumperTags.Count > 1)
        {
            if (bumperTags[bumperTags.Count - 2] ==
                bumperTags[bumperTags.Count - 1])
            {
                scoreMultiplier++;              // verhoog multiplier
            }
            else
            {
                scoreMultiplier = 1;            // reset multiplier
                bumperTags.Clear();             // leeg lijst
            }
        }

        // voeg score toe
        ScoreManager.Instance.AddScore(bumperValue * scoreMultiplier);

        // verstuur score + multiplier via Action
        OnScoreChange?.Invoke(
            ScoreManager.Instance.score,
            scoreMultiplier
        );
    }
}
