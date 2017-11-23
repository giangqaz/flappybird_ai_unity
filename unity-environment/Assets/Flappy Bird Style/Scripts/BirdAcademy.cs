using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAcademy : Academy
{

    public override void AcademyReset()
    {
        Bird.Instance.ResetBird();
        GameControl.instance.ResetGame();
    }
    public override void AcademyStep()
    {
        
    }
}
