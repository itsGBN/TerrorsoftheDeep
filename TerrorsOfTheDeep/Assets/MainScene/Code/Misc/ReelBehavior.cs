using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class ReelBehavior : MonoBehaviour
{
    public Transform orb;
    public float radius;

    private Transform pivot;
    private float checkAngle;

    public string reelDirection;

    void Start()
    {
        pivot = orb.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }

    void OnMouseDrag()
    {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(orb.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;
        pivot.position = orb.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        if(checkAngle > angle && checkAngle - angle >= -50) { reelDirection = "Left";} 
        else if (checkAngle < angle && checkAngle - angle <= 50) { reelDirection = "Right"; }
        else { reelDirection = "None"; }
        checkAngle = angle;
    }

    private void OnMouseUp()
    {
       reelDirection = "None";
    }
}
