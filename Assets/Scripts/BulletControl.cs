using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    // delcares variables
    public Rigidbody2D bulletRB;
    public Rigidbody2D explosionRB;
    public GameObject explosionPrefab;
    
    public float speed;

    private GameObject gameManager;
    private GameObject clone;

    // Use this for initialization
    void Start () {
        bulletRB.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update () {

    }

    // hides object when it collides with obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // hides bullet once it hits something
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player One" || collision.gameObject.tag == "Player Two")
        {
            bulletRB.isKinematic = true;
            bulletRB.velocity = Vector2.zero;
            bulletRB.transform.localScale = new Vector2(0, 0);
            
            if (collision.gameObject.tag == "Player One")
            {
                GameObject.Find("GameManager").GetComponent<HealthManager>().updateHealth("p1", -1);
            }

            if (collision.gameObject.tag == "Player Two")
            {
                GameObject.Find("GameManager").GetComponent<HealthManager>().updateHealth("p2", -1);
            }

            // explosion effect            
            clone = Instantiate(explosionPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            StartCoroutine(Wait(true));
        }
    }

    // slows down bullet if it's in the timestop field or stops it if it is touching a forcefield
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "TimeStop Pl1" || collision.gameObject.tag == "TimeStop Pl2")
        {
            bulletRB.velocity = bulletRB.velocity * 0.1f;
            bulletRB.angularVelocity = bulletRB.angularVelocity * 0.1f;
        }

        if (collision.gameObject.tag == "ForceField")
        {
            GameObject x = GameObject.FindGameObjectsWithTag("Player One")[0];
            GameObject y = GameObject.FindGameObjectsWithTag("Player Two")[0];

            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), x.GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), y.GetComponent<Collider2D>());

            gameObject.SetActive(false);
        }
    }

    // speeds it up once it escapes
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TimeStop Pl1" || collision.gameObject.tag == "TimeStop Pl2")
        {
            bulletRB.velocity = bulletRB.velocity / 0.1f;
            bulletRB.angularVelocity = bulletRB.angularVelocity / 0.1f;
        }
    }

    IEnumerator Wait(bool isExplosion)
    {
        yield return new WaitForSeconds(0.05f);
        
        clone.SetActive(false);
        gameObject.SetActive(false);
    }
}
