using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : ObjectColor
{

    private static int colorChoice = 2;
    private static Color redColor;

    // Use this for initialization
    void Start()
    {
        redColor = Player.Single.color[colorChoice - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override Color GetColor()
    {
        return redColor;
    }
}
