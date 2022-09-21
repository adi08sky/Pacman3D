using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    void FixedUpdate()
    {
        Teleportation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

    void Teleportation()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = new Vector3(reciever.position.x + positionOffset.x, player.position.y, reciever.position.z + positionOffset.z);
                playerIsOverlapping = false;
            }
        }
    }
}
