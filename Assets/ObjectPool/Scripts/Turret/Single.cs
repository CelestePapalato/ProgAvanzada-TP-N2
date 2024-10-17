using UnityEngine;

public class Single : Turret
{
    protected override void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        turreyHead.forward = targetDir;
    }

    protected override void InitializeProjectile(Projectile projectile, GameObject go)
    {
        projectile.target = go.transform;
    }
}
