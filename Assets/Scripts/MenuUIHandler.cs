using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUIHandler : MonoBehaviour
{

    public TMP_InputField NameInput; // NEW Drag your InputField here
    public Text BestScoreText; // Add this for the Menu high score display

    private void Start() //NEW All of this is new
    {
        // 1. Check if we have persistent data available
        if (MainDataPersistence.Instance != null)
        {
            // 2. Prefill the InputField if a name was previously saved
            if (!string.IsNullOrEmpty(MainDataPersistence.Instance.BestPlayerName))
            {
                NameInput.text = MainDataPersistence.Instance.BestPlayerName;
            }

            // 3. Update the High Score text
            if (MainDataPersistence.Instance.HighScore > 0)
            {
                BestScoreText.text = $"Best Score: {MainDataPersistence.Instance.BestPlayerName} : {MainDataPersistence.Instance.HighScore}";
            }
            else
            {
                BestScoreText.text = "Best Score: none";
            }
        }
    }

    public void StartNew()
    {
        //start NEW02
        // Check if the input is null, empty, or just spaces
        if (string.IsNullOrWhiteSpace(NameInput.text))
        {
            // Optional: Give visual feedback
            Debug.Log("Please enter a name before starting!");

            // You could also change the placeholder color to red to alert the user
            var placeholder = NameInput.placeholder as TextMeshProUGUI;
            if (placeholder != null)
            {
                placeholder.color = Color.red;
                placeholder.text = "NAME REQUIRED!";
            }

            return; // EXIT the function early so SceneManager.LoadScene(1) is never called
        }
        //End NEW02

        //NEW
        if (MainDataPersistence.Instance != null && NameInput != null)
        {
            MainDataPersistence.Instance.PlayerName = NameInput.text;
        }
        //End NEW

        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    //start NEW
    // New method to update the screen text
    public void RefreshUI()
    {
        if (MainDataPersistence.Instance != null)
        {
            // Update Name Input
            if (!string.IsNullOrEmpty(MainDataPersistence.Instance.BestPlayerName))
            {
                NameInput.text = MainDataPersistence.Instance.BestPlayerName;
            }
            else
            {
                NameInput.text = ""; // Clear it if no name exists
            }

            // Update Best Score Text
            if (MainDataPersistence.Instance.HighScore > 0)
            {
                //  BestScoreText.text = $"Best Score: {MainDataPersistence.Instance.BestPlayerName} : {MainDataPersistence.Instance.HighScore}";
                BestScoreText.text = $"Player: {MainDataPersistence.Instance.PlayerName} | Best Score: {MainDataPersistence.Instance.BestPlayerName} : {MainDataPersistence.Instance.HighScore}";
            }
            else
            {
                BestScoreText.text = "Best Score: none";
            }
        }
    }
    public void ResetData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted!");

            // Optional: Refresh the UI immediately
            MainDataPersistence.Instance.HighScore = 0;
            MainDataPersistence.Instance.BestPlayerName = "";
            MainDataPersistence.Instance.PlayerName = "";
        }
        // Refresh the screen immediately
        RefreshUI();
    }
    //End NEW

}
