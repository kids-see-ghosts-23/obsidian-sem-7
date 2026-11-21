using UnityEngine;

public class Raycast2DTarget : MonoBehaviour
{
    public float rayDistance = 10f;

    void Update()
    {
        Vector2 origin = transform.position;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouse - origin).normalized;

        // Red line in the Scene view
        Debug.DrawRay(origin, direction * rayDistance, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name + " | Distance: " + hit.distance);
                hit.collider.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }
}
