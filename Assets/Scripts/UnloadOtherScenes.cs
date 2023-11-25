using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadOtherScenes : MonoBehaviour
{
    // Start is called before the first frame update

    //public string thisScene;

    void Awake()
    {
     /*   Scene thisScene = SceneManager.GetSceneByBuildIndex(0);
        SceneManager.LoadScene(thisScene.buildIndex);
        SceneManager.SetActiveScene(thisScene);*/
    }
    void Start()
    {
        //GameManager.Instance.SetCurrentScene(SceneManager.GetActiveScene().buildIndex);
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
                SceneManager.UnloadScene(i);
        }
            
    }

}
