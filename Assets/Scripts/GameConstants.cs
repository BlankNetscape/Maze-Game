public enum Dirrections
{
    RIGHT = 1,
    LEFT = 2,
    FORWARD = 3,
    BACKWARD = 4
}

public enum NodeState
{
    Available,
    Current,
    Completed
}

public enum NodeType
{
    Floor,
    Respawn,
    Finish
}

public enum Destinations
{
    START,
    FINISH
}


public class GameTags
{
    public static readonly string Untagged = "Untagged";
    public static readonly string Respawn = "Respawn";
    public static readonly string Finish = "Finish";
    public static readonly string EditorOnly = "EditorOnly";
}

public class SpeedValues
{
    public static readonly float SLOW = 2f;
    public static readonly float NORMAL = 2.5f;
    public static readonly float FAST = 4f;
}

public class BotSpeedModValues
{
    // Multipliers
    public static readonly float FAST = .9f;
    public static readonly float NORMAL = .75f;
    public static readonly float SLOW = .5f;
}