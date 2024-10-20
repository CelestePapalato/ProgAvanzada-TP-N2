using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(TurretAI))]
public abstract class Turret : MonoBehaviour
{
    protected GameObject currentTarget;
    public GameObject Target
    {
        get => currentTarget;
        set
        {
            if(currentTarget == null)
            {
                InvokeRepeating(nameof(ShootTrigger), 0f, shootCoolDown);
            }
            else
            {
                if (!value)
                {
                    CancelInvoke(nameof(ShootTrigger));
                }
            }
            currentTarget = value;
        }
    }
    public Transform turreyHead;

    public float attackDist = 10.0f;
    public float attackDamage;
    public float shootCoolDown;
    protected float timer;
    public float lockSpeed;

    //public Quaternion randomRot;
    public Vector3 randomRot;
    public Animator animator;

    public Transform muzzleMain;
    public string muzzleEff_tag = "Eff_Muzzle";
    public string bullet_tag;

    protected Transform lockOnPos;

    protected virtual void Start()
    {
        //InvokeRepeating("CheckForTarget", 0, 0.5f);
        animator = GetComponentInChildren<Animator>();
        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }

    protected virtual void Update()
    {
        if (currentTarget)
        {
            FollowTarget();
        }
        else
        {
            IdleRotate();
        }
    }

    private bool IsTargetInRange()
    {
        if (!currentTarget) { return false; }
        float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
        return currentTargetDist <= attackDist;
    }

    protected abstract void FollowTarget();
    protected void ShootTrigger()
    {
        if(IsTargetInRange())
        {
            Shoot(currentTarget);
            //Debug.Log("We shoot some stuff!");
        }
    }

    protected Vector3 CalculateVelocity(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void IdleRotate()
    {
        bool refreshRandom = false;

        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), lockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public virtual void Shoot(GameObject go)
    {
        animator?.SetTrigger("Fire");
        InstantiateMuzzle();
        GameObject bullet = InstantiateBullet();
        Projectile projectile = bullet.GetComponent<Projectile>();
        InitializeProjectile(projectile, go);
        bullet.SetActive(true);
    }

    protected void InstantiateMuzzle()
    {
        GameObject muzzleEff = ObjectPool.Instance.GetObject(muzzleEff_tag);
        muzzleEff.transform.position = muzzleMain.transform.position;
        muzzleEff.transform.rotation = muzzleMain.rotation;
        muzzleEff.SetActive(true);
    }

    protected abstract void InitializeProjectile(Projectile projectile, GameObject go);

    protected virtual GameObject InstantiateBullet()
    {
        GameObject missleGo = ObjectPool.Instance.GetObject(bullet_tag);
        missleGo.transform.position = muzzleMain.transform.position;
        missleGo.transform.rotation = muzzleMain.rotation;
        missleGo.SetActive(true);
        return missleGo;
    }
}
