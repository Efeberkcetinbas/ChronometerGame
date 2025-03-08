using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelConditionManager : MonoBehaviour
{
    [SerializeField] private ConditionGenerator conditionGenerator;
    [SerializeField] private TextMeshProUGUI conditionInstructionText;
    
    private List<ITimeCondition> activeConditions = new List<ITimeCondition>();
    private List<ConditionConfig> activeConfigs = new List<ConditionConfig>();
    private int currentConditionIndex = 0;

    private void Start()
    {
        // Build conditions from the generator.
        foreach (ConditionConfig config in conditionGenerator.conditions)
        {
            activeConditions.Add(ConditionFactory.CreateCondition(config));
            activeConfigs.Add(config);
        }
        UpdateInstructionText();
    }

    /// <summary>
    /// Validates only the current condition using the provided time.
    /// If satisfied, moves to the next condition.
    /// </summary>
    public void ValidateCurrentCondition(float time)
    {
        if (currentConditionIndex >= activeConditions.Count)
        {
            Debug.Log("All conditions satisfied!");
            conditionInstructionText.text = "Level Complete!";
            return;
        }

        ITimeCondition currentCondition = activeConditions[currentConditionIndex];
        if (currentCondition.IsSatisfied(time))
        {
            Debug.Log($"Condition {currentConditionIndex + 1} satisfied at time: {time}");
            currentConditionIndex++;
            UpdateInstructionText();
        }
        else
        {
            Debug.Log($"Condition {currentConditionIndex + 1} not satisfied at time: {time}");
            // Optionally provide user feedback here.
        }
    }

    /// <summary>
    /// Updates the instruction text for the current condition.
    /// </summary>
    private void UpdateInstructionText()
    {
        if (currentConditionIndex < activeConfigs.Count)
        {
            conditionInstructionText.text = GenerateInstruction(activeConfigs[currentConditionIndex]);
        }
        else
        {
            conditionInstructionText.text = "All conditions satisfied!";
        }
    }

    /// <summary>
    /// Generates a descriptive instruction string for the given condition.
    /// </summary>
    private string GenerateInstruction(ConditionConfig config)
    {
        switch (config.conditionType)
        {
            case ConditionType.GreaterThan:
                return $"Greater than {config.value1}";
            case ConditionType.LessThan:
                return $"Less than {config.value1}";
            case ConditionType.Between:
                return $"Between {config.value1} and {config.value2}";
            case ConditionType.DivisibleBy:
                return $"Divisible by {config.value1}";
            case ConditionType.EqualTo:
                return $"Equal to {config.value1}";
            default:
                return "Unknown condition";
        }
    }
}