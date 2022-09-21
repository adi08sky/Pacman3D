using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator _doorAnim;

    private void OnTriggerEnter(Collider other)
    {
        _doorAnim.SetBool("isOpening", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _doorAnim.SetBool("isOpening", false);
    }
}
