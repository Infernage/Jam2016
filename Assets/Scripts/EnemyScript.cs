using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
    public bool debug = true;
    public float speed = 0;
    public Waypoint currentNode;
    private List<Waypoint> nodeList;
    private List<Waypoint> currentPath;
    private int next;
    private float timer = 3f;

	// Use this for initialization
	void Start () {
        if (currentNode == null) return;
        if (speed == 0) speed = 5;

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject obj in waypoints)
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Breadth First Search from initial waypoint
        Waypoint current = currentNode;
        Queue<Waypoint> queue = new Queue<Waypoint>();
        nodeList = new List<Waypoint>();
        queue.Enqueue(current);
        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            nodeList.Add(current);
            foreach (Waypoint node in current.linkedNodes)
            {
                if (nodeList.Contains(node)) continue;
                queue.Enqueue(node);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentNode == null) return;

        if (currentPath != null)
        {
            Vector2 dir = currentPath[next].transform.position - transform.position;
            if (dir == Vector2.zero)
            {
                transform.LookAt(currentPath[next].transform);
                if (next + 1 == currentPath.Count)
                {
                    currentNode = currentPath[next];
                    currentPath = null;
                    timer = 3f;
                }
                else
                {
                    next++;
                    timer = 1f;
                }
            }
            else if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (Mathf.Abs(dir.magnitude) <= (speed / 10))
                {
                    Vector3 pos = currentPath[Mathf.Abs(next)].transform.position;
                    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
                } else transform.Translate(dir.normalized * speed * Time.deltaTime);
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
}
