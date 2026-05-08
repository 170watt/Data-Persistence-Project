using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUIHandler : MonoBehaviour
{

    public TMP_InputField NameInput; // NEW Drag your InputField here

    private void Start()
    {

    }

    public void StartNew()
    {
        //NEW
        if (MainDataPersistence.Instance != null)
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
