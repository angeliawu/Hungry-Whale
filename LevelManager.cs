using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip ("The text to print")]
    public string levelText;

    [Tooltip ("How many lives the player has before they lose")]
    public int lives;

    [Tooltip("The color of the player")]
    public Color playerColor;

    [Tooltip("The save points")]
    public Transform[] savePoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
