using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
    public bool debug = true;
    public float speed = 0;
    private Waypoint currentNode;
    private List<Waypoint> nodeList;
    private List<Waypoint> currentPath;
    private int next;
    private float timer = 3f;
    private float rotateTimer;
    private Transform spriteTransform;

	// Use this for initialization
	void Start () {
        if (speed == 0) speed = 5;

        spriteTransform = transform.FindChild("Sprite");

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        nodeList = new List<Waypoint>();
        foreach (GameObject obj in waypoints)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
            nodeList.Add(obj.GetComponent<Waypoint>());
        }

        // Get random node
        currentNode = nodeList[Random.Range(0, nodeList.Count - 1)];
        transform.position = currentNode.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentPath != null)
        {
            Vector2 dir = currentPath[next].transform.position - transform.position;
            if (dir == Vector2.zero)
            {
                //spriteTransform.LookAt(currentPath[next].transform.position);
                if (next + 1 == currentPath.Count)
                {
                    currentNode = currentPath[next];
                    currentPath = null;
                    timer = 3f;
                    rotateTimer = Random.Range(0.5f, 1.5f);
                }
                else
                {
                    next++;
                    //timer = 1f;
                    Rotate();
                }
            }
            else if (timer > 0)
            {
                timer -= Time.deltaTime;
                rotateTimer -= Time.deltaTime;
                if (rotateTimer <= 0)
                {
                    spriteTransform.rotation = Quaternion.Euler(0, 0, Random.Range(1, 7) * 45);
                    rotateTimer = Random.Range(0.5f, 1.5f);
                }
            }
            else
            {
                if (Mathf.Abs(dir.magnitude) <= (speed / 10))
                {
                    Vector3 pos = currentPath[Mathf.Abs(next)].transform.position;
                    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
                }
                else
                {
                    Rotate();
                    transform.Translate(dir.normalized * speed * Time.deltaTime);
                }
            }
        }
        else
        {
            List<Waypoint> targetNodes = new List<Waypoint>(nodeList);
            targetNodes.Remove(currentNode);
            Waypoint target = targetNodes[Random.Range(0, targetNodes.Count - 1)];
            List<Waypoint> path = new List<Waypoint>();
            List<Waypoint> explored = new List<Waypoint>();
            // Breadth First Search from initial waypoint
            SearchPath(target, currentNode, path, explored);
            next = 0;
        }
    }

    private void SearchPath(Waypoint target, Waypoint expanded, List<Waypoint> path, List<Waypoint> explored)
    {
        if (explored.Contains(expanded)) return;
        explored.Add(expanded);
        path.Add(expanded);
        foreach (Waypoint node in expanded.linkedNodes)
        {
            if (explored.Contains(node)) continue; // Ignore duplicates
            if (node.ID == target.ID)
            {
                // Found path!
                path.Add(target);
                currentPath = path;
                return;
            }
            else
            {
                SearchPath(target, node, path, explored);
                if (!path.Contains(target)) path.Remove(node);
                else return; // Found, we keep going back!
            }
        }
    }

    void Rotate()
    {
        Vector3 direction = currentPath[next].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteTransform = transform.FindChild("Sprite");
        spriteTransform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);
    }

    public void RotateToPlayer()
    {
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteTransform = transform.FindChild("Sprite");
        spriteTransform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);
    }
}
