using System.Drawing;
using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class GunEnemy : Enemy
{

    private void Start()
    {
        attackAction = attackFactory.createOperation<ShootAttack>(this);
        moveAction = moveFactory.createOperation<FollowMove>(this);
        //Global.EventHandler.Invoke<Enemy>(Type.EnemyAttack, this);
        attackAction.Execute();
        moveAction.Execute();
    }

    
}
