using System.Collections;
using System.Collections.Generic;


public class ScoreComparer : IComparer<Score> {
    public int Compare(Score x, Score y) {
        return x.finalScore().CompareTo(y.finalScore());
    }
}
