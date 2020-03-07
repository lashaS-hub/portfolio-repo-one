using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable/Obstacle Value")]
public class ObstacleValue : BoxValue
{
    public override bool Contact(BoxBehaviour from, BoxBehaviour to)
    {
        if(from.lastStats is ChainBreakerValue)
        {
            to.animator.SetTrigger("Rotate");
            to.IdentifyBox();
        }else{
            to.animator.SetTrigger("Shake");
            to.source.PlayOneShot(blockSound);
            to.FinishAnimation();
        }

        if(from == to){
            return false;
        }
        return true;
    }
}