using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEnemyController : MonoBehaviour
{
    private bool isBeingAttacked = false;

    private Rigidbody2D enemyRb;
    private GameObject player;

    public float speedOffset;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Destroy(gameObject);
        }

        if (player.transform.position.x < transform.position.x)
        {
            enemyRb.velocity = (player.transform.position - transform.position) - new Vector3(speedOffset, 0f);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            enemyRb.velocity = (player.transform.position - transform.position) + new Vector3(speedOffset, 0f);
        }

        if (health <= 0)
        {
            isBeingAttacked = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack" && !isBeingAttacked)
        {
            isBeingAttacked = true;
            health--;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack")
        {
            isBeingAttacked = false;
        }
    }
}
