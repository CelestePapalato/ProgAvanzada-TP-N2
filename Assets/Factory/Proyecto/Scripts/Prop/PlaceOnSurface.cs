using UnityEngine;

public class PlaceOnSurface : MonoBehaviour
{
    [SerializeField] float heightMargin = .1f;
    [SerializeField] float raycastDistance;
    [SerializeField] LayerMask _floorLayerMask;

    void Start()
    {
        Place();
    }

    void Place()
    {
        RaycastHit hit;
        Vector3 position = transform.position;
        position.y += heightMargin;
        if (Physics.Raycast(position, Vector3.down, out hit, raycastDistance, _floorLayerMask))
        {
            Vector3 newPosition = hit.point;
            transform.position = newPosition;
            return;
        }
        else
        {
            Debug.Log("Placement failed");
            Destroy(gameObject);
        }
    }
}