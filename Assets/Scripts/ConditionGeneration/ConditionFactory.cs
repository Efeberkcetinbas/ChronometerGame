public static class ConditionFactory
{
    public static (ITimeCondition condition, string sceneName) CreateCondition(ConditionConfig config)
    {
        ITimeCondition condition = null;
        switch (config.conditionType)
        {
            case ConditionType.GreaterThan:
                condition = new GreaterThanCondition(config.value1);
                break;
            case ConditionType.LessThan:
                condition = new LessThanCondition(config.value1);
                break;
            case ConditionType.Between:
                condition = new BetweenCondition(config.value1, config.value2);
                break;
            case ConditionType.DivisibleBy:
                condition = new DivisibleByCondition(config.value1); // DivisibleBy uses tolerance internally.
                break;
            case ConditionType.EqualTo:
                condition = new EqualToCondition(config.value1);
                break;
        }
        return (condition, config.conditionSceneName);
    }
}