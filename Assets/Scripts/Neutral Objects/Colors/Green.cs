using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : ObjectColor
{

    private static int colorChoice = 3;
    private static Color greenColor;

    // Use this for initialization
    void Start()
    {
        greenColor = Player.Single.color[colorChoice - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override Color GetColor()
    {
        return greenColor;
    }
}

