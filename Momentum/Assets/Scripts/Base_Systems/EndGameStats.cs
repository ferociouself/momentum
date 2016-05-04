using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public static class EndGameStats {
	public static int totalPauseCount = 0;
	public static long initTime = 0;
	public static long endTime = 0;

	public static Stopwatch stopwatch = new Stopwatch();
	public static double totalPauseTime = 0.0;

	public static int numRestarts = 0;

	public static int curNumRestarts = 0;

	public static string hardestLevel = "";
	public static Sprite hardestLevelImage = null;
	public static int maxRestarts = -1;

	public static void beginGame() {
		stopwatch.Start();
	}

	public static void updateHardestLevel(string sceneName) {
		if (!sceneName.Contains("_")){
			return;
		}
		hardestLevel = sceneName;
		string[] nameSplit = sceneName.Split('_');
		string nameBegin = nameSplit[0];
		string nameEnd = nameSplit[1];
		int num1 = int.Parse(nameBegin.Substring(nameBegin.Length - 1));
		int num2 = int.Parse(nameEnd);
		hardestLevelImage = (Sprite)Resources.Load<Sprite>("worldsprites/world" + num1 + "_" + num2);
	}

	public static void endLevel(string sceneName) {
		numRestarts += curNumRestarts;
		UnityEngine.Debug.Log("curNumRestarts: " + curNumRestarts);
		UnityEngine.Debug.Log("numRestarts: " + numRestarts);
		UnityEngine.Debug.Log("totalPauseCount: " + totalPauseCount);
		UnityEngine.Debug.Log("totalPauseTime: " + totalPauseTime);
		if (curNumRestarts > maxRestarts) {
			maxRestarts = curNumRestarts;
			updateHardestLevel(sceneName);
		}
		curNumRestarts = 0;
	}

	public static void finalLevel() {
		stopwatch.Stop();
	}

	public static void addToTotalPause(int pauses) {
		totalPauseCount += pauses;
	}

	public static void addToTotalPauseTime(TimeSpan time) {
		totalPauseTime += time.TotalMilliseconds;
	}

	public static void restarted() {
		curNumRestarts++;
	}

	public static TimeSpan getTotalTime() {
		return stopwatch.Elapsed;
	}
}
