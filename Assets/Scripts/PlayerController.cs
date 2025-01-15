using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Speed of the player mouvement
    public float speed;

    // health of the player
    public int health;

    private Rigidbody rb;
    private float vertical;
    private float horizontal;
    private int score;

    private bool isTransporting;
    private GameObject teleporterCible;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0; 
        isTransporting = false;
        teleporterCible = null;
    }

    // Update is called once per frame
    void Update()
    {
        // DEPLACEMENT
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 mouvement = new Vector3(horizontal, 0, vertical).normalized;
        rb.velocity = new Vector3(mouvement.x* speed, rb.velocity.y, mouvement.z * speed);

        if (health == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if  (teleporterCible != null)
        {
            if (isTransporting && (other.gameObject.name != teleporterCible.name))
            {
                teleporterCible.gameObject.SetActive(true);
                teleporterCible = null;
                isTransporting = false;
            }
        }
        

        if (other.gameObject.tag == "Pickup")
        {
            score += 1;
            Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Trap")
        {
            health -= 1;
            Debug.Log("Health: " + health);
        }
        else if(other.gameObject.tag == "Goal")
        {
            Debug.Log("You win!");
        }
        else if (other.gameObject.tag == "Teleporter" && !isTransporting)
        {
            isTransporting = true;
            other.gameObject.SetActive(false);
            teleporterCible  = GameObject.FindWithTag("Teleporter");
            teleporterCible.SetActive(false);
            transform.position = teleporterCible.transform.position;
            other.gameObject.SetActive(true);
        }
    }
}
