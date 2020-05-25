using System;
using System.IO;
using System.Collections.Generic;

public class LevelReader {
    private BinaryReader reader = new BinaryReader(File.Open("", FileMode.Open));




    public Level readLevel(string filePath)
    {
        Level loadedLevel = null;
        try
        {
            if (File.Exists(filePath))
            {

                reader = new BinaryReader(File.Open(filePath, FileMode.Open));
                string name = reader.ReadString();
                short width = reader.ReadInt16();
                short height = reader.ReadInt16();
                int[,] map = new int[width, height];
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        map[x, y] = reader.ReadInt16();
                    }
                }
                int key = 1;
                string blockID;
                Dictionary<int, string> levelObjects = new Dictionary<int, string>();

                while (!endOfFile())
                {
                    blockID = reader.ReadString();
                    levelObjects.Add(key, blockID);
                    key++;
                }

                loadedLevel = new Level(name, new Grid(width, height, map, levelObjects));
            }
        } catch (Exception ex)
        {
            //uh oh;
        }

        return loadedLevel;
    }



    private bool endOfFile()
    {
        return reader.BaseStream.Position == reader.BaseStream.Length;
    }

}
