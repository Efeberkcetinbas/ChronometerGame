using UnityEngine;

public class EqualToCondition : ITimeCondition
{
    private readonly float _exactValue;

    public EqualToCondition(float exactValue)
    {
        _exactValue = exactValue;
    }

    public bool IsSatisfied(float time)
    {
        // Use a tolerance when comparing floats.
        return Mathf.Abs(time - _exactValue) < 0.01f;
    }
}