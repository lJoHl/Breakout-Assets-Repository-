using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private MainManager manager;

    private void Start()
    {
        manager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        manager.lives--;

        if (manager.lives > 0)
            manager.NewBall();
        else
            manager.GameOver();
    }
}