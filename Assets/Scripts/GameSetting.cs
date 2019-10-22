using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSetting
{
    public static int numOfPlayerInput = 5;
    public enum PlayerInput { Up, Down, Left, Right, Interact }
    // Key Setting
    public static KeyCode[,] keyList = { { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Slash}, 
                                  { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Z }, 
                                  { KeyCode.Delete, KeyCode.Delete, KeyCode.Delete, KeyCode.Delete, KeyCode.Delete },
                                  { KeyCode.Delete, KeyCode.Delete, KeyCode.Delete, KeyCode.Delete, KeyCode.Delete }};



}
