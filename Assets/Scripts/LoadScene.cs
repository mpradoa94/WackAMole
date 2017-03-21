using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void NextSceneButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NextSceneButton(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
