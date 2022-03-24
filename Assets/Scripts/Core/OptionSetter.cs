using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OptionType
{
    GameMode
}

public class OptionSetter : MonoBehaviour
{
    public OptionType type;

    public void SetOption(int option)
    {
        switch (type)
        {
            case OptionType.GameMode:
                Options.gameMode = (GameMode)option;
                break;
        }
    }
}
