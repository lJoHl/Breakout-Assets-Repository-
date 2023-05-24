using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    public UnityEvent onAllDestroyed;

    public int pointValue;


    // Assigns a general material with a specific color based on its "pointValue"
    private void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new();
        switch (pointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", Color.green);
                break;
            case 2:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 5:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }
        renderer.SetPropertyBlock(block);
    }


    private void OnCollisionEnter(Collision other)
    {
        tag = "Untagged";

        onDestroyed.Invoke(pointValue);
        StartCoroutine(WaitForOnAllDestroyed());

        // Slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
    private IEnumerator WaitForOnAllDestroyed()
    {
        // Gives the brick time to remove its tag
        yield return new WaitForSeconds(.1f);

        // Checks if all bricks have been destroyed
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
            onAllDestroyed.Invoke();
    }
}