using UnityEngine;

public class SurveyController : MonoBehaviour
{
    [SerializeField] HookController hookController;
    [SerializeField] GameObject survey1, survey2, survey3, nextPageButton;
    GameObject currentSurvey;
    int surveyTarget = 0;
    bool canMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSurvey = survey1;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            currentSurvey.transform.position = Vector3.MoveTowards(currentSurvey.transform.position, new Vector3(surveyTarget, currentSurvey.transform.position.y, currentSurvey.transform.position.z), 60f * Time.deltaTime);
            if(currentSurvey.transform.position == new Vector3(surveyTarget, currentSurvey.transform.position.x, currentSurvey.transform.position.y))
            {
                canMove = false;
            }
        }
    }

    public void NextPage()
    {
        surveyTarget -= 121;
        canMove = true;
        nextPageButton.SetActive(false);
    }

    public void TurnPage()
    {
        nextPageButton.SetActive(true);
    }

    public void SurveyDone()
    {
        currentSurvey.SetActive(false);
        currentSurvey.transform.parent.transform.GetChild(0).gameObject.SetActive(false);
        currentSurvey.transform.parent.transform.GetChild(1).gameObject.SetActive(false);
        AudioManager.instance.MusicTwo();
        AudioManager.instance.AmbieneceOne();
        hookController.canLevel2 = true;
    }

    public void OpenSurvey1()
    {
        currentSurvey = survey1;
        currentSurvey.SetActive(true);
        currentSurvey.transform.parent.transform.GetChild(0).gameObject.SetActive(true);
        currentSurvey.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
    }
}

