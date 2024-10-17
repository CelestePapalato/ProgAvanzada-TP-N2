using UnityEngine;

public class Dual : Turret
{
    public Transform muzzleSub;
    bool shootLeft;

    protected override void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), lockSpeed * Time.deltaTime);
    }

    protected override GameObject InstantiateBullet()
    {
        GameObject missleGo = ObjectPool.Instance.GetObject(bullet_tag);

        if (shootLeft)
        {
            missleGo.transform.position = muzzleMain.transform.position;
            missleGo.transform.rotation = muzzleMain.rotation;
        }
        else
        {
            missleGo.transform.position = muzzleSub.transform.position;
            missleGo.transform.rotation = muzzleSub.rotation;
        }

        shootLeft = !shootLeft;

        return missleGo;
    }

    protected override void InitializeProjectile(Projectile projectile, GameObject go)
    {
        projectile.target = go.transform;
    }
}
