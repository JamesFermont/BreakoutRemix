[System.Serializable]
public class LevelBundle {

    public string name;
    public string[] levels;
    public int score;
    public bool unlocksEditor = false;

    public LevelBundle(string name, int score, string[] levels, bool unlocksEditor) {
        this.name = name;
        this.score = score;
        this.levels = levels;
        this.unlocksEditor = unlocksEditor;
    }

    public int TotalScore() {
        int totalScore = 0;
        for (int i = 0; i < levels.Length; i++)
            totalScore += Scores.GetHighscore(levels[i]);

        return totalScore;
    }
}
