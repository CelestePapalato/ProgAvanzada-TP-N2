using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            return;
        }
        factory.GetProduct(propName, position);
    }

    private bool GetMousePosition(out Vector3 worldPosition)
    {
        worldPosition = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer.value))
        {
            worldPosition = hit.point;
            return true;
        }
        return false;
    }
}
