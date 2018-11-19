
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void TakeShot(Vector3 shootDirection,float speed,ForceMode forceMode);
    GameObject  GetPulled(Transform puller,float pullSpeed,ForceMode forceMode);
    void Stop();
}
