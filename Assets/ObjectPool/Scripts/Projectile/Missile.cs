using UnityEngine;

public class Missile : Projectile
{
    protected override void Update()
    {
        base.Update();

        Vector3 dir = target.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, Time.deltaTime * turnSpeed, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
