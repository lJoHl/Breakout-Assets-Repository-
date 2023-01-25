using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    private float velocityBooster = 0.01f;
    private float velocityLimit = 3;

    private ComboBehaviour comboBehaviour;
    private MainManager mainManager;


    void Start()
    {
        comboBehaviour = GameObject.Find("MainManager").GetComponent<ComboBehaviour>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        velocityBooster = mainManager.ChangeDifficultyParameter(velocityBooster, 3, .01f, true);
        velocityLimit = mainManager.ChangeDifficultyParameter(velocityLimit, 5, 1f, true);

        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)  //inGame Branch
    {
        if (collision.gameObject == GameObject.Find("Paddle"))
            comboBehaviour.breakCombo();
    }

    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * velocityBooster;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > velocityLimit)
        {
            velocity = velocity.normalized * velocityLimit;
        }

        m_Rigidbody.velocity = velocity;
    }
}
