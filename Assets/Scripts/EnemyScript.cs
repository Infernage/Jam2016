using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
    public float speed = 0;
    public Waypoint init;
    private List<List<Waypoint>> paths = new List<List<Waypoint>>();
    private List<Waypoint> currentPath;
    private int next;
    private float timer = 3f;

	// Use this for initialization
	void Start () {
        if (init == null) return;
        if (speed == 0) speed = 5;

        // Breadth First Search from initial waypoint
        Waypoint current = init;
        Queue<Waypoint> queue = new Queue<Waypoint>();
        List<Waypoint> path = new List<Waypoint>();
        List<Waypoint> explored = new List<Waypoint>();
        Dictionary<Waypoint, List<Waypoint>> partialPaths = new Dictionary<Waypoint, List<Waypoint>>();
        queue.Enqueue(init);
        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            explored.Add(current);
            if (partialPaths.Count > 0) path = partialPaths[current];
            path.Add(current);
            foreach (Waypoint node in current.linkedNodes)
            {
                if (explored.Contains(node)) continue;
                if (node.IsLeaf())
                {
                    List<Waypoint> newPath = new List<Waypoint>(path);
                    newPath.Add(node);
                    paths.Add(newPath);
                }
                else
                {
                    queue.Enqueue(node);
                    partialPaths.Add(node, new List<Waypoint>(path));
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (init == null) return;

        if (currentPath != null)
        {
            Vector2 dir = currentPath[Mathf.Abs(next)].transform.position - transform.position;
            if (dir == Vector2.zero)
            {
                if (next + 1 == currentPath.Count) next = -next;
                next++;
                timer = 3f;
                if (next == 0) currentPath = null;
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
            currentPath = new List<Waypoint>(paths[Random.Range(0, paths.Count - 1)]);
            next = 0;
        }
    }
}
