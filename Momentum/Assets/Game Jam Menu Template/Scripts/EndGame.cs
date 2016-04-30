using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {
    private ShowPanels showPanels;
    private bool victory;
    public Text GameTime;
    public Text PauseCount;
    public Text TotalRestarts;
    public Text HardestRestarts;
    public Image HardestLevel;

    // Use this for initialization
    void Start () {
        showPanels = GetComponent<ShowPanels>();

    }
	
	// Update is called once per frame
	void Update () {
        if (victory && !showPanels.endGameIsActive())
        {
            GameTime.text = "Total Game Time: " + (EndGameStats.endTime - EndGameStats.initTime).ToString("0.00");
            PauseCount.text = EndGameStats.totalPauseCount.ToString();
            TotalRestarts.text = EndGameStats.numRestarts.ToString();
            HardestRestarts.text = EndGameStats.maxRestarts.ToString();
            HardestLevel = HardestLevel; //Jackson plz
            showPanels.showEndGamePanel();
        }
	
	}
}
