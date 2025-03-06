using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chronometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private LevelConditionManager conditionManager;
    
    // Speed multiplier for the timer (adjustable in the Inspector).
    // Lower values make the timer run slower, giving the player more time to stop it.
    [SerializeField] private float timerSpeed = 50f;
    
    private float _currentTime;
    private bool _isRunning = false;
    
    /// <summary>
    /// Starts the chronometer from 0.
    /// </summary>
    public void StartTimer()
    {
        _currentTime = 0;
        _isRunning = true;
    }

    /// <summary>
    /// Stops the chronometer and validates the current condition.
    /// </summary>
    public void StopTimer()
    {
        _isRunning = false;
        conditionManager.ValidateCurrentCondition(_currentTime);
    }

    private void Update()
    {
        if (_isRunning)
        {
            // Increase current time based on Time.deltaTime multiplied by timerSpeed.
            _currentTime += Time.deltaTime * timerSpeed;
            
            // When reaching 100, reset to 0 so the timer cycles.
            if (_currentTime >= 100f)
            {
                _currentTime = 0f;
            }
            
            UpdateDisplay();
        }
    }
    
    /// <summary>
    /// Updates the timer display.
    /// </summary>
    private void UpdateDisplay()
    {
        timerText.text = _currentTime.ToString("F2");
    }
}