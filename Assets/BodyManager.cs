using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    private static BodyManager instance;
    public static BodyManager Instance { get { return instance; } }
    
    private List<BodyPart> bodyParts = new List<BodyPart>();
    public List<BodyPart> BodyParts { get {  return bodyParts; } }

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

    public void ResetBody()
    {
        foreach(var part in bodyParts)
        {
            while (part.transform.GetChild(4) != null)
                Destroy(part.transform.GetChild(4).gameObject);
        }
    }

    public BodyPart GetRandomBodyPart()
    {
        return bodyParts[Random.Range(0, bodyParts.Count)];
    }
    public BodyPart GetBodyPart(BodyPartType type)
    {
        Debug.Log(type);
        return bodyParts.Where(b => b.Type.Equals(type)).ToList()[0];
    }
}
