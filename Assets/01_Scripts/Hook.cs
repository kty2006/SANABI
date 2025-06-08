using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform hook;
    [SerializeField] private float width;

    public bool IsAttached;
    private bool isHookActive;

    private Camera mainCam;
    private DistanceJoint2D joint;
    private Vector3 mouseDir;

    private Coroutine hookCoroutine;

    private void Start()
    {
        mainCam = Camera.main;
        joint = hook.GetComponent<DistanceJoint2D>();

        line.positionCount = 2;
        line.endWidth = line.startWidth = width;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;

        isHookActive = false;
        IsAttached = false;
    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        HookControl();
    }

    public void HookControl()
    {
        if(Vector2.Distance(hook.position, transform.position) < 0.1f && !isHookActive)
        {
            isHookActive = false;
            hook.gameObject.SetActive(false);
        }
        if(!IsAttached && !isHookActive)
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 15f);
        }

        joint.enabled = IsAttached;

        if(Input.GetMouseButtonDown(0) && !isHookActive && Vector2.Distance(hook.position, transform.position) < 0.1f)
        {
            hook.position = transform.position;
            hook.gameObject.SetActive(true);

            mouseDir = (mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            isHookActive = true;
            hook.GetComponent<Rigidbody2D>().DOMove(hook.position + mouseDir * 10f, 0.2f).OnKill(()=> {
                if (!IsAttached)
                {
                    isHookActive = false;
                }
            });
        }

        else if(Input.GetMouseButtonUp(0) && isHookActive)
        {
            IsAttached = false;
            isHookActive = false;
        }
    }
}
