using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    
    void LateUpdate()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }

        transform.LookAt(transform.position + cam.rotation * Vector3.forward);

    }
}
