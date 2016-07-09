using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
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
            Vector2 dir = currentPath[Mathf.Abs(next)].transform.position - transform.position;
            if (dir == Vector2.zero)
            {
                if (next + 1 == currentPath.Count)
                {
                    currentNode = currentPath[next];
                    currentPath = null;
                }
                else
                {
                    next++;
                    timer = 3f;
                }
            }
            else if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (Mathf.Abs(dir.magnitude - 0.5f) <= 0.4f)
                {
                    Vector3 pos = currentPath[Mathf.Abs(next)].transform.position;
                    transform.position = new Vector3(pos.x, pos.y, transform.position.z);
                } else transform.Translate(dir.normalized * speed * Time.deltaTime);
            }
        }
        else
        {
            SearchPath();
            next = 0;
        }
    }

    private void SearchPath()
    {
        List<Waypoint> targetNodes = new List<Waypoint>(nodeList);
        targetNodes.Remove(currentNode);
        Waypoint target = targetNodes[Random.Range(0, targetNodes.Count - 1)];
        // Breadth First Search from initial waypoint 
        Waypoint current = currentNode;
        Queue<Waypoint> queue = new Queue<Waypoint>();
        List<Waypoint> path = new List<Waypoint>();
        List<Waypoint> explored = new List<Waypoint>();
        Dictionary<Waypoint, List<Waypoint>> partialPaths = new Dictionary<Waypoint, List<Waypoint>>();
        queue.Enqueue(current);
        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            explored.Add(current);
            if (partialPaths.Count > 0) path = partialPaths[current];
            path.Add(current);
            foreach (Waypoint node in current.linkedNodes)
            {
                if (explored.Contains(node)) continue;
                if (node.ID == target.ID)
                {
                    path.Add(target);
                    currentPath = path;
                    return;
                }
                else
                {
                    queue.Enqueue(node);
                }
            }
        }
    }
}
