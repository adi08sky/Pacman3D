using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [HideInInspector] public Transform playerCamera;
    [SerializeField] public Transform portal;
    [SerializeField] public Transform otherPortal;
    private float _myAngle;

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        PortalCameraController();
    }

    public void SetmyAngle(float angle)
    {
        _myAngle = angle;
    }

    void PortalCameraController()
    {
        Vector3 playerOffsetFromportal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromportal;

        float angularDifferenceBetweenportalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        if (_myAngle == 90 || _myAngle == 270)
        {
            angularDifferenceBetweenportalRotations -= 90;
        }

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenportalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;

        if (_myAngle == 90 || _myAngle == 270)
        {
            newCameraDirection = new Vector3(newCameraDirection.z * -1, newCameraDirection.y, newCameraDirection.x);

            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
        else
        {
            newCameraDirection = new Vector3(newCameraDirection.x * -1, newCameraDirection.y, newCameraDirection.z * -1);

            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
    }
}
