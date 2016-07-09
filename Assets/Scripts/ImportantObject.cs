using UnityEngine;
using System.Collections;

public class ImportantObject : MonoBehaviour {

    public float minimum = 0.5f;
    public float maximum = 1f;
    public float duration = 5.0f;
    private float startTime;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }
    void Update()
    {
        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
    }
}
