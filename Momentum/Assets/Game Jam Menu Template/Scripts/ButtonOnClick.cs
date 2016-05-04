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
    public Image[] myImages;

    private StartOptions startoptions;
    private ShowPanels showPanels;
    private bool victory;


    // Use this for initialization
    void Start () {

        mySprites = (Sprite[])Resources.LoadAll<Sprite>("worldsprites");
        showPanels = GetComponent<ShowPanels>();
        startoptions = GetComponent<StartOptions>();


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
            myImages[i].sprite = mySprites[i + ((currworld-1)*5)];
          

        if (Input.GetKeyUp("left"))
        {
            currworld--;
            if (currworld < 1)
                currworld = 5;
            
        
           
        }
        if (Input.GetKeyUp("right"))
        {
            currworld++;
            if (currworld > 5)
                currworld = 1;

        }
    }
}
