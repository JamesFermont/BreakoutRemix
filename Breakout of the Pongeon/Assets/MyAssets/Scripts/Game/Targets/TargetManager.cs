using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour {
    private TargetArea[] targetAreas;
    public bool isCompleted;
    
    private void OnEnable() {
        targetAreas = FindObjectsOfType<TargetArea>();
    }

    public void CheckCompleted() {
        int targetsHit = 0;
        foreach (TargetArea target in targetAreas) {
            if (target.isActivated) targetsHit++;
        }

        if (targetsHit == targetAreas.Length) {
            isCompleted = true;

            LevelStatistics.instance.EndTracker();
            SceneManager.LoadSceneAsync("UITest", LoadSceneMode.Additive);
            Time.timeScale = 0;
        }
    }
}
