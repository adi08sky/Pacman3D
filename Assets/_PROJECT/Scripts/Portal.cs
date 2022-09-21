using UnityEngine;

public class Portal : MonoBehaviour
{ 
    [SerializeField] private Camera _myCamera;
    [SerializeField] private Transform _myRenderPlane;
    [SerializeField] private Transform _myCollidPlane;
    [SerializeField] public Portal _otherPortal;

    private GameObject _player;
    private PortalCamera _portalCamera;
    private PortalTeleport portalTeleport;

    [SerializeField] private Material material;
    float _myAngle;

    private void Awake()
    {
        _portalCamera = _myCamera.GetComponent<PortalCamera>();
        portalTeleport = _myCollidPlane.gameObject.GetComponent<PortalTeleport>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _portalCamera.playerCamera = _player.gameObject.transform.GetChild(0);
        _portalCamera.otherPortal = _otherPortal.transform;
        _portalCamera.portal = this.transform;

        portalTeleport.player = _player.transform;
        portalTeleport.reciever = _otherPortal.transform;

        _myRenderPlane.gameObject.GetComponent<Renderer>().material = Instantiate(material);

        if (_myCamera.targetTexture != null)
        {
            _myCamera.targetTexture.Release();
        }

        _myCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        _myAngle = transform.localEulerAngles.y % 360;

        _portalCamera.SetmyAngle(_myAngle);
    }
    private void Start()
    {
        _myRenderPlane.gameObject.GetComponent<Renderer>().material.mainTexture =
        _otherPortal._myCamera.targetTexture;
        CheckAngle();
    }
    void CheckAngle()
    {
        if (Mathf.Abs(_otherPortal.Return_myAngle() - Return_myAngle()) != 180)
        {
            Debug.LogWarning("Portale nie s¹ odpowiednio ustawione: " + gameObject.name);
            Debug.Log("Angle: " + (_otherPortal.Return_myAngle() - Return_myAngle()));
        }
    }
    public float Return_myAngle()
    {
        return _myAngle;
    }
}
