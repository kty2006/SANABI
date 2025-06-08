using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.transform.Translate(Vector3.right * 8 * Time.deltaTime);
    }
}
