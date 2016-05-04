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
    public Sprite HardestLevel;
   

    // Use this for initialization
    void Start () {
        showPanels = GetComponent<ShowPanels>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!showPanels.endGameIsActive() && victory)
        {
            GameTime.text = "Total Game Time: " + (EndGameStats.endTime - EndGameStats.initTime).ToString("0.00");
            PauseCount.text = EndGameStats.totalPauseCount.ToString();
            TotalRestarts.text = EndGameStats.numRestarts.ToString();
            HardestRestarts.text = EndGameStats.maxRestarts.ToString();
            HardestLevel = EndGameStats.hardestLevelImage; //Jackson plz
            showPanels.showEndGamePanel();
        }
	
	}
}
