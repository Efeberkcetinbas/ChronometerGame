public static class ConditionFactory
{
    public static ITimeCondition CreateCondition(ConditionConfig config)
    {
        switch (config.conditionType)
        {
            case ConditionType.GreaterThan:
                return new GreaterThanCondition(config.value1);
            case ConditionType.LessThan:
                return new LessThanCondition(config.value1);
            case ConditionType.Between:
                return new BetweenCondition(config.value1, config.value2);
            case ConditionType.DivisibleBy:
                return new DivisibleByCondition(config.value1);
            case ConditionType.EqualTo:
                return new EqualToCondition(config.value1);
            default:
                return null;
        }
    }
}