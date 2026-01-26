using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float speed;
    private Vector3 objective;
    
    void Start()
    {
        objective = pointA.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, objective, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, objective) < 0.1f)
        {
            if (objective == pointA.position)
            {
                objective = pointB.position;
            }

            else if (objective == pointB.position)
            {
                objective = pointA.position;
            }
        }
    }
}
