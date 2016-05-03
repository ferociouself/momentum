using UnityEngine;
using UnityEngine.UI;

public class EndGameStats {
	public static int totalPauseCount;
	public static long initTime;
	public static long endTime;
	public static long totalPauseTime;

	public static int numRestarts;

	public static string hardestLevel;
	public static Image hardestLevelImage;
	public static int maxRestarts;

	public static void updateHardestLevel() {

	}

	public static void addToTotalPause(int pauses) {
		totalPauseCount += pauses;
	}

	public static void addToTotalPauseTime(long time) {
		totalPauseTime += time;
	}

	public static void addToNumRestarts(int restarts) {
		numRestarts += restarts;
	}
}
