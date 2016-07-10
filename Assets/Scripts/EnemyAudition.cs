using UnityEngine;
using System.Collections;

public class EnemyAudition : MonoBehaviour
{

    private GameObject player;
    private CharacterScript characterScript;
    private LevelManager levelScript;
    private EnemyScript enemyScript;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterScript = player.GetComponent<CharacterScript>();
        levelScript = FindObjectOfType<LevelManager>();
        enemyScript = GetComponent<EnemyScript>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //If the player is crouched, then nothing happens
        //If the player is standing, he will be detected
        if (collision.gameObject == player && !characterScript.isCrouched()
            && !Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 targetDirection = player.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection);

            if(hit.collider != null && hit.collider.Equals(player.GetComponent<BoxCollider2D>()))
            {
                print("Te he oído");
                levelScript.playerDetected = true;
                enemyScript.RotateToPlayer();
            }
        }
    }
}
