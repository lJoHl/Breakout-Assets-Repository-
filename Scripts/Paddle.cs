using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement;
    private const float maxMovementInitialValue = 2.0f;

    private MainManager mainManager;
    private bool rescaled;

    private bool lockLeft;
    private bool lockRight;

    
    // Start is called before the first frame update
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        MaxMovement = maxMovementInitialValue;
    }


    // Update is called once per frame
    void Update()
    {
        Rescale();


        lockLeft = Input.GetKey(ControlsSettings.moveRightKey) & !lockRight;
        lockRight = Input.GetKey(ControlsSettings.moveLeftKey) & !lockLeft;

        float input = lockLeft ? 1 : lockRight ? -1 : 0;

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;
    }

    private void Rescale()
    {
        if (transform.childCount > 0)
        {
            if (!rescaled)
            {
                MaxMovement = maxMovementInitialValue;
                MaxMovement = mainManager.ChangeDifficultyParameter(MaxMovement, 5, .015f, true);

                rescaled = true;
            }
        }
        else rescaled = false;
    }
}
