using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chronometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private LevelConditionManager conditionManager;
    [SerializeField] private float timerSpeed = 50f; // Adjust this value to make the timer slower or faster.
    
    private float _currentTime;
    private bool _isRunning = true; // Initially, the timer is running.
    
    // To detect a new touch without triggering multiple toggles per touch.
    private bool _touchInProgress = false;

    private void Update()
    {
        // Check for touch input.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // When a new touch begins, toggle the timer state.
            if (touch.phase == TouchPhase.Began && !_touchInProgress)
            {
                _touchInProgress = true;
                ToggleTimer();
            }
        }
        else
        {
            _touchInProgress = false;
        }

        // If the timer is running, update the timer.
        if (_isRunning)
        {
            _currentTime += Time.deltaTime * timerSpeed;
            if (_currentTime >= 100f)
            {
                _currentTime = 0f;
            }
            UpdateDisplay();
        }
    }
    
    /// <summary>
    /// Toggles the timer: stops and validates the condition if running, resumes otherwise.
    /// </summary>
    private void ToggleTimer()
    {
        if (_isRunning)
        {
            // Stop the timer and validate the current condition.
            _isRunning = false;
            conditionManager.ValidateCurrentCondition(_currentTime);
        }
        else
        {
            // Resume the timer without resetting the current time.
            _isRunning = true;
        }
    }
    
    /// <summary>
    /// Updates the timer UI text.
    /// </summary>
    private void UpdateDisplay()
    {
        timerText.text = _currentTime.ToString("F2");
    }
}