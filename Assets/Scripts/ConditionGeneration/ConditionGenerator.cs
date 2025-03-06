using UnityEngine;

[CreateAssetMenu(fileName = "ConditionGenerator", menuName = "Game/ConditionGenerator", order = 2)]
public class ConditionGenerator : ScriptableObject
{
    public ConditionConfig[] conditions;
}