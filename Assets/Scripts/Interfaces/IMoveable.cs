using UnityEngine;
using System.Collections;

public interface IMoveable
{
    bool MoveTo(Transform moveableObject, Transform targetObject, float velocity);
}
