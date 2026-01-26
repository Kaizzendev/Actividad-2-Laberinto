using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public GameObject button;
    public Camera cam;
    private bool isActivated = false;
    public float cameraTime = 1f;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        isActivated = true;
        StartCoroutine(DoorSequence());
    }

    IEnumerator DoorSequence()
    {
        cam.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("isActivated");

        yield return new WaitForSeconds(cameraTime);
        
        cam.gameObject.SetActive(false);
        
    }
}
