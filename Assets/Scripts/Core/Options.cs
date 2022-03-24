using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Slow,
    Fast
}

public class Options
{
    public static GameMode gameMode;
    public static string difficultyModeName = "Easy";
}
