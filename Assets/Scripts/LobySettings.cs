using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobySettings : MonoBehaviour
{
    public static float matchTimeInSeconds;
    public static float characterKickForce;
    public static int maxBoxSpawn;

    private string nextMatchScene;

    [SerializeField] private Slider matchTimeSlider;
    [SerializeField] private TMP_Text matchTimeValueDisplay;

    [SerializeField] private Slider kickForceSlider;
    [SerializeField] private TMP_Text kickForceValueDisplay;
    
    [SerializeField] private Slider maxBoxSlider;
    [SerializeField] private TMP_Text maxBoxValueDisplay;

    [SerializeField] private TMP_Dropdown levelSelectorDropdown;
    [SerializeField] private Image selectedLevelDisplay;

    [SerializeField] private Sprite squareLevelDisplay;
    [SerializeField] private Sprite raceLevelDisplay;
    [SerializeField] private Sprite hLevelDisplay;

    private void Start()
    {
        matchTimeInSeconds = 300f;
        characterKickForce = 10f;
        maxBoxSpawn = 100;

        matchTimeSlider.value = matchTimeInSeconds;
        kickForceSlider.value = characterKickForce;
        maxBoxSlider.value = maxBoxSpawn;

        UpdateSettings();
    }

    public void UpdateSettings()
    {
        matchTimeInSeconds = matchTimeSlider.value;
        // Creates variables to display time in minutes and seconds
        float minutes = Mathf.FloorToInt(matchTimeInSeconds / 60);
        float seconds = Mathf.FloorToInt(matchTimeInSeconds % 60);

        characterKickForce = kickForceSlider.value;
        maxBoxSpawn = (int)maxBoxSlider.value;

        // Updates the UI text element to display the time
        matchTimeValueDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        kickForceValueDisplay.text = characterKickForce.ToString();
        maxBoxValueDisplay.text = maxBoxSpawn.ToString();

        Debug.Log(levelSelectorDropdown.value.ToString());

        if (levelSelectorDropdown.value.ToString() == "0")
        {
            selectedLevelDisplay.sprite = squareLevelDisplay;
            nextMatchScene = "SquareLevel";
        }
        if (levelSelectorDropdown.value.ToString() == "1")
        {
            selectedLevelDisplay.sprite = raceLevelDisplay;
            nextMatchScene = "RaceLevel";
        }
        if (levelSelectorDropdown.value.ToString() == "2")
        {
            selectedLevelDisplay.sprite = hLevelDisplay;
            nextMatchScene = "HLevel";
        }
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene(nextMatchScene);
    }
}
