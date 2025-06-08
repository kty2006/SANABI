using System;
using UnityEngine;

[Serializable]
public struct States
{
    public int MaxHp;
    public int CurrentHp;
    public int Power;
    public int MoveSpeed;
    public int AttackSpeed;
}



public abstract class Enemy : MonoBehaviour
{
    //юс╫ц
    public DataFactory DataFactory;
    public GameObject Target;
    public RaycastHit2D Hit;
    [field: SerializeField]
    public Transform[] points { get; private set; }

    [SerializeField]
    protected States states;
    protected ActionType attackAction;
    protected ActionType moveAction;
    protected AttackFactory attackFactory = new();
    protected AttackFactory moveFactory = new();

    public States GetStates()
    {
        return states;
    }

    public void Update()
    {
        Hit = Physics2D.CircleCast(transform.position, 6, Vector2.zero, 0f, LayerMask.GetMask("Player"));
    }
}
