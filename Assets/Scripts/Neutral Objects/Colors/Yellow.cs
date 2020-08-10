using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow : ObjectColor
{

    private static int colorChoice = 4;
    private static Color yellowColor;

    // Use this for initialization
    void Start()
    {
        yellowColor = Player.Single.color[colorChoice - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override Color GetColor()
    {
        return yellowColor;
    }
}
