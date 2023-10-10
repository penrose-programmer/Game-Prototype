using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject attackObject;
    public GameObject EndScreen;

    public float speed = 1;
    public float jumpHeight = 1;
    public int health = 5;

    public bool onGround;

    public int dashDir = 1;
    public float dash = 1;
    public bool dashing = false;
    public float dashDuration = 1;

    private float horizontalInput;

    private Rigidbody2D playerRb;
    private FloorDetector floorDetector;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        floorDetector = GetComponentInChildren<FloorDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = floorDetector.onGround;

        if (Input.GetKeyDown(KeyCode.E) && !dashing)
        {
            dashing = true;
            attackObject.SetActive(true);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            playerRb.AddForce(new Vector2(dash * dashDir, 0), ForceMode2D.Impulse);
            StartCoroutine(EndDash());
        }


        if (!dashing)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);
        }

        if (horizontalInput > 0)
        {
            dashDir = 1;
        }
        else if (horizontalInput < 0)
        {
            dashDir = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (floorDetector.onGround)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpHeight);
                floorDetector.onGround = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!dashing)
            {
                StartCoroutine(Damaged());
            }
        }
    }
    IEnumerator Damaged ()
    {
        floorDetector.onGround = false;
        transform.position = Vector2.zero;
        playerRb.velocity = Vector2.zero;
        health--;
        if (health <= 0) { EndScreen.SetActive(true); Destroy(gameObject); }
        playerRb.simulated = false;
        yield return new WaitForSeconds(0.25f);
        playerRb.simulated = true;
    }

    IEnumerator EndDash ()
    {
        yield return new WaitForSeconds(dashDuration);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        attackObject.SetActive(false);
        dashing = false;
    }
}
