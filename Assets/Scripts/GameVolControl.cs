using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVolControl : MonoBehaviour
{
    public static float Vol;
    public static bool BackVol;
    public static bool ispreesssword;
    public static bool ispressed;

    static GameVolControl()
    {
        Vol = 1;
        BackVol = true;
        ispreesssword = false;
        ispressed = false;
    }
}
