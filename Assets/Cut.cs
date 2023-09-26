using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
    [SerializeField] private List<Sprite> cuts = new();
    List<SpriteRenderer> cutRenderers = new();
    void Start()
    {
        foreach (Transform child in transform)
            cutRenderers.Add(child.GetComponent<SpriteRenderer>());
        var chosenSprite = cuts[Random.Range(0, cuts.Count)];
        foreach (var r in cutRenderers)
            r.sprite = chosenSprite;
        //GetComponent<SpriteRenderer>().sprite = cuts[Random.Range(0, cuts.Count)];
    }

    void Update()
    {
        
    }
}
