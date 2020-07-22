[System.Serializable]
public class LevelBundle {

    string name;
    string[] levels;
    int score;
    


    public LevelBundle(string name, int score, string[] levels) {
        this.name = name;
        this.score = score;
        this.levels = levels;

    }

    public int TotalScore() {
        int totalScore = 0;
        for (int i = 0; i < levels.Length; i++)
            totalScore += Scores.GetHighscore(levels[i]);

        return totalScore;
    }
    
    public string Name() {
        return name;
    }
    public int RequiredScore() {
        return score;
    }

    public bool isUnlocked(int score) {
        return (score >= this.score);
    }

}
