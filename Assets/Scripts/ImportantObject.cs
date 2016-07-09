using UnityEngine;
using System.Collections;

public class ImportantObject : MonoBehaviour
{

    public float minimum = 0.6f;
    public float maximum = 1f;
    public float duration = 2.0f;
    private float startTime;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
        Invoke("fadeIn",0f);
    }

    void fadeIn()
    {
        startTime = Time.time;
        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(minimum, maximum, t));
        Invoke("fadeOut",0.3f);
    }

    void fadeOut()
    {
        startTime = Time.time;
        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(maximum, minimum, t));
        Invoke("fadeIn",0.3f);
    }
}
