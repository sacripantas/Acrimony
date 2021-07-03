using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    public static string playerName;
    public static int saveSlot;
    public static string[] minimaps = {
        "11111111111111111111111", //first room 23 panels
        "11111111111111111111111", //Second room 23 panels
    };
    public static int sceneToLoad = 0;
}
