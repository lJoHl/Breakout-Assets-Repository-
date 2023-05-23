using UnityEngine;

public class Ball : MonoBehaviour
{
    private ComboBehaviour comboBehaviour;
    private MainManager mainManager;

    private Rigidbody m_Rigidbody;
    private float velocityBooster = 0.01f;
    private float velocityLimit = 3;


    private void Start()
    {
        comboBehaviour = GameObject.Find("MainManager").GetComponent<ComboBehaviour>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        m_Rigidbody = GetComponent<Rigidbody>();
        velocityBooster = mainManager.ChangeDifficultyParameter(velocityBooster, 3, .01f, true);
        velocityLimit = mainManager.ChangeDifficultyParameter(velocityLimit, 5, 1f, true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.Find("Paddle"))
            comboBehaviour.BreakCombo();
    }

    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        // After a collision we accelerate a bit
        velocity += velocity.normalized * velocityBooster;
        
        // Checks if we are not going too horizontally as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        // Limits the speed
        if (velocity.magnitude > velocityLimit)
        {
            velocity = velocity.normalized * velocityLimit;
        }

        m_Rigidbody.velocity = velocity;
    }
}