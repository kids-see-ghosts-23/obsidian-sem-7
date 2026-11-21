using UnityEngine;

public class TransformController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;
    public float scaleSpeed = 2f;

    void Update()
    {
        // Move on the XZ plane with WASD / arrow keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime);

        // Rotate around the Y axis with Q and E
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // Scale with R and F
        if (Input.GetKey(KeyCode.R))
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.F))
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
    }
}
