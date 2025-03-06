using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelConditionManager : MonoBehaviour
{
    [SerializeField] private ConditionGenerator conditionGenerator;
    
    // List of mappings between condition scene identifiers and their GameObjects.
    [SerializeField] private List<ConditionSceneMapping> conditionSceneMappings;
    
    // UI Text element to display the current condition's instructions.
    [SerializeField] private TextMeshProUGUI conditionInstructionText;
    
    // List of built ITimeCondition objects.
    private List<ITimeCondition> activeConditions = new List<ITimeCondition>();
    
    // Keep a reference to the original configs for instruction display.
    private List<ConditionConfig> activeConfigs = new List<ConditionConfig>();

    // Index of the current condition.
    private int currentConditionIndex = 0;

    private void Start()
    {
        // Build conditions from the generator.
        foreach (ConditionConfig config in conditionGenerator.conditions)
        {
            var (condition, sceneName) = ConditionFactory.CreateCondition(config);
            activeConditions.Add(condition);
            activeConfigs.Add(config);
        }
        
        // Set up the first condition scene and instruction.
        UpdateActiveConditionSceneAndText();
    }
    
    /// <summary>
    /// Validates only the current condition with the provided time.
    /// If the condition is satisfied, it moves to the next condition.
    /// </summary>
    public void ValidateCurrentCondition(float time)
    {
        // If all conditions are completed, report level complete.
        if (currentConditionIndex >= activeConditions.Count)
        {
            Debug.Log("All conditions have been satisfied!");
            conditionInstructionText.text = "Level Complete!";
            return;
        }
        
        ITimeCondition currentCondition = activeConditions[currentConditionIndex];
        if (currentCondition.IsSatisfied(time))
        {
            Debug.Log($"Condition {currentConditionIndex + 1} satisfied at time: {time}");
            // Deactivate the current condition scene.
            SetConditionSceneActive(currentConditionIndex, false);
            
            // Move to the next condition.
            currentConditionIndex++;
            UpdateActiveConditionSceneAndText();
        }
        else
        {
            Debug.Log($"Condition {currentConditionIndex + 1} not satisfied at time: {time}");
            // Optionally, provide feedback here.
        }
    }
    
    /// <summary>
    /// Updates which condition scene is active and updates the instruction text.
    /// Only the current condition scene will be active.
    /// </summary>
    private void UpdateActiveConditionSceneAndText()
    {
        // Deactivate all condition scenes first.
        foreach (var mapping in conditionSceneMappings)
        {
            if(mapping.conditionSceneObject != null)
                mapping.conditionSceneObject.SetActive(false);
        }
        
        // Check if we still have a condition to activate.
        if (currentConditionIndex < activeConfigs.Count)
        {
            // Get the scene name for the current condition.
            string sceneName = activeConfigs[currentConditionIndex].conditionSceneName;
            // Activate the scene associated with the current condition.
            SetConditionSceneActive(currentConditionIndex, true);
            
            // Update the instruction text based on the current condition.
            conditionInstructionText.text = GenerateInstruction(activeConfigs[currentConditionIndex]);
        }
        else
        {
            conditionInstructionText.text = "All conditions satisfied!";
        }
    }
    
    /// <summary>
    /// Activates (or deactivates) the scene corresponding to the condition at the given index.
    /// It looks up the scene via the condition config's scene name.
    /// </summary>
    private void SetConditionSceneActive(int conditionIndex, bool isActive)
    {
        if (conditionIndex < 0 || conditionIndex >= activeConfigs.Count)
            return;
        
        string sceneName = activeConfigs[conditionIndex].conditionSceneName;
        ConditionSceneMapping mapping = conditionSceneMappings.Find(m => m.sceneName == sceneName);
        if (mapping != null && mapping.conditionSceneObject != null)
        {
            mapping.conditionSceneObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning($"No mapping found for condition scene: {sceneName}");
        }
    }
    
    /// <summary>
    /// Generates a string instruction for a condition based on its type and parameter values.
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
                return $"Between {config.value1} - {config.value2}";
            case ConditionType.DivisibleBy:
                return $"Divisible by {config.value1}";
            case ConditionType.EqualTo:
                return $"Equal to {config.value1}";
            default:
                return "Unknown condition";
        }
    }
}

// Helper class to map a condition scene identifier to a GameObject.
[System.Serializable]
public class ConditionSceneMapping
{
    public string sceneName;
    public GameObject conditionSceneObject;
}