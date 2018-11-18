
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void Shoot(Vector3 shootDirection,ForceMode forceMode);
    GameObject  Pull(Transform pullDestination,float pullSpeed);
    void Stop();
}
