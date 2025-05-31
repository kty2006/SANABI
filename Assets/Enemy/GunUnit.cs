using System.Drawing;
using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class GunUnit : Enemy
{

    private void Start()
    {
       attackAction =  attackFactory.createOperation<GunAttack>();
        Global.EventHandler.Invoke<Enemy>(Type.Enemy, this);
        attackAction.Execute();
    }
}
