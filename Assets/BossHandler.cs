using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    public GameObject congrats;

    public AudioSource music, death;

    private Rigidbody[] rbs;

    private Animator animator;

    private bool dead;

    private void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        foreach(Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }
    }

    public void Dead()
    {
        if (!dead)
        {
            music.Stop();
            death.Play();

            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
            }

            animator.enabled = false;

            Invoke("End", 2f);

            dead = true;
        }
    }

    public void End()
    {
        congrats.SetActive(true);
    }
}
