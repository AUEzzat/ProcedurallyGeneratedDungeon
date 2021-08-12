using System.Collections.Generic;
using UnityEngine;

public enum GenerationDirection
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class NextDirection
{
    const GenerationDirection NONE = GenerationDirection.NONE;
    const GenerationDirection UP = GenerationDirection.UP;
    const GenerationDirection DOWN = GenerationDirection.DOWN;
    const GenerationDirection LEFT = GenerationDirection.LEFT;
    const GenerationDirection RIGHT = GenerationDirection.RIGHT;

    public GenerationDirection Direction { get; private set; } = NONE;
    Dictionary<GenerationDirection, List<GenerationDirection>> probabilities;

    public NextDirection()
    {
        probabilities = new Dictionary<GenerationDirection, List<GenerationDirection>>()
        {
            { NONE, new List<GenerationDirection>(){UP, DOWN, LEFT, RIGHT} },
            { UP, new List<GenerationDirection>(){UP, UP, LEFT, RIGHT} },
            { DOWN, new List<GenerationDirection>(){ DOWN, DOWN, LEFT, RIGHT} },
            { LEFT, new List<GenerationDirection>(){ LEFT, LEFT, UP, DOWN } },
            { RIGHT, new List<GenerationDirection>(){ RIGHT, RIGHT, UP, DOWN} },
        };
    }

    public GenerationDirection Get()
    {
        return Direction = probabilities[Direction][Random.Range(0, 3)];
    }

    public Vector3 GetValue(bool newDirection = true)
    {
        if (newDirection) Get();
        switch (Direction)
        {
            default:
            case GenerationDirection.NONE:
                return Vector3.zero;
            case GenerationDirection.UP:
                return Vector3.forward;
            case GenerationDirection.DOWN:
                return Vector3.back;
            case GenerationDirection.LEFT:
                return Vector3.left;
            case GenerationDirection.RIGHT:
                return Vector3.right;
        }
    }
}
