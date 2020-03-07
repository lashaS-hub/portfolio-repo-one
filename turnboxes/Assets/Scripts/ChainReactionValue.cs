using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Chain", menuName = "Scriptable/Chain Value")]
public class ChainReactionValue : PieceValue
{
    public override bool Contact(BoxBehaviour from, BoxBehaviour to)
    {
        if(from.lastStats == to.stats)
        {
            to.animator.SetTrigger("Shake");
            return false;
        }
        return base.Contact(from, to);
    }

    public override void Init(BoxBehaviour box)
    {
        // Debug.LogWarning("Enque");
        LevelManager.Instance.queue.Enqueue(new System.Action(()=>{
            InitKillers(box);
            Contact(box, box);
        }));



    }

    public void QueFunc(BoxBehaviour box)
    {
        InitKillers(box);
        Contact(box, box);
    }
}