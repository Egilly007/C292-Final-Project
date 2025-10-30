using UnityEngine;

public class NormalCrateBehavior : MonoBehaviour
{
    int hitPoints = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        hitPoints -= damageAmount;

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
