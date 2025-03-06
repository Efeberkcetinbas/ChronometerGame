public interface ITimeCondition
{
    bool IsSatisfied(float time);
}

public enum ConditionType
{
    GreaterThan,
    LessThan,
    Between,
    DivisibleBy,
    EqualTo
}


