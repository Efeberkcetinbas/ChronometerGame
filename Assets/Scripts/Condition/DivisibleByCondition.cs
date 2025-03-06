using UnityEngine;

public class DivisibleByCondition : ITimeCondition
{
    private readonly float _divisor;
    // Tolerance used only in this condition for floating point comparison.
    private const float Tolerance = 0.01f;

    public DivisibleByCondition(float divisor)
    {
        _divisor = divisor;
    }

    public bool IsSatisfied(float time)
    {
        float remainder = time % _divisor;
        return (Mathf.Abs(remainder) < Tolerance || Mathf.Abs(remainder - _divisor) < Tolerance);
    }
}