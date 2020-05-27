using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string name;
    public int width;
    public int height;
    public Grid grid;

    public Level (string name, Grid grid)
    {
        this.name = name;
        this.grid = grid;
        this.width = grid.width;
        this.height = grid.height;
    }


}
