using UnityEngine;

public class SwitchMaster : MonoBehaviour {
    public SwitchColor color;
    [HideInInspector]
    public SwitchSlave[] slaves;

    private void OnEnable() {
        gameObject.GetComponent<BlockManager>().onDestroyed += PerformEffect;
    }

    private void PerformEffect() {
        foreach (SwitchSlave slave in slaves)
        {
            slave.gameObject.GetComponent<BlockManager>().ToggleBlock(true);
        }
    }
}
