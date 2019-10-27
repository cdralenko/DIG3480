using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public AudioClip background;
    public AudioClip victory;
    public AudioSource musicSource;

    public Text score;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    public Text winText;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        winText.text = "";
    }
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            WinLossText();
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            WinLossText();
        }

        if (collision.collider.tag == "Why")
        {
            livesValue = 3;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue == 4)
        {
            gameObject.transform.position = new Vector2(-6.05f, -58.05f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void WinLossText()
    {
        if (scoreValue >= 9)
        {
            winText.text = "You win! Game created by Ainslee Flowers.";
            musicSource.clip = victory;
            musicSource.Play();
            Destroy(rd2d);
        }

        if (livesValue == 0)
        {
            winText.text = "You lose! Try again.";
            Destroy(gameObject);
        }
    }
}