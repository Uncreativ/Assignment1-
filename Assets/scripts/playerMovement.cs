using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D Rb;
    private Vector2 moveDirection;
    public Text coinText;
    public static int count;
    public GameObject winTextObject;
    //sounds
    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip winSound;
    public AudioClip backgroundSound;
    //particle
    public GameObject coinParticle;
    
    void Start()
    {
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        audioSource.clip = backgroundSound;
        audioSource.Play();
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        Rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject);
            count = count + 1;
            SetCountText();

            Instantiate(coinParticle,new Vector2 (other.transform.position.x, other.transform.position.y), Quaternion.identity);
        }
    }

    void SetCountText()
    {
        coinText.text = count.ToString() + "/10";
        if (count >= 10)
        {
            winTextObject.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(winSound);
            coinText.text = "reset";
        }
    }
}