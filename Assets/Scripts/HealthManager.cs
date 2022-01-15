using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    // declares variables
    public Text p1HealthText;
    public Text p2HealthText;
    public Text winner;

    private int health;

	// Use this for initialization
	void Start () {
        // send message from bullet to here, access text, change the text value
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // method to update health, called in the BulletControl script
    public void updateHealth(string player, int healthChange)
    {
        // changes health as long as it isn't equal to zero
        if (health + healthChange >= 0)
        {
            health += healthChange;
        } else
        {
            health = 0;
        }

        // updates to proper place, if the health is zero, then the other player wins
        if (player == "p1")
        {
            p1HealthText.text = health.ToString();

            if (health == 0)
            {
                winner.text = "Player Two Wins!";
            }
        } else if (player == "p2")
        {
            p2HealthText.text = health.ToString();

            if (health == 0)
            {
                winner.text = "Player One Wins!";
            }
        } else
        {
            Debug.Log("There was an error!");
        }
    }
}
