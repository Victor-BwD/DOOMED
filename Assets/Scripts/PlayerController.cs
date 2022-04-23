using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    private Rigidbody rb;

    Vector3 direction;

    public LayerMask floorMask; // limit the ray just to hit the ground

    public GameObject gameOverText;

    Animator anim;

    public int life = 100;

    public UIController scriptUIController;

    public AudioClip damageSound;

    private PlayerMoviment myPlayerMoviment;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        myPlayerMoviment = GetComponent<PlayerMoviment>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float Xaxis = Input.GetAxis("Horizontal");
        float Zaxis = Input.GetAxis("Vertical");

        direction = new Vector3(Xaxis, 0, Zaxis).normalized;

        

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
        myPlayerMoviment.MovimentCaracter(direction, speed);

        myPlayerMoviment.PlayerRotation(floorMask);
    }

   public void TakeDamage(int damage)
    {
        life -= damage;
        scriptUIController.UpdateSliderPlayerLife();
        AudioController.instance.PlayOneShot(damageSound);
        if(life <= 0)
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
        
    }
}
