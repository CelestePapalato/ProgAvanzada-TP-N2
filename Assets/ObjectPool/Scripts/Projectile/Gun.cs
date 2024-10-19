using UnityEngine;

public class Gun : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (!target) { return; }
        Vector3 dir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    protected override void Update()
    {
        base.Update();
        float singleSpeed = speed * Time.deltaTime;
        transform.Translate(transform.forward * singleSpeed * 2, Space.World);
    }
}
