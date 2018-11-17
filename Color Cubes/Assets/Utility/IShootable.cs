
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void TakeShot(float blastForce, Vector3 blastPosition,float blastRadius,ForceMode forceMode);
}
