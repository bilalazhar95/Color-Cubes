using UnityEngine;

public interface IMoveable
{
    void Move(Vector3 moveDirection, float speed, ForceMode forceMode);
}
