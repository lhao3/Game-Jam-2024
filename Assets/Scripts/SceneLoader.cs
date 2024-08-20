using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string goScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Load(goScreen);
    }

    public string GoScreen()
    {
        return goScreen;
    }

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
