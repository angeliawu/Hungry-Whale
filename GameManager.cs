using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Level variables
    private bool startPointSet = true;
    private int currentLevel;

    //Public variable to keep track of gems
    [HideInInspector]
    public int gemsCollected = 0;

    //Public variable to keep track of fish left
    [HideInInspector]
    public int fishLeft = 3;

    //Public variable to keep track of health
    [HideInInspector]
    public int health = 100;

    //Public variable for health bar
    public Slider healthBar;

    //Public variable to keep track of whether or not the player has the key
    [HideInInspector]
    public bool hasKey = false;

    //Make save point public variable to code but hidden in the scene
    [HideInInspector]
    public Vector3 currentSavePoint;

    // Start is called before the first frame update
    void Start()
    {
        //Make object exist on all scenes 
        DontDestroyOnLoad(gameObject);

        //Carry stored information through playthroughs
        currentLevel = PlayerPrefs.GetInt("Level");
        gemsCollected = PlayerPrefs.GetInt("Gems");
        fishLeft = PlayerPrefs.GetInt("Fish");
        health = PlayerPrefs.GetInt("Health");
    }

    // Update is called once per frame
    void Update()
    {
        //Save save points
        if (GameObject.FindGameObjectsWithTag("LevelManager") != null && !startPointSet)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().savePoints[0].position;
            startPointSet = true;
        }
    }

    //Function for start button must be public so button in scene knows the function exists
    public void ProgressToNextLevel()
    {
        //Increment level each time loading a new level
        currentLevel++;

        //Set player prefs to set current progress whenever reaching the next level
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.SetInt("Gems", gemsCollected);
        PlayerPrefs.SetInt("Fish", fishLeft);
        PlayerPrefs.SetInt("Health", health);

        //Load the next scene
        SceneManager.LoadScene(currentLevel);

        //Reset variables
        startPointSet = false;
        hasKey = false;
    }

    //Function for when player dies
    public void PlayerDeath()
    {
        //Set player's position to most recent save point
        GameObject.FindGameObjectWithTag("Player").transform.position = currentSavePoint;
    }

    //Start over resets all variables and sets current level to level 1
    public void StartOver()
    {
        currentLevel = 1;
        gemsCollected = 0;
        fishLeft = 3;
        health = 100;
        hasKey = false;
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.SetInt("Gems", gemsCollected);
        PlayerPrefs.SetInt("Fish", fishLeft);
        PlayerPrefs.SetInt("Health", health);
        SceneManager.LoadScene(currentLevel);
    }

    //Load save loads the farthest saved level, but only if there is a save
    public void LoadSave()
    {
        if (currentLevel >= 1)
        {
            SceneManager.LoadScene(currentLevel);
        }
    }

    //Destroy duplicate game objects
    public void DestroyGameManager()
    {
        Destroy(gameObject);
    }
}