
public class GreaterThanCondition : ITimeCondition
{
    private readonly float _minValue;

    public GreaterThanCondition(float minValue)
    {
        _minValue = minValue;
    }

    public bool IsSatisfied(float time)
    {
        return time > _minValue;
    }
}
