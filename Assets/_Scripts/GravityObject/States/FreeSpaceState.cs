using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSpaceState : GravityObjectState
{
    public FreeSpaceState(Rigidbody2D rb) : base(rb)
    {
        acceleration = 0;
    }
}
