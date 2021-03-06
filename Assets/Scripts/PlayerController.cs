using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IKilliable, ICurable
{
    Vector3 direction;

    public LayerMask floorMask; // limit the ray just to hit the ground

    public GameObject gameOverText;

    public UIController scriptUIController;

    public AudioClip damageSound;

    private PlayerMoviment myPlayerMoviment;

    private AnimationCaracters myAnimationPlayer;

    public Status statusPlayer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerMoviment = GetComponent<PlayerMoviment>();
        myAnimationPlayer = GetComponent<AnimationCaracters>();
        statusPlayer = GetComponent<Status>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float Xaxis = Input.GetAxis("Horizontal");
        float Zaxis = Input.GetAxis("Vertical");

        direction = new Vector3(Xaxis, 0, Zaxis).normalized; 

        myAnimationPlayer.AnimationMoving(direction.magnitude); // get the magnitude from vector 3 to be compatible with float

        //transform.Translate(direction * speed * Time.deltaTime);

        

        

    }

    private void FixedUpdate()
    {
        myPlayerMoviment.MovimentCaracter(direction, statusPlayer.speed);

        myPlayerMoviment.PlayerRotation(floorMask);
    }

   public void TakeDamage(int damage)
    {
        statusPlayer.health -= damage;
        scriptUIController.UpdateSliderPlayerLife();
        AudioController.instance.PlayOneShot(damageSound);
        if(statusPlayer.health <= 0)
        {
            Die();
        }
        
    }

    public void Die()
    {
        
        scriptUIController.GameOver();
    }

    public void HealHealth(int amountHealed)
    {
        statusPlayer.health += amountHealed;
        if(statusPlayer.health > statusPlayer.health_Begin)
        {
            statusPlayer.health = statusPlayer.health_Begin;
        }
        scriptUIController.UpdateSliderPlayerLife();
    }
}
