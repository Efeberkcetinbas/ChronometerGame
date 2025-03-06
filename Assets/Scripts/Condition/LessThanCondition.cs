public class LessThanCondition : ITimeCondition
{
    private readonly float _maxValue;

    public LessThanCondition(float maxValue)
    {
        _maxValue = maxValue;
    }

    public bool IsSatisfied(float time)
    {
        return time < _maxValue;
    }
}