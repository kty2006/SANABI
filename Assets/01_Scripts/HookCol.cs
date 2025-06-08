using UnityEngine;

public class HookCol : MonoBehaviour
{
    [SerializeField] private Hook hook;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            hook.IsAttached = true;
        }
    }
}
