using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Piece", menuName = "Scriptable/Pice Value")]
public class PieceValue : BoxValue
{
    public List<Vector2> stepPositions;

    public override bool Contact(BoxBehaviour from, BoxBehaviour to)
    {
        to.IdentifyBox();

        to.animator.SetTrigger("Rotate");
        if(from == to){
            InitKillers(to);
            return true;
        }
        return false;
    }

    public virtual void InitKillers(BoxBehaviour to)
    {
        var boardLength = LevelManager.Instance.boardLength;
        var boxContainer = LevelManager.Instance.boxContainer;
        var pieceTileCount = 0;

        for (int i = 0; i < stepPositions.Count; i++)
        {
            Vector2 pos = stepPositions[i] + to.address;

            if (pos.x < boardLength && pos.y < boardLength && pos.x >= 0 && pos.y >= 0)
            {
                BoxBehaviour newBox = boxContainer[(int)pos.x, (int)pos.y];
                pieceTileCount++;
                float delay = (newBox.address - to.address).magnitude / 10f;

                if (newBox.stats is ScoreValue)
                {
                    LevelManager.Instance.currentScore += ((ScoreValue)newBox.stats).score;
                }

                var killerObj = Instantiate(to.frontPanel.gameObject, to.transform);
                killerObj.transform.position = to.frontPanel.transform.position;
                killerObj.AddComponent<Killer>().Init(to, newBox);
            }
        }
        LevelManager.Instance.pieceTileCount = pieceTileCount;
    
    }
}