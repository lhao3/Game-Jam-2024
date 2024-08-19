using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Loader
{
    //Will be used to Manage screen Transitions (Cam Movement, Scene mangement, etc.)
    // Add Desired Scenes into Build Settings for this to function
    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

public class ScreenEdge : MonoBehaviour
{
    [SerializeField] private string goScreen;

    // Add Desired Scenes into Build Settings for this to function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Loader.Load(goScreen);
    }

    public string GoScreen()
    {
        return goScreen;
    }
}