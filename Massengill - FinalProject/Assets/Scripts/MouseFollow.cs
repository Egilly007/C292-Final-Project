using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }
}
