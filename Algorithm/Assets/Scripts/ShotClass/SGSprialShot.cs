using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SGSprialShot : SGBaseShot
{
    public float startAngle = 180f;
    public float shiftAngle = 10f;
    public float betweenDelay = 0.2f;
    private int nowIndex;
    private float delayTimer;

    public override void Shot()
    {
        if (projectileNum <= 0 || projectileSpeed <= 0f)
        {
            return;
        }

        if (_shooting)
        {
            return;
        }

        _shooting = true;
        nowIndex = 0;
        delayTimer = 0;


    }

   protected virtual void Update()
    {
        if(_shooting == false)
        {
            return;
        }

        delayTimer -= SGTimer.Instance.deltaTime;

        while( delayTimer <= 0)
        {
            SGProjectile projectile = GetProjectile(transform.position);
            if(projectile == null)
            {
                FinishedShot();
                return;
            }
            float angle = startAngle + (shiftAngle * nowIndex);
            ShotProjectile(projectile, projectileSpeed, angle);
            projectile.UpdateMove(-delayTimer);
            nowIndex++;
            FiredShot();
            if(nowIndex >= projectileNum)
            {
                FinishedShot();
                return;
            }
            delayTimer += betweenDelay;

        }
    }


}
