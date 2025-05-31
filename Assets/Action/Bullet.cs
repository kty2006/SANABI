using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.transform.Translate(Vector3.right * 3 * Time.deltaTime);
    }
}
