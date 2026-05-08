using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUIHandler : MonoBehaviour
{

    public TMP_InputField NameInput; // NEW Drag your InputField here
    public Text BestScoreText; // Add this for the Menu high score display

    private void Start() //NEW
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
        //NEW
        if (MainDataPersistence.Instance != null && NameInput != null)
        {
            MainDataPersistence.Instance.PlayerName = NameInput.text;
        }

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



}
