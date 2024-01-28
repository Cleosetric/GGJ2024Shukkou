using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleScreen : MonoBehaviour
{
    
    public void ChangeSceneGameplay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
