using UnityEngine;
using System.Collections;

public class MovePlayerCard : IMoveable
{
    public bool MoveTo(Transform moveableObject, Transform targetObject, float velocity)
    {
        float distance = Vector3.Distance(moveableObject.position, targetObject.position);
        if (distance > 0.01f)
        {
            moveableObject.position = Vector3.Lerp(moveableObject.position, targetObject.position, Time.deltaTime * velocity);
            moveableObject.localRotation = Quaternion.Lerp(moveableObject.localRotation, targetObject.localRotation, Time.deltaTime * velocity * 1.2f);
            return true;
        }
        return false;
    }
}

//public class MovePlayer2Card : IMoveable
//{
//    public bool MoveTo(Transform moveableObject, Transform targetObject, float velocity)
//    {
//        float distance = Vector3.Distance(moveableObject.position, targetObject.position);
//        if (distance > 0.01f)
//        {
//            moveableObject.position = Vector3.Lerp(moveableObject.position, targetObject.position, Time.deltaTime * velocity);
//            moveableObject.localRotation = Quaternion.Lerp(moveableObject.localRotation, targetObject.localRotation, Time.deltaTime * velocity * 1.2f);
//            return true;
//        }
//        return false;
//    }
//}
