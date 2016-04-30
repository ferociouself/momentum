using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour {
    int sceneToEnable = 0;
    public GameObject UI;
    private Sprite[] mySprites;
    public int currworld;
    public Text worldtext;
    public static Image[] myImages;

    private StartOptions startoptions;
    private ShowPanels showPanels;

    // Use this for initialization
    void Start () {
        mySprites = (Sprite[])Resources.LoadAll<Sprite>("worldsprites");
        showPanels = GetComponent<ShowPanels>();
        startoptions = GetComponent<StartOptions>();
        //worldtext = GameObject.Find("WorldSelectTitle").GetComponent<Text>();
        /* myImages = new Image[5];
        for (int i = 0; i < 5; i++)
        {
            myImages[i] = GameObject.Find("world" + (1+i)).GetComponent<Image>();
        } */



    }

    public void onClick(int scene)
    {
        startoptions.sceneToStart = scene + (currworld-1)*5;
        startoptions.StartButtonClicked();
        showPanels.HideWorldsPanel();


    }

    // Update is called once per frame
    void Update () {
        worldtext.text = "World " + currworld;
        for (int i = 0; i < 5; i ++)
        {
           // Debug.Log(i + ((currworld - 1) * 5));
            //Debug.Log(myImages[i]);
            myImages[i].sprite = mySprites[i + ((currworld-1)*5)];
        }

        if (Input.GetKeyUp("left"))
        {
            currworld--;
            if (currworld < 1)
                currworld = 2;

           
        }
        if (Input.GetKeyUp("right"))
        {
            currworld++;
            if (currworld > 2)
                currworld = 1;

        }
    }
}
