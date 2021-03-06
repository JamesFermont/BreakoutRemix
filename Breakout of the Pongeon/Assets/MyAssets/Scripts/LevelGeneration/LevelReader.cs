﻿using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LevelReader {

    private string filePath;
    BinaryReader reader;
    public LevelReader (string path) {
        filePath = path;
        
}

    public Level readLevel()
    {
        Level loadedLevel = null;
        try
        {
            if (File.Exists(filePath))
            {

                reader = new BinaryReader(File.Open(filePath, FileMode.Open));
                string name = reader.ReadString();
                int width = reader.ReadInt16();
                int height = reader.ReadInt16();

                int[,] map = new int[Constants.GRID_WIDTH, Constants.GRID_HEIGHT];
                for (int x = 0; x < Constants.GRID_WIDTH; x++)
                {
                    for (int y = 0; y < Constants.GRID_HEIGHT; y++)
                    {
                        if (x * height + y >= width * height)
                            break;
                        map[x, y] = reader.ReadInt16();
                    }
                }
                int key = 1;
                string blockID;
                GridList levelObjects = new GridList();

                while (!endOfFile())
                {
                    blockID = reader.ReadString();
                    levelObjects.Add(key, blockID);
                    key++;
                }

                loadedLevel = new Level(name, new Grid(map, levelObjects));
                reader.Close();
            }
        } catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }

        return loadedLevel;
    }



    private bool endOfFile()
    {
        return reader.BaseStream.Position == reader.BaseStream.Length;
    }

}
