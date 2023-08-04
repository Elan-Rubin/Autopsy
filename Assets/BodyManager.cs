using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    private static BodyManager instance;
    public static BodyManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    void Start()
    {
        List<Collider2D> colliders = transform.GetComponentsInChildren<Collider2D>().ToList();
        foreach (Collider2D collider in colliders)
        {
            foreach (Collider2D collider2 in colliders)
                Physics2D.IgnoreCollision(collider, collider2);
        }
    }

    void Update()
    {
        
    }
}
