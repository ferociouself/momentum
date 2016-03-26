using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    GameObject greenGoal;
    GameObject greenBall;
    GameObject canvas;
    Text[] textValue;
    public EditModeScript EMS;
    float timer = 0;
    float timeToWait = 0.10f;
    bool checkingTime;
    bool timerDone;
    bool used;
    bool firstRun;



    // Use this for initialization
    void Start()
    {
        firstRun = true;
        greenBall = GameObject.Find("circleGreenNew");
        greenGoal = GameObject.Find("goalGreen");
        canvas = GameObject.Find("Canvas");
        textValue = canvas.GetComponentsInChildren<Text>();
        EMS = GetComponent<EditModeScript>();
        foreach (Text txt in textValue)
        {
            txt.enabled = false;
        }
        textValue[0].enabled = true;
        checkingTime = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (firstRun)
        {
            EMS.enabled = false;
            firstRun = false;
        }

        if (checkingTime)
        {
            timer += 0.001f;

            if (timer >= timeToWait)
            {
                timerDone = true;
                checkingTime = false;
                timer = 0;
            }
        }

        if (timerDone)
        {
            textValue[0].enabled = false;
            textValue[1].enabled = true;
            EMS.enabled = true;
            timerDone = false;
        }

        if (Input.GetButtonDown("Pause") && textValue[1].enabled == true && !used)
        {
            textValue[1].enabled = false;
            textValue[2].enabled = true;
            used = true;
        }

        if (Input.GetButtonDown("Pause") && textValue[2].enabled == true && !used)
        {
            textValue[2].enabled = false;
            textValue[3].enabled = true;
            used = true;
        }

        if (Input.GetMouseButtonUp(0) && textValue[3].enabled == true && !used)
        {
            textValue[3].enabled = false;
            textValue[4].enabled = true;
            used = true;
        }

        if (Input.GetMouseButtonUp(0) && textValue[4].enabled == true && !used)
        {
            textValue[4].enabled = false;
            textValue[5].enabled = true;
            used = true;
        }

        if (Input.GetMouseButtonUp(0) && textValue[5].enabled == true && !used)
        {
            textValue[5].enabled = false;
            textValue[6].enabled = true;
            used = true;
        }

//        if (greenBall.transform.position == greenGoal.transform.position)
//        {
//            textValue[6].enabled = false;
//            textValue[7].enabled = true;
//            used = true;
//        }

        used = false;

    }

}