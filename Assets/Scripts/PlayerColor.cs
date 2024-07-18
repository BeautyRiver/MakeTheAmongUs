using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerColor
{
    Red, Blue, Green,
    Pink, Orange, Yellow,
    Black, White, Purple,
    Bronw, Cyan, Lime
}
public class PlayerColor
{
    // 색깔 지정
    private static List<Color> colors = new List<Color>()
    {
        new Color(1f, 0f, 0f), // Red
        new Color(0.1f, 0.1f, 0f), // Blue
        new Color(0f, 0.6f, 0f), // Green
        new Color(1f, 0.3f, 0.9f), // Pink
        new Color(1f, 0.4f, 0f), // Orange
        new Color(1f, 0.9f, 0.1f), // Yellow
        new Color(0.2f, 0.2f, 0.2f), // Black
        new Color(0.9f, 1f, 1f), // White
        new Color(0.6f, 0f, 0.6f), // Purple
        new Color(0.7f, 0.2f, 0f), // Brown
        new Color(0f, 1f, 1f), // Cyan
        new Color(0.1f, 0f, 0.1f) // Lime
    };

    // 색깔 가져오기
    public static Color GetColor(EPlayerColor playerColor)
    { 
        return colors[(int)playerColor]; 
    }
}
