using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Score", menuName = "Scriptable/Score Value")]
public class ScoreValue : BoxValue
{
    public int score = 0;

    public override bool Contact(BoxBehaviour from, BoxBehaviour to)
    {
        if (from == to)
        {
            to.animator.SetTrigger("Shake");
            to.source.PlayOneShot(blockSound);

            return false;
        }
        if(score > 0){
            to.source.PlayOneShot(triggerSound);
            to.particle.Play();
            UiManager.Instance.CreateCoin(new Vector3(to.transform.position.x, to.transform.position.y, to.transform.position.z - 5));
        }
        to.IdentifyBox();
        to.animator.SetTrigger("Rotate");
        return true;
    }
}