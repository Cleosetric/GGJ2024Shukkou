using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TittleScreen : MonoBehaviour
{
    public TextMeshProUGUI textVersion;

    private void Start() {
        string unityVersion = Application.version;
        textVersion.SetText("Ver "+ unityVersion);
    }
    
    public void ChangeSceneGameplay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
