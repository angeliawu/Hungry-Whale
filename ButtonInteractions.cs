using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
