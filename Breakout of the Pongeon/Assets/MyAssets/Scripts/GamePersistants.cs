using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersistants : MonoBehaviour
{
    public static GamePersistants instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
