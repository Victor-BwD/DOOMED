using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCaracters : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(bool state)
    {
        myAnimator.SetBool("isAttacking", state); // Receive a parameter from enemy script
    }

    public void AnimationMoving(float movingValue)
    {
        myAnimator.SetFloat("Running", movingValue);
    }

    public void Dead()
    {
        myAnimator.SetTrigger("Death");
    }
}
