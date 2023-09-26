using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
    [SerializeField] private List<Sprite> cuts = new();
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = cuts[Random.Range(0, cuts.Count)];
    }

    void Update()
    {
        
    }
}
