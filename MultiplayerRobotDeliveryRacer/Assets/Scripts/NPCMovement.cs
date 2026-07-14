using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(patrolPoints[targetPoint].position.x, transform.position.y, patrolPoints[targetPoint].position.z);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            IncreaseTargetInt();
        }

        target = new Vector3(patrolPoints[targetPoint].position.x, transform.position.y, patrolPoints[targetPoint].position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void IncreaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length) { targetPoint = 0; }
    }
}
