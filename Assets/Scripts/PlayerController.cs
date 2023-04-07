using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float health;
    public float shootDamage;
    public Vector3 direction;
    public bool isMooving;
    public bool isShooting;
    public GameObject bullet;
    public Transform playerInfo;
    public Transform gunBarrel;
    public GameManager gameManager;
    private Image healthLevel;
    private PhotonView view;
    private float borderX = 11;
    private float borderY = 5;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            healthLevel = playerInfo.gameObject.GetComponentInChildren<Image>();
        }
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
            TurnPlayer();
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (Math.Abs(transform.position.x) > borderX) transform.position = new Vector3(transform.position.x > 0 ? borderX : -borderX, transform.position.y, transform.position.z);
            if (Math.Abs(transform.position.y) > borderY) transform.position = new Vector3(transform.position.x, transform.position.y > 0 ? borderY : -borderY, transform.position.z);
            playerInfo.position = transform.position + new Vector3(0, 0.8f, 0);
            GetComponent<Animator>().SetBool("mooving", true);
        }
        else GetComponent<Animator>().SetBool("mooving", false);
    }

    private void TurnPlayer()
    {
        float newAngle = Vector2.SignedAngle(Vector3.up, direction);
        float oldAngle = transform.rotation.eulerAngles.z;
        float rotateAngle = newAngle - oldAngle;
        if (rotateAngle > 180) rotateAngle -= 360;
        else if (rotateAngle < -180) rotateAngle = 360 + newAngle - oldAngle;
        transform.Rotate(rotateAngle * Vector3.forward);
    }

    private void Shoot()
    {
        isShooting = false;
        PhotonNetwork.Instantiate(bullet.name, gunBarrel.position, gunBarrel.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health -= 10;
            healthLevel.fillAmount -= 0.1f;
            if (healthLevel.fillAmount < 0.7) healthLevel.color = Color.yellow;
            if (healthLevel.fillAmount < 0.4) healthLevel.color = Color.red;
            gameManager.SendPlayerHealth(playerInfo.GetComponent<PhotonView>().ViewID, healthLevel.fillAmount);
        }
    }
}
