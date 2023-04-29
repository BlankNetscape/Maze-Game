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

public class GameTags
{
    public static readonly string Untagged = "Untagged";
    public static readonly string Respawn = "Respawn";
    public static readonly string Finish = "Finish";
    public static readonly string EditorOnly = "EditorOnly";
}

public enum Destinations
{
    START,
    FINISH
}


