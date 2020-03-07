using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBreakerValue : PieceValue {
    public override bool Contact(BoxBehaviour from, BoxBehaviour to)
    {
        return base.Contact(from, to);
    }
}
