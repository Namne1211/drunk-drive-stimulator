using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcar : MonoBehaviour
{
    public Vector3 destination;
    public bool reachedDestination;
  

    [SerializeField] private float movementSpeed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
    }
    void Update()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if(destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
