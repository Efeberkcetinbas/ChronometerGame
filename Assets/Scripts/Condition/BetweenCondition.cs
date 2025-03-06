public class BetweenCondition : ITimeCondition
{
    private readonly float _min;
    private readonly float _max;

    public BetweenCondition(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public bool IsSatisfied(float time)
    {
        return time >= _min && time <= _max;
    }
}