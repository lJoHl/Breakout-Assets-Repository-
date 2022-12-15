using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    public UnityEvent onAllDestroyed;   //inGame Branch

    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
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

        onDestroyed.Invoke(PointValue);
        StartCoroutine(WaitForOnAllDestroyed());

        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);

        IEnumerator WaitForOnAllDestroyed()
        {
            yield return new WaitForSeconds(.1f);

            if (GameObject.FindGameObjectsWithTag("Brick").Length == 0) //inGame Branch
                onAllDestroyed.Invoke();    //inGame Branch
        }
    }
}
