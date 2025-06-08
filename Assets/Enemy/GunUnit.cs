using System.Drawing;
using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class GunUnit : Enemy
{

    private void Start()
    {
       attackAction =  attackFactory.createOperation<LaserAttack>();
        moveAction = moveFactory.createOperation<FollowMove>();
        Global.EventHandler.Invoke<Enemy>(Type.EnemyAttack, this);
        attackAction.Execute();
        moveAction.Execute();
    }
}
