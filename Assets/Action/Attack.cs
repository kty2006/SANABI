using Cysharp.Threading.Tasks;
using System.Data;
using UnityEngine;

public class GunAttack : ActionType
{
    private Vector3 dir;
    public override void Execute()
    {
        Shoot().Forget();
    }

    async UniTaskVoid Shoot()
    {
        while (true)
        {
            Enemy.CreatBullet(new Vector3(0, 0, Angle()));
            await UniTask.WaitForSeconds(Enemy.GetStates().AttackSpeed);
        }
    }

    private float Angle()
    {
        dir = Enemy.Target.transform.position - Enemy.transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }


    public override void Setting()
    {
        base.Setting();
    }
}

public class LaserAttack : ActionType
{
    public Vector3 dir;
    public GameObject LaserObj;
    public override void Execute()
    {
        Shoot().Forget();
    }

    async UniTaskVoid Shoot()
    {
        while (true)
        {
            LaserObj = Enemy.Instantiate(Enemy.DataFactory.LaserObj, Enemy.transform.position, Quaternion.Euler(0, 0, Angle() + 180));
            LaserObj.transform.parent = Enemy.transform;
            while (LaserObj.transform.localScale.y > 0)
            {
                LaserObj.transform.localScale -= new Vector3(0, Time.deltaTime, 0);
                LaserObj.transform.rotation = Quaternion.Euler(0, 0, Angle() + 180);
                await UniTask.Yield();
            }
            Enemy.Destroy(LaserObj);
            Enemy.CreatBullet(new Vector3(0, 0, Angle()));
            await UniTask.WaitForSeconds(Enemy.GetStates().AttackSpeed);
        }
    }

    private float Angle()
    {
        dir = Enemy.Target.transform.position - Enemy.transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    public override void Setting()
    {
        base.Setting();
    }
}
