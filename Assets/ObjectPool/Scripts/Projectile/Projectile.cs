using UnityEngine;

public class Projectile : MonoBehaviour {

    public Transform target;

    public float speed = 1;
    public float turnSpeed = 1;

    public float knockBack = 0.1f;
    public float boomTimer = 1;

    private float activeBoomTimer;

    TrailRenderer trailRenderer;

    protected virtual void OnEnable()
    {
        if (!trailRenderer)
        {
            trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        if (!target) { gameObject.SetActive(false); return; }
        trailRenderer?.Clear();
        activeBoomTimer = boomTimer;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            Explosion();
            return;
        }

        if (transform.position.y < -0.2F)
        {
            Explosion();
            return;
        }

        activeBoomTimer -= Time.deltaTime;
        if (activeBoomTimer < 0)
        {
            Explosion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Vector3 dir = other.transform.position - transform.position;
            //Vector3 knockBackPos = other.transform.position * (-dir.normalized * knockBack);
            Vector3 knockBackPos = other.transform.position + (dir.normalized * knockBack);
            knockBackPos.y = 1;
            other.transform.position = knockBackPos;
            Explosion();
        }
    }

    public void Explosion()
    {
        GameObject explosion = ObjectPool.Instance.GetObject("Eff_Explosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        explosion.SetActive(true);
        gameObject.SetActive(false);
    }
}
