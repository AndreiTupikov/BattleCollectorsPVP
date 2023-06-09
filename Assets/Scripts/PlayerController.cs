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
    public Text scoreText;
    private Image healthLevel;
    private PhotonView view;
    public int score = 0;
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
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            health -= 10;
            healthLevel.fillAmount -= 0.1f;
            if (health < 70) healthLevel.color = Color.yellow;
            if (health < 40) healthLevel.color = Color.red;
            if (health <= 0) Defeat();
            gameManager.SendPlayerHealth(playerInfo.GetComponent<PhotonView>().ViewID, healthLevel.fillAmount);
        }
        else if (other.CompareTag("Money"))
        {
            PhotonNetwork.Destroy(other.gameObject);
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void Defeat()
    {
        gameManager.EndGame(true);
    }
}
