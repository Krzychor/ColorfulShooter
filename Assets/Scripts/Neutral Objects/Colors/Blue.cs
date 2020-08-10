using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : ObjectColor
{

    private static int colorChoice = 1;
    private static Color blueColor;

    // Use this for initialization
    void Start()
    {
        blueColor = Player.Single.color[colorChoice - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override Color GetColor()
    {
        return blueColor;
    }
}
