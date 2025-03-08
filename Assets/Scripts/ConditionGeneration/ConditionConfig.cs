using UnityEngine;

[CreateAssetMenu(fileName = "ConditionConfig", menuName = "Game/ConditionConfig", order = 1)]
public class ConditionConfig : ScriptableObject
{
    public ConditionType conditionType;
    
    // Parameters for the condition.
    // For GreaterThan, LessThan, DivisibleBy, EqualTo: only one value is needed.
    // For Between: use minValue and maxValue.
    public float value1;
    public float value2; // Only used for Between type.
    
    
}