using Cysharp.Threading.Tasks;
using System.Data;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;



public class ShootAttack : ActionType
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
            await UniTask.WaitUntil(() => Enemy.Hit);
            LaserObj = Enemy.Instantiate(Enemy.DataFactory.LaserObj, Enemy.transform.position, Quaternion.Euler(0, 0, Angle() + 180));
            LaserObj.transform.parent = Enemy.transform;
            while (LaserObj.transform.localScale.y > 0)
            {
                LaserObj.transform.localScale -= new Vector3(0, Time.deltaTime, 0);
                LaserObj.transform.rotation = Quaternion.Euler(0, 0, Angle() + 180);
                await UniTask.Yield();
            }
            Enemy.Destroy(LaserObj);
            Enemy.Instantiate(Enemy.DataFactory.BulletObj, Enemy.transform.position, Quaternion.Euler(new Vector3(0, 0, Angle())));
            await UniTask.WaitForSeconds(Enemy.GetStates().AttackSpeed);
        }
    }

    private float Angle()
    {
        dir = Enemy.Target.transform.position - Enemy.transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    public override void Setting(Enemy enemy)
    {
        base.Setting(enemy);
    }
}

