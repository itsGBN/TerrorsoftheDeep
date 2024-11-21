using UnityEngine;

public class ReelController : MonoBehaviour
{
    LineRenderer lineRenderer;
    Transform[] linePoint;

    HookController hookController;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        hookController = gameObject.transform.parent.GetComponent<HookController>();
    }

    public  void SetReel(Transform[] linePoints)
    {
        lineRenderer.positionCount = linePoints.Length;
        this.linePoint = linePoints;
    }

    public void ResetReel()
    {
        lineRenderer.positionCount = 0;
    }

    private void FixedUpdate()
    {
        if(hookController.hookState != "notFishing")
        {
            for (int i = 0; i < linePoint.Length; i++)
            {
                lineRenderer.SetPosition(i, linePoint[i].position);
            }
        }
    }
}
