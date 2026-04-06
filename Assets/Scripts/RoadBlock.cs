using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public bool fetch(float z)
    {
        bool result = false;

        if (z > transform.position.z + 100f)
        {
            result = true;
        }

        return result;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

}
