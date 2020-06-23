using UnityEngine;

public class SwitchSlave : MonoBehaviour {
    public SwitchColor color;

    private void Start() {
        SwitchMaster[] masters = FindObjectsOfType<SwitchMaster>();

        foreach (SwitchMaster master in masters) {
            if (master.color == color) {
                SwitchSlave[] oldSlaves = master.slaves;
                SwitchSlave[] newSlaves = new SwitchSlave[oldSlaves.Length+1];
                var count = 0;
                foreach (SwitchSlave slave in oldSlaves) {
                    newSlaves[count] = slave;
                    count++;
                }

                newSlaves[count] = this;
                master.slaves = newSlaves;
                break;
            }
        }
        
        gameObject.GetComponent<BlockManager>().ToggleBlock(false);
        
    }
}
