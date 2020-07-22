using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scores {
    private static List<Score> scores;


    public static bool SubmitScore(Score score) {
        if (scores == null)
            scores = ScoreIO.LoadScores();

        if (score == null || !score.isValid())
            return false;
        List<Score> levelScores = GetSortedScoresFromLevel(score.level);

        if (levelScores == null || levelScores.Count <= Constants.SCORE_COUNT) {
            scores.Add(score);
        } else {
            for (int i = 0; i < levelScores.Count; i++) {
                if (levelScores[i].finalScore() <= score.finalScore()) {
                    scores.Remove(levelScores[levelScores.Count - 1]);
                    scores.Add(score);
                    break;
                }
            }
        }

        ScoreIO.SaveScores(scores);
        return true;
    }

    public static List<Score> GetSortedScoresFromLevel(string levelName) {
        if (scores == null)
            scores = ScoreIO.LoadScores();

        if (string.IsNullOrWhiteSpace(levelName))
            return null;

        List<Score> returnScores = new List<Score>();
        foreach (Score score in scores)
            if (score.level == levelName)
                returnScores.Add(score);
        returnScores.Sort(new ScoreComparer());
        return returnScores;
    }

    public static int GetHighscore(string levelName) {
        return (GetSortedScoresFromLevel(levelName).Count == 0) ? 0 : (int)GetSortedScoresFromLevel(levelName)[0].finalScore();
    }

    public static bool isInHighScore(string level, int score) {
        List<Score> levelScores = GetSortedScoresFromLevel(level);
        if (levelScores.Count < 10)
            return true;
        foreach (Score highScore in levelScores) {
            if (score >= highScore.finalScore())
                return true;
        }
        return false;
    }


}
