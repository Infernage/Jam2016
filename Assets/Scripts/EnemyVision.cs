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
        canSeePlayer();
    }

    public void canSeePlayer()
    {

        Vector2 targetDirection = player.transform.position - transform.position;
        Vector2 forward = transform.up;
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if(Vector2.Angle(forward, targetDirection) <= visionAngle && distance <= visionDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, visionDistance);
            if(hit.collider != null && hit.collider.Equals(player.GetComponent<BoxCollider2D>()))
            {
                LevelManager manager = FindObjectOfType<LevelManager>();
                manager.playerDetected = true;
            }
        }

    }

    /*public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 direction = player.transform.position - transform.position;

        Gizmos.DrawRay(transform.position, Vector2.up * visionDistance);
    }*/
}
