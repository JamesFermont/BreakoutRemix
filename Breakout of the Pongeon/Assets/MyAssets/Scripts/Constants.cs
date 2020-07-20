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

    //Amount of scores saved per Level
    public static int SCORE_COUNT = 10;

    //Score to use as Filler for Lists
    public static Score DEFAULT_SCORE = new Score("0d1n", null, 0, 0);



    //Dimensions of a level in Unity
    public static float LEVEL_WIDTH = 12.8f; 
    public static float LEVEL_HEIGHT = 7.2f;

    //Bonus for winning without losing a ball
    public static int PERFECT_GAME_BONUS;
    
    public static int DROPPED_BALL_PENATLY;
    

    public static int SECONDS_PER_TIMEMOD_INTERVAL;
    public static float MIN_TIMEMOD;
    public static float MAX_TIMEMOD;


}
