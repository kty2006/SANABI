using Cysharp.Threading.Tasks;
using UnityEngine;

public class GunAttack : IAction
{
    private Enemy enemy;
    Vector3 dir;
    public void Execute()
    {
        Shoot().Forget();
    }

    async UniTaskVoid Shoot()
    {
        while(true)
        {
            enemy.CreatBullet(new Vector3(0,0,Angle()));
            await UniTask.WaitForSeconds(1);
        }
    }

    private float Angle()
    {
        dir = enemy.Target.transform.position - enemy.transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    public void Setting()
    {
        Global.EventHandler.Register<Enemy>(Type.Enemy, (ev) =>
        {
            enemy = ev;
        });
    }
}
