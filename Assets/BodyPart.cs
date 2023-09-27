using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private BodyPartType type;
    public BodyPartType Type { get { return type; } }
    [SerializeField] private bool reversed;
    [SerializeField] private bool still;
    bool selected;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed = 200;
    private Camera cam;
    private int biteCounter;
    public int BiteCounter { get { return biteCounter; } }

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        BodyManager.Instance.BodyParts.Add(this);
    }

    void Update()
    {
        if (still) return;
        if (selected)
        {
            Vector3 playerPos = (Vector3)cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = playerPos - transform.position;
            float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
            if(reversed) rotationZ = -rotationZ;
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.deltaTime));

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                selected = false;
                AutopsyManager.Instance.LimbSelected = selected;

            }
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            selected = true;
            AutopsyManager.Instance.LimbSelected = selected;
            //CameraManager.Instance.HitCamera();

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!AutopsyManager.Instance.LimbSelected) return;
            AutopsyManager.Instance.Bite(type, gameObject);
            AutopsyManager.Instance.PlaceBlood((Vector3)cam.ScreenToWorldPoint(Input.mousePosition));
            biteCounter++;
        }
    }
}
