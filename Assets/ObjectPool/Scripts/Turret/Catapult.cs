using UnityEngine;

public class Catapult : Turret
{
    protected override void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), lockSpeed * Time.deltaTime);
    }

    protected override void InitializeProjectile(Projectile projectile, GameObject go)
    {
        lockOnPos = go.transform;
        projectile.target = lockOnPos;
    }
}
