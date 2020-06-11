using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string name;
    public Grid grid;

    public Level (string name, Grid grid)
    {
        this.name = name;
        this.grid = grid;
    }


}
