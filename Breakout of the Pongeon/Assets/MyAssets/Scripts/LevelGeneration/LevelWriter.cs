using System.IO;

public class LevelWriter {
    Level level;
    BinaryWriter writer;

    public LevelWriter(Level level, string path) {
        this.level = level;
        writer = new BinaryWriter(File.Open(path, FileMode.Create));
    }


    public void WriteLevel() {
        writer.Write(level.name);
        writer.Write((short)level.width);
        writer.Write((short)level.height);

        for (int x = 0; x < level.width; x++) {
            for (int y = 0; y < level.height; y++) {
                writer.Write((short)level.grid.IDAtPosition(x, y));
            }
        }

        int key = 1;

        while (level.grid.hasLevelObject(key)) {
            writer.Write(level.grid.getLevelObject(key));
            key++;
        }


    }



}