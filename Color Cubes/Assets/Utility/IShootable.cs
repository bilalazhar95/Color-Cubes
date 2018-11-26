
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void TakeShot(Vector3 shootDirection, float speed, ForceMode forceMode);
}
