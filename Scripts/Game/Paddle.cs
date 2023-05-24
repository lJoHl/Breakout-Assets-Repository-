using UnityEngine;

public class Paddle : MonoBehaviour
{
    private MainManager mainManager;

    private bool readjusted;

    private const float speed = 2.0f;

    private float maxMovement;
    private const float maxMovementInitialValue = 2.0f;

    private bool lockLeft;
    private bool lockRight;

    
    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        maxMovement = maxMovementInitialValue;
    }


    // Handles the movement of the paddle
    private void Update()
    {
        ReadjustMaxMovement();

        lockLeft = Input.GetKey(ControlsSettings.moveRightKey) & !lockRight;
        lockRight = Input.GetKey(ControlsSettings.moveLeftKey) & !lockLeft;

        float input = lockLeft ? 1 : lockRight ? -1 : 0;

        Vector3 pos = transform.position;
        if (!mainManager.m_GameOver) pos.x += input * speed * Time.deltaTime;

        // Limits the movement
        if (pos.x > maxMovement)
            pos.x = maxMovement;
        else if (pos.x < -maxMovement)
            pos.x = -maxMovement;

        transform.position = pos;
    }
    private void ReadjustMaxMovement()
    {
        if (transform.childCount > 0)
        {
            if (!readjusted)
            {
                maxMovement = maxMovementInitialValue;
                maxMovement = mainManager.ChangeDifficultyParameter(maxMovement, 5, .015f, true);

                readjusted = true;
            }
        }
        else readjusted = false;
    }
}