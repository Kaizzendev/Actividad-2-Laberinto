using System;
using System.Collections;
using UnityEngine;

public class FlyingSpike : MonoBehaviour
{
    public GameObject spike;
    public Transform toPoint;
    public float speed = 1;
    public Vector3 originalPosition;
    bool isActivated = false;
    void Start()
    {
        originalPosition = spike.transform.position;
    }

    private void Update()
    {
        if (isActivated)
        {
            ActivateTrap();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           isActivated = true;
        }
        else
        {
            isActivated = false;
        }

        
    }

    public void ActivateTrap()
    {
        spike.transform.position =
            Vector3.MoveTowards(spike.transform.position, toPoint.position, speed * Time.deltaTime);
        
    }
}
