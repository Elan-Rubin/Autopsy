using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using System.Numerics;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    public static CameraManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    private Vector2 startPos;
    [SerializeField] private Vector2 bottomLeft, topRight;
    private Vector2 currentPos, targetPos;
    private float startZoom = 5;
    private float currentZoom = 5, targetZoom = 5;
    private float zoomMutliplier;
    private float posMultiplier;
    private Camera cam;
    private bool hitting;
    void Start()
    {
        startPos = transform.position;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!AutopsyManager.Instance.LimbSelected)
        {
            posMultiplier = 1f;
            zoomMutliplier = 1f;
            targetPos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            targetPos = new Vector2(Mathf.Clamp(targetPos.x, bottomLeft.x, topRight.x), Mathf.Clamp(targetPos.y, bottomLeft.y, topRight.y));
        }
        else
        {
            zoomMutliplier = 0.75f;
            posMultiplier = 5f;
        }

        if (!hitting)
        {
            currentZoom = Mathf.Lerp(cam.orthographicSize, targetZoom * zoomMutliplier, Time.deltaTime * 1f);
            cam.orthographicSize = currentZoom;
        }

        //System.Numerics.Vector2 t = VectorConvert(targetPos);
        //System.Numerics.Vector2.Clamp(t, VectorConvert(bottomLeft), VectorConvert(topRight));
        //targetPos = VectorConver2(t);
        //Debug.Log((Vector2.Distance(targetPos, startPos)));
        currentPos = UnityEngine.Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 1f * posMultiplier);
        transform.position = new UnityEngine.Vector3(currentPos.x, currentPos.y, -10);
    }

    //private System.Numerics.Vector2 VectorConvert(UnityEngine.Vector2 v) => new System.Numerics.Vector2(v.x, v.y);
    //private UnityEngine.Vector2 VectorConver2(System.Numerics.Vector2 v) => new UnityEngine.Vector2(v.X, v.Y);

    public void HitCamera()
    {
        hitting = true;
        var seq = DOTween.Sequence();
        seq.Append(cam.DOOrthoSize(currentZoom - 0.3f, 0.1f));
        seq.Append(cam.DOOrthoSize(currentZoom, 0.1f)).OnComplete(() => hitting = false);
    }
}
