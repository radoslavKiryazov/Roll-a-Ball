using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public float speedIncrementer = 2;

    private int count;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public float jumpForce = 2.0f;
    private bool isGrounded;

    public float fallThreshold = -10.0f;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        updateCountText();

        winTextObject.SetActive(false);

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f);


        if (transform.position.y < fallThreshold)
        {
            Die();
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            speed += speedIncrementer;
            updateCountText();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void OnJump()
    {
        if (isGrounded) // Only allow jump if the player is grounded
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }


    void updateCountText()
    {
        countText.text = "Score: " + count.ToString() + "\n" + "Speed: " + speed.ToString();

        if (count >= 12)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }


    void Die()
    {
        Destroy(gameObject);
        gameObject.SetActive(false);
        winTextObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }

}
