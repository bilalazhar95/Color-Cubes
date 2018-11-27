using UnityEngine;

public interface IMoveable
{
    void Move(Vector3 moveDirection, float speed, ForceMode forceMode);
    void Rotate(Vector3 torque, float magnitude, ForceMode forcemode);
}
