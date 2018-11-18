
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void Shoot(Vector3 shootDirection,ForceMode forceMode);
    GameObject  Pull(Transform puller,float pullSpeed);
    void Stop();
}
