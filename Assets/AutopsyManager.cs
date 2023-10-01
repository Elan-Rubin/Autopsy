using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopsyManager : MonoBehaviour
{
    private static AutopsyManager instance;
    public static AutopsyManager Instance { get { return instance; } }
    public bool LimbSelected;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    [SerializeField] private GameObject bitePrefab, cutPrefab, bloodPreab;
    void Start()
    {

    }

    void Update()
    {

    }

    public void Bite(BodyPartType type, GameObject go)
    {
        var newBite = Instantiate(bitePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).transform;
        newBite.name = $"{type}Bite{BodyManager.Instance.GetBodyPart(type).BiteCounter}";
        newBite.position = new Vector3(newBite.position.x, newBite.position.y, 0);
        newBite.parent = go.transform;
        var sm1 = newBite.GetComponent<SpriteMask>();
        var sm2 = newBite.GetChild(0).GetComponent<SpriteMask>();
        var sm3 = newBite.GetChild(1).GetComponent<SpriteMask>();

        var index = (int)type;
        sm1.frontSortingLayerID = SortingLayer.NameToID(type + "Overskin");
        sm1.backSortingLayerID = SortingLayer.NameToID(type + "Skin");
        sm2.frontSortingLayerID = SortingLayer.NameToID(type + "Skin");
        sm2.backSortingLayerID = SortingLayer.NameToID(type + "Underskin");
        sm3.frontSortingLayerID = SortingLayer.NameToID(type + "Underskin");
        sm3.backSortingLayerID = SortingLayer.NameToID(type + "Muscle");

    }

    public void ResetEverything()
    {
        BodyManager.Instance.ResetBody();
    }

    public void PlaceBlood(Vector2 position)
    {
        Instantiate(bloodPreab, position, Quaternion.identity);
    }
}
public enum BodyPartType
{
    Head = 5,
    Chest = 11,
    Larm = 17,
    Lhand = 23,
    Rarm = 29,
    Rhand = 35,
    Lthigh = 41,
    Lcalf = 47,
    Lfoot = 53,
    Rthigh = 59,
    Rcalf = 65,
    Rfoot = 71,
}