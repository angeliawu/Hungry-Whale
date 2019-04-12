using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Public variable for player going forward
    [HideInInspector]
    public GameObject forwardWhale;

    //Public variable for player going backward
    [HideInInspector]
    public GameObject backwardWhale;

    //Start is called before the first frame update
    void Start()
    {
        //Keep updated health bar and gem count from last level
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Slider healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        healthBar.value = gameManager.health;
        Text gemsText = GameObject.FindGameObjectWithTag("GemsText").GetComponent<Text>();
        gemsText.text = "Gems: " + gameManager.gemsCollected;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player pressed the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Jumping mechanic for the whale to swim- 2D uses Rigidbody2D and Vector2
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250);
        }

        //Check if player is currently pressing the right arrow
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Player moves right if right arrow key is being pressed
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10);

            //Use the forward facing penguin if moving right
            forwardWhale.SetActive(true);

            //Set the backward facing penguin inactive if moving right
            backwardWhale.SetActive(false);
        }

        //Check if player is currently pressing the left arrow
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Player moves left if right arrow key is being pressed
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10);

            //Use the backward facing penguin if moving left
            backwardWhale.SetActive(true);

            //Set the forward facing penguin inactive if moving left
            forwardWhale.SetActive(false);
        }
    }

    //Functions for objects that trigger actions
    void OnTriggerEnter2D(Collider2D other)
    {   
        //Variable for game manager
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        //Check if player gets a gem
        if (other.tag == "Gem")
        {
            //Destroy the gem if the player gets it
            Destroy(other.gameObject);

            //Add one to the coins collected count
            gameManager.gemsCollected++;

            //Display the change of coins on the UI
            Text gemsText = GameObject.FindGameObjectWithTag("GemsText").GetComponent<Text>();
            gemsText.text = "Gems: " + gameManager.gemsCollected;

            //Debug log to check coins are being processed correctly
            Debug.Log("Gems: " + gameManager.gemsCollected);
        }

        //Save point
        else if (other.tag == "SavePoint")
        {
            //Tell the GameManager the most recent save point
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().currentSavePoint = other.transform.position;
        }

        //Check if player gets the key
        else if (other.tag == "Key")
        {
            //Change the hasKey variable to true so the door can be unlocked
            gameManager.hasKey = true;

            //Destroy the key
            Destroy(other.gameObject);
        }

        //Check if the player gets to the door
        else if (other.tag == "Door")
        {
            //Nothing will happen unless the player has the key
            if (gameManager.hasKey)
            {
                //Triggers door to open if player has key
                other.GetComponent<Animator>().SetTrigger("Open");
            }
        }

        //Check if player hits an object that causes damage
        else if (other.tag == "Damage")
        {
            //Player health goes down by 10 if an obstacle is hit
            gameManager.health = gameManager.health - 10;

            //Display the change of health on the UI
            Slider healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();

            //Update health bar slider based on new health value
            healthBar.value = gameManager.health;

            //Debug log to check if health is being processed correctly
            Debug.Log("Health: " + gameManager.health);

            //Check if player dies
            if (gameManager.health <= 0)
            {
                //Player dies if health is 0
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PlayerDeath();

                //Reset the health
                gameManager.health = 100;
                healthBar.value = gameManager.health;
            }
        }

        //Check for death
        else if (other.tag == "Death")
        {
            //Find game object
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PlayerDeath();

            //Reset the health
            gameManager.health = 100;
            Slider healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
            healthBar.value = gameManager.health;
        }

        //Progress to next level if player gets the fish
        else if (other.tag == "Fish")
        {
            //Destroy the fish if the player gets it
            Destroy(other.gameObject);

            //Subtract one from the fish left
            gameManager.fishLeft--;

            //Display the change of fish on the UI
            Text fishText = GameObject.FindGameObjectWithTag("FishText").GetComponent<Text>();
            fishText.text = "Fish Left: " + gameManager.fishLeft;

            //Find game object
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ProgressToNextLevel();
        }

    }
}
