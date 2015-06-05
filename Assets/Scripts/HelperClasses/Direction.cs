
using UnityEngine;

public class Directions
{
    public enum Direction
    {
        Left,
        Up,
        Down,
        Right,
    }

    public static readonly Direction[] AllDirections = { Direction.Left, Direction.Right, Direction.Up, Direction.Down, };

    public static readonly Vector2[] VectorDirections =
        {
            new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, 1),
            new Vector2(0, -1),
        };

}
