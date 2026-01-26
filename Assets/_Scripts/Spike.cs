using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Animator animator;
    public float interval = 3f;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpikeLoop());
    }

    IEnumerator SpikeLoop()
    {
        while (true)
        {
            animator.SetTrigger("Out");
            yield return new WaitForSeconds(interval);

            animator.SetTrigger("In");
            yield return new WaitForSeconds(interval);
        }
    }
}
