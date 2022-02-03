using UnityEngine;

public class Constraint : MonoBehaviour
{
    [SerializeField]
    private float desiredHeight = 1.6f;

    //Assign these variables in the inspector, or find them some other way (eg. in Start() )
    [SerializeField]
    private Transform steamCamera;

    [SerializeField]    
    private Transform cameraRig;

    public void SetReset()
    {
        if ((steamCamera != null) && (cameraRig != null))
        {
            /*ROTATION*/
            // Get current head heading in scene (y-only, to avoid tilting the floor)
            float offsetAngle = steamCamera.rotation.eulerAngles.y;
            float offsetX = steamCamera.transform.position.x;
            float offsetZ = steamCamera.transform.position.z;

            // Now rotate CameraRig in opposite direction to compensate
            cameraRig.Rotate(0f, -offsetAngle, 0f);

            /*POSITION*/
            // calculate how much to add or subtract from the height, to arrive at y 1.6
            float headHeight = steamCamera.transform.localPosition.y;
            float heightChange = desiredHeight - headHeight;
            float xChange = transform.position.x - offsetX;
            float zChange = transform.position.z - offsetZ;
            transform.position = new Vector3(xChange, heightChange, zChange);

            Debug.Log("Seat recentered!");
        }
        else
        {
            Debug.Log("Error: SteamVR objects not found!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetReset();
        }
    }
}