using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 10f;
    private Rigidbody rb;

    Vector3 direction;

    public LayerMask floorMask; // limit the ray just to hit the ground

    public GameObject gameOverText;

    Animator anim;

    public int life = 100;

    public UIController scriptUIController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float Xaxis = Input.GetAxis("Horizontal");
        float Zaxis = Input.GetAxis("Vertical");

        direction = new Vector3(Xaxis, 0, Zaxis);

        

        //transform.Translate(direction * speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if(life <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (direction * speed * Time.deltaTime)); // Move player from rigibody position

        Ray radius = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(radius.origin, radius.direction * 100, Color.red);

        RaycastHit impact; // check collision

        if(Physics.Raycast(radius, out impact, 100, floorMask))
        {
            Vector3 playerAimPosition = impact.point - transform.position;

            playerAimPosition.y = transform.position.y;

            Quaternion newRotation = Quaternion.LookRotation(playerAimPosition);

            rb.MoveRotation(newRotation);
        }
    }

   public void TakeDamage(int damage)
    {
        life -= damage;
        scriptUIController.UpdateSliderPlayerLife();
        if(life <= 0)
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
        
    }
}
