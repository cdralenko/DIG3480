using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;

    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        SetCountText();
        lives = 3;
        SetLivesText();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.SetActive(false);
                lives = lives - 1;
                SetLivesText();
            }

            if (count == 12)
            {
                transform.position = new Vector2(0f, -50f);
            }
              
        
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "WINNER! Game created by Ainslee Flowers!";
            Destroy(gameObject);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            winText.text = "You lose!";
            Destroy(gameObject);
        }
    }
}
