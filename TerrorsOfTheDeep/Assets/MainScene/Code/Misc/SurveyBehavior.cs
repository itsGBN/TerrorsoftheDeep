using System.Net.Sockets;
using UnityEngine;

namespace JustFish
{
    public class SurveyBehavior : MonoBehaviour
    {

        private void OnMouseDrag()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 5;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
        }

        private void OnMouseDown()
        {
            print("Hello");
        }
    }
}

