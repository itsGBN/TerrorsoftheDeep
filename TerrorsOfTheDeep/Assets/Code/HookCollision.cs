using UnityEngine;

public class HookCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level1to2") 
        {
            if(GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState")==1)
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 2);
            }
            else
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 1);
            }
        }

        if (other.tag == "Level2to3") 
        {
            if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") == 2)
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 3);
            }
            else
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 2);
            }
        }
    }

}
