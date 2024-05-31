using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingCharacter : MonoBehaviour
{
    public float roamRadius = 10f; // Radius within which the character can roam
    public float waitTime = 3f;    // Time to wait at each point
    public float speed = 3.5f;     // Movement speed of the character

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float timer;
    private bool isWaiting = false;

    void Start()
    {
        initialPosition = transform.position;
        SetNewTargetPosition();
    }

    void Update()
    {
        if (isWaiting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                isWaiting = false;
                SetNewTargetPosition();
            }
        }
        else
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isWaiting = true;
            timer = waitTime;
        }
    }

    void SetNewTargetPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle * roamRadius;
        targetPosition = initialPosition + new Vector3(randomDirection.x, 0, randomDirection.y);
    }
}
