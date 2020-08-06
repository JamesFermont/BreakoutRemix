using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour {
    private TargetArea[] targetAreas;
    public bool isCompleted;
    private static readonly int IsActive = Animator.StringToHash("isActive");
    private static readonly int IsBallLaunched = Animator.StringToHash("isBallLaunched");

    private void OnEnable() {
        FindTargetAreas();
    }

    public void CheckCompleted() {
        int targetsHit = 0;
        foreach (TargetArea target in targetAreas) {
            if (target.isActivated) targetsHit++;
        }

        if (targetsHit == targetAreas.Length) {
            isCompleted = true;
            GameObject.FindWithTag("Background").GetComponent<Animator>().SetBool(IsBallLaunched, false);
            if(SceneManager.GetActiveScene().name == "GameLevel") {
                StartCoroutine(DespawnLevel());
                LevelManager.EndLevel();
            } else {
                FindObjectOfType<EditorNavigation>().SwitchToEdit();
            }
            
        }
    }

    public void ApplyPenalty() {
        foreach (TargetArea target in targetAreas) {
            if (target.isActivated) {
                target.isActivated = false;
                target.animator.SetBool(IsActive, false);
                LevelStatistics.instance.AddLevelScore(-target.scoreOnTag);
                break;
            }
        }
    }

    public void FindTargetAreas () {
        targetAreas = FindObjectsOfType<TargetArea>();
    }
    
    static IEnumerator DespawnLevel() {
        foreach (Transform child in LevelManager.currentLevelGO.transform) {
            if (child.GetComponent<SpriteRenderer>().enabled) {
                child.transform.GetComponent<BlockManager>().StartCoroutine(child.transform.GetComponent<BlockManager>().FadeOut());
                yield return new WaitForSecondsRealtime(0.02f);
            }
        }

        foreach (DataPack dp in FindObjectsOfType<DataPack>()) {
            dp.StartCoroutine(dp.FadeOut());
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
