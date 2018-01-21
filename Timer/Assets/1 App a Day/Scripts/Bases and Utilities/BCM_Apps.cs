using UnityEngine;

public class BCM_Apps : MonoBehaviour
{
    public static bool touchCheck()
    {
        if (Input.touchCount > 0)
            return true;
        return false;
    }
}
