using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public bool isMooving;
    public bool isShooting;
    public GameObject bullet;
    public Transform gunBarrel;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            MovePlayer();
            if (isShooting) Shoot();
        }
    }

    private void MovePlayer()
    {
        if (isMooving)
        {
            float newAngle = Vector2.SignedAngle(Vector3.up, direction);
            float oldAngle = transform.rotation.eulerAngles.z;
            float rotateAngle = newAngle - oldAngle;
            if (rotateAngle > 180) rotateAngle -= 360;
            else if (rotateAngle < -180) rotateAngle = 360 + newAngle - oldAngle;
            transform.Rotate(rotateAngle * Vector3.forward);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            GetComponent<Animator>().SetFloat("speed", 1);
        }
        else GetComponent<Animator>().SetFloat("speed", 0);
    }

    private void Shoot()
    {
        isShooting = false;
        PhotonNetwork.Instantiate(bullet.name, gunBarrel.position, gunBarrel.rotation);
    }
}
