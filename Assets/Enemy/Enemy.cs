using UnityEngine;

struct States
{
    int MaxHp;
    int CurrentHp;
    int Power;
    int MoveSpeed;
}

public abstract class Enemy : MonoBehaviour
{
    //юс╫ц
    public GameObject Bullet;
    public GameObject Target;

    protected IAction attackAction;
    protected AttackFactory attackFactory = new();

    public GameObject CreatBullet(Vector3 euler)
    {
        return Instantiate(Bullet,transform.position,Quaternion.Euler(euler));
    }
}
