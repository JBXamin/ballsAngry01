using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 initialPosition;
    public int birdSpeed;
    public bool firstShot = true;
    public static int count;
    public void Awake()
    {
        initialPosition = transform.position;
    }

    public void OnMouseUp()
    {
        if (firstShot)
        {
            Vector2 springForce = initialPosition - transform.position;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Rigidbody2D>().AddForce(springForce * birdSpeed);
            Invoke("destroyOBJ", 4f);
            firstShot = false;
            ShootSling.shoot = true;
        }
    }

    public void OnMouseDrag()
    {
        if (firstShot)
        {
            Vector3 DragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(DragPosition.x, DragPosition.y);
        }
    }
    private void Update()
    {
        gotPPUP();
    }

    void gotPPUP()
    {
        if (GameManager.powerUp1)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            birdSpeed += 500;
            GameManager.powerUp1 = false;
        }
        if (GameManager.powerUp2)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            GameManager.powerUp2 = false;
        }
    }

    void destroyOBJ()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            count++;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PUP1")
        {
            GameManager.powerUp1 = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "PUP2")
        {
            GameManager.powerUp2 = true;
            Destroy(collision.gameObject);
        }
    }
}
