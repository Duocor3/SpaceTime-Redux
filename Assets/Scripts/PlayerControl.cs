using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    // declares variables
    private Rigidbody2D playerRB;
    public Transform player;
    public Transform bulletPrefab;
    public GameObject abilitySprite;

    private GameObject[] shipChoiceGame = new GameObject[3];
    private Transform bulletClone;

    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;
    public KeyCode fire;
    public KeyCode ability;

    public float speed;
    public float rotateSpeed;
    public string playerTag;

    private float fireTimer;
    private float fireRate;
    private bool abilityActivated;
    private bool inTimeStop;

    // Use this for initialization
    void Start () {

        switch (GameObject.Find("GameManager").GetComponent<PlayerManager>().ShipChoice()[0])
        {
            case 0:
                shipChoiceGame[0] = GameObject.Find("Players/Player One");
                Debug.Log("Working on it!");
                disableEveryoneElse(GameObject.Find("Players"), shipChoiceGame[0]);
                break;

            case 1:
                shipChoiceGame[0] = GameObject.Find("Players/Player One");
                disableEveryoneElse(GameObject.Find("Players"), shipChoiceGame[0]);
                break;

            case 2:
                shipChoiceGame[0] = GameObject.Find("Players/Player One Sharpshooter");
                disableEveryoneElse(GameObject.Find("Players"), shipChoiceGame[0]);
                break;
        }

        switch (GameObject.Find("GameManager").GetComponent<PlayerManager>().ShipChoice()[2])
        {
            case 0:
                shipChoiceGame[2] = GameObject.Find("Players/Player Two");
                Debug.Log("Working on it!");
                disableEveryoneElse(GameObject.Find("Players"), shipChoiceGame[2]);
                break;

            case 1:
                shipChoiceGame[2] = GameObject.Find("Players/Player Two");
                disableEveryoneElse(GameObject.Find("Players"), shipChoiceGame[2]);
                break;

            case 2:
                shipChoiceGame[2] = GameObject.Find("Players/Player Two Sharpshooter");
                disableEveryoneElse(GameObject.Find("Players"), shipChoiceGame[2]);
                break;
        }

        if (playerTag == "TimeStop Pl1")
        {
            playerRB = shipChoiceGame[0].GetComponent<Rigidbody2D>();
        } else
        {
            playerRB = shipChoiceGame[2].GetComponent<Rigidbody2D>();
        }

        abilitySprite.transform.localScale = new Vector2(0, 0);
        abilitySprite.SetActive(false);

        fireRate = 0f;
        abilityActivated = false;
    }

    // Update is called once per frame
    void Update () {
        // increments firing timer
        fireTimer += Time.deltaTime;

        // checks if timestop is activated
        if (abilityActivated)
        {
            if (inTimeStop)
            {
                changeFireRate("DOUBLE TROUBLE", true);
            } else
            {
                changeFireRate(playerTag, true);
            }

            abilitySprite.SetActive(true);
        }
        else
        {
            if (!inTimeStop) {
                changeFireRate(playerTag, false);
            }
            else
            {
                changeFireRate(playerTag, true);
            }

            abilitySprite.SetActive(false);
        }

        // moves player forward when W is pressed
        if (Input.GetKey(forward))
        {
            playerRB.AddForce(transform.right * speed);
        }

        // moves player backward when S is pressed
        if (Input.GetKey(backward))
        {
            playerRB.AddForce(transform.right * -speed/2);
        }

        // turns player right
        if (Input.GetKey(left))
        {
            playerRB.rotation += rotateSpeed;
        }

        // turns player left
        if (Input.GetKey(right))
        {
            playerRB.rotation -= rotateSpeed;
        }

        // shoots weapon with a timer so there's a fire rate
        if (Input.GetKey(fire))
        {
            if (fireTimer >= fireRate)
            {
                bulletClone = Instantiate(bulletPrefab, transform.position + (transform.right * 0.5f), Quaternion.Euler(0, 0, player.eulerAngles.z)) as Transform;
                Physics2D.IgnoreCollision(bulletClone.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                fireTimer = 0f;
            }
        }

        // activates special move
        if (Input.GetKey(ability))
        {
            abilityActivated = true;
            abilitySprite.transform.position = player.transform.position;

            // for both the if and the else, it checks what type of player it is
            if (player.name.Contains("Sharpshooter"))
            {
                abilitySprite.tag = "ForceField";
                abilitySprite.transform.localScale = new Vector2(0.7f, 0.7f);
            }
            else
            {
                abilitySprite.transform.localScale = new Vector2(3.5f, 3.5f);
            }

        } else
        {
            abilityActivated = false;
            abilitySprite.transform.localScale = new Vector2(0, 0);
        }
    }

    private void changeFireRate(string player, bool slow)
    {
        if (player == "TimeStop Pl1")
        {
            if (slow)
            {
                GameObject.Find(shipChoiceGame[0].name).GetComponent<PlayerControl>().fireRate = 0.2f;
            } else
            {
                GameObject.Find(shipChoiceGame[0].name).GetComponent<PlayerControl>().fireRate = 0f;
            }
        }
        else if (player == "TimeStop Pl2")
        {
            if (slow)
            {
                GameObject.Find(shipChoiceGame[2].name).GetComponent<PlayerControl>().fireRate = 0.2f;
            }
            else
            {
                GameObject.Find(shipChoiceGame[2].name).GetComponent<PlayerControl>().fireRate = 0f;
            }
        } else
        {
            GameObject.Find(shipChoiceGame[0].name).GetComponent<PlayerControl>().fireRate = 2f;
            GameObject.Find(shipChoiceGame[2].name).GetComponent<PlayerControl>().fireRate = 2f;
        }
    }

    private void disableEveryoneElse(GameObject parent, GameObject special)
    {
        foreach (Transform child in parent.transform)
        {
            if ((child.gameObject.name != special.name) && (child.GetComponent<PlayerControl>().playerTag == special.gameObject.GetComponent<PlayerControl>().playerTag))
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != playerTag && collision.gameObject.tag != "ForceField")
        {
            speed *= 0.1f;
            rotateSpeed *= 0.5f;
            inTimeStop = true;
            Debug.Log(fireRate + playerTag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != playerTag && collision.gameObject.tag != "ForceField")
        {
            speed /= 0.1f;
            rotateSpeed /= 0.5f;
            inTimeStop = false;
            Debug.Log(fireRate + playerTag);
        }
    }
}
