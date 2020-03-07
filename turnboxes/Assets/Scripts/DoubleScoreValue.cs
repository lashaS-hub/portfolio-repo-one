using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreValue : PieceValue
{
    public override bool Contact(BoxBehaviour from, BoxBehaviour to)
    {
        return base.Contact(from, to);
    }

	public override void InitKillers(BoxBehaviour to)
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
                    LevelManager.Instance.currentScore += ((ScoreValue)newBox.stats).score * 2;
                }

                var killerObj = Instantiate(to.frontPanel.gameObject, to.transform);
                killerObj.transform.position = to.frontPanel.transform.position;
                killerObj.AddComponent<Killer>().Init(to, newBox);
            }
        }
        LevelManager.Instance.pieceTileCount = pieceTileCount;
    }
}
