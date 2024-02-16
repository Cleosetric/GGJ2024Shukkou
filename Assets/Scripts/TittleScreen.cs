using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{
    public TextMeshProUGUI textVersion;

    private void Start()
    {
        string unityVersion = Application.version;
        textVersion.SetText("Ver " + unityVersion);
        AudioManager.instance.Play("TittleScreen");
    }

    public void ChangeSceneGameplay()
    {
        AudioManager.instance.Stop("TittleScreen");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
