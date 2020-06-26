public enum EffectType {ON_DAMAGED, ON_DESTROYED, ON_ALL}

public enum UnitType {UNIT_TIME, UNIT_BOUNCE}

public enum SoundType {BGM, SFX}

public enum SwitchColor {SWITCH_RED, SWITCH_GREEN, SWITCH_BLUE}

public enum EditorMode { EDIT, PLAY }

public class Constants
{
    //Size of a level in cells
    public static int GRID_WIDTH = 20;
    public static int GRID_HEIGHT = 18;

    //Number of rows from the bottom protected from block placement
    public static int PROTECTED_ROWS = 3;

    //Dimensions of a level in Unity
    public static float LEVEL_WIDTH = 12.8f; 
    public static float LEVEL_HEIGHT = 7.2f;
}
