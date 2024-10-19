using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private GameObject currentTarget;

    private Turret turret;

    public float attackDist = 10.0f;

    private void Start()
    {
        turret = GetComponentInChildren<Turret>();
        InvokeRepeating("CheckForTarget", 0, 0.5f);
    }

    private void OnDisable()
    {

        if (turret) { turret.Target = null; }
    }

    private void Update()
    {
        if (currentTarget)
        {
            float currentTargetDist = Vector3.Distance(turret.transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
                turret.Target = null;
            }
        }
    }

    private void CheckForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Player")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    turret.Target = currentTarget;
                    distAway = dist;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }
}
