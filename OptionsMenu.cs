using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //Hide the options menu on load
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Load a given level
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    //Exit button
    public void ExitButton()
    {
        Application.Quit();
    }

    //Method to make sure options menu doesn't duplicate
    public void DestroyOptions()
    {
        Destroy(gameObject);
    }
}
