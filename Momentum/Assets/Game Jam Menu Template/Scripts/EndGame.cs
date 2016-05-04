using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    private ShowPanels showPanels;
    public static bool victory;
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
        if (!showPanels.endGameIsActive() && victory)
        {
            GameTime.text = "Total Game Time: " + EndGameStats.getTotalTime().Hours + ":" + EndGameStats.getTotalTime().Minutes + ":" + EndGameStats.getTotalTime().Seconds;
            PauseCount.text = "Total Pauses: " + EndGameStats.totalPauseCount.ToString();
            TotalRestarts.text = "Total Restarts: " + EndGameStats.numRestarts.ToString();
            HardestRestarts.text = "Max Restarts: " +  EndGameStats.maxRestarts.ToString();
            HardestLevel.sprite = EndGameStats.hardestLevelImage; //Jackson plz
            showPanels.showEndGamePanel();
            victory = false;
        }
	
	}
}
