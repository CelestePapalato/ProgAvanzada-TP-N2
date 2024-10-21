using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Factory))]
public class PropInstancer : MonoBehaviour
{
    [SerializeField]
    LayerMask floorLayer;

    [SerializeField]
    private string TreeName = "Tree";
    [SerializeField]
    private string RockName = "Rock";
    [SerializeField]
    private string ShrubName = "Shrub";

    private string propName = "";
    private Factory factory;

    private void Start()
    {
        Debug.Log(floorLayer.value);
        factory = GetComponent<Factory>();
    }

    public void InstanceTrees()
    {
        propName = TreeName;
    }

    public void InstanceRocks()
    {
        propName = RockName;
    }

    public void InstanceShrubs()
    {
        propName = ShrubName;
    }

    private void Update()
    {
        if (propName == "")
        {
            return;
        }
        Vector2 mousePosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        Debug.DrawRay(ray.origin, ray.direction * 5000, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            TryInstance();
        }
    }

    private void TryInstance()
    {
        Vector3 position;
        if(!GetMousePosition(out position))
        {
            Debug.Log("No floor detected");
            return;
        }
        factory.GetProduct(propName, position);
    }

    public bool IsInLayerMask(GameObject obj, LayerMask mask) => (mask.value & (1 << obj.layer)) != 0;

    private bool GetMousePosition(out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;
        Vector2 mousePosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(IsInLayerMask(hit.collider.gameObject, floorLayer))
            {
                worldPosition = hit.point;
                return true;
            }
        }
        return false;
    }
}
