using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    float lifetime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        transform.Translate(Vector2.right * 200f * Time.deltaTime);
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
