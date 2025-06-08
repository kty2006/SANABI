using Cysharp.Threading.Tasks;
using UnityEngine;

public class FollowMove : ActionType
{
    private int index = 0;
    private bool trigger = false;
    private float value = 0;

    public override void Execute()
    {
        FllowUnit().Forget();
        
    }

    async UniTaskVoid FllowUnit()
    {
        while (true)
        {
            Enemy.transform.position = (trigger) ?
                Vector3.Lerp(Enemy.points[index].position, Enemy.points[index - 1].position, value) :
                Vector3.Lerp(Enemy.points[index].position, Enemy.points[index + 1].position, value);
            Debug.Log(Enemy.gameObject.name);
            value += Time.deltaTime;
            await UniTask.Yield();
            if (value >= 1)
            {
                value = 0;

                index = (trigger) ? index - 1 : index + 1;

                if (index >= Enemy.points.Length - 1)
                {
                    trigger = true;
                }
                if (index <= 0)
                {
                    trigger = false;
                }
            }
        }
    }

    public override void Setting(Enemy enemy)
    {
        base.Setting(enemy);
    }
}