using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects;

    Dictionary<string, GameObject> products;

    private void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        products = new Dictionary<string, GameObject>();
        foreach (var p in gameObjects)
        {
            if (!products.ContainsKey(p.name))
            {
                products.Add(p.name, p);
            }
        }
        gameObjects.Clear();
    }

    public GameObject GetProduct(string name, Vector3 position)
    {
        GameObject toInstance;
        if(!products.TryGetValue(name, out toInstance))
        {
            Debug.LogWarning($"{name} no pertenece a este Factory");
            return null;
        }
        return Instantiate(toInstance, position, Quaternion.identity);
    }
}
