using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (view.IsMine)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
