using UnityEngine;

public class BaseUnit: MonoBehaviour
{
    public int UID  = -1;
    

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}
