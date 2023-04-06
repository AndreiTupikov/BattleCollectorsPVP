using Photon.Pun;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
    }
}
