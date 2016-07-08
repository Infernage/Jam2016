using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour
{

    public float visionDistance, visionAngle;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        player.transform.Translate(0.1f, 0, 0);
        if (canSeePlayer())
        {
            print("Te veo!!!");
        }else
        {
            print("No te veo!!!");
        }
    }

    public bool canSeePlayer()
    {
        bool result = false;

        Vector2 targetDirection = player.transform.position - transform.position;
        Vector2 forward = transform.up;
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if(Vector2.Angle(forward, targetDirection) <= visionAngle && distance <= visionDistance)
        {
            result = true;
        }

        return result;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;

        Gizmos.DrawRay(transform.position, Vector2.up * visionDistance);
    }
}
