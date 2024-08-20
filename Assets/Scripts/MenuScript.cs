using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] public AudioManager audioManager;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EnterOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void EnterTutorial()
    {
        StartCoroutine(EnterTutorialCoroutine());
        
    }

    private IEnumerator EnterTutorialCoroutine()
    {
        //audioManager.Stop("mainmenumusic");
        yield return null;
        SceneManager.LoadScene("TutorialLevel1");
    }
}
