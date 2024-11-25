using UnityEngine;

public class HookCollision : MonoBehaviour
{

    public GameObject goUp, goDown;
    [SerializeField] HookController hookController;

    private void Start()
    {
        goDown.SetActive(false);
        goUp.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level1to2" && hookController.canLevel2) 
        {
            if(GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState")==1)
            {
                goDown.SetActive(true);
            }
            else
            {
                goUp.SetActive(true);
            }
        }

        if (other.tag == "Level2to3") 
        {
            if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") == 2)
            {
                goDown.SetActive(true);
            }
            else
            {
                goUp.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Level1to2" && hookController.canLevel2)
        {
            if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") == 1 && Input.GetMouseButtonDown(0))
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 2);
                goDown.SetActive(false);
            }
            else if(GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") != 1 && Input.GetMouseButtonDown(0))
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 1);
                goUp.SetActive(false);
            }
        }

        if (other.tag == "Level2to3")
        {
            if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") == 2 && Input.GetMouseButtonDown(0))
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 3);
                goDown.SetActive(false);
            }
            else if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") != 2 && Input.GetMouseButtonDown(0))
            {
                GameObject.Find("State-Driven Camera").GetComponent<Animator>().SetInteger("camState", 2);
                goUp.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Level1to2" && hookController.canLevel2)
        {
            if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") == 1)
            {
                goDown.SetActive(false);
            }
            else
            {
                goUp.SetActive(false);
            }
        }

        if (other.tag == "Level2to3")
        {
            if (GameObject.Find("State-Driven Camera").GetComponent<Animator>().GetInteger("camState") == 2)
            {
                goDown.SetActive(false);
            }
            else
            {
                goUp.SetActive(false);
            }
        }
    }
}
