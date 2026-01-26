using System;
using System.Collections;
using UnityEngine;

public class FlyingSpike : MonoBehaviour
{
    public GameObject spike;
    public Transform toPoint;
    public float speed = 1;
    public Vector3 originalPosition;
    void Start()
    {
        originalPosition = spike.transform.position;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrap());
        }
    }

    IEnumerator ActivateTrap()
    {
        spike.transform.position = Vector3.MoveTowards(transform.position, toPoint.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(2);
        spike.transform.position = originalPosition;
    }
}
