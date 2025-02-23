using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    private int count;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

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
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("PickUp")) {
        other.gameObject.SetActive(false);
        count++;
            updateCountText();
        }
    }

    void OnMove(InputValue movementValue) { 
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }


    void updateCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12) {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
