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

public class ActionType : IProduct
{
    public Enemy Enemy;
    public virtual void Execute()
    {

    }

    public virtual void Setting()
    {
        Global.EventHandler.Register<Enemy>(Type.EnemyAttack, (ev) =>
        {
            Enemy = ev;
        });
    }

}

public abstract class Enemy : MonoBehaviour
{
    //юс╫ц
    public GameObject Bullet;
    public GameObject Target;
    public DataFactory DataFactory;
    [field: SerializeField]
    public Transform[] points { get; private set; }

    [SerializeField]
    protected States states;
    protected ActionType attackAction;
    protected ActionType moveAction;
    protected AttackFactory attackFactory = new();
    protected AttackFactory moveFactory = new();

    public GameObject CreatBullet(Vector3 euler)
    {
        return Instantiate(Bullet, transform.position, Quaternion.Euler(euler));
    }

    public States GetStates()
    {
        return states;
    }
}
