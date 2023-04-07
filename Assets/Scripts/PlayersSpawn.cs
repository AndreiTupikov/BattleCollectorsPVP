using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayersSpawn : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Transform startPosition1;
    public Transform startPosition2;
    public GameObject playerInfo;
    public RectTransform canvas;
    public JoystickController joystick;
    public FireButton fireButton;
    public Text score;

    private void Start()
    {
        bool isFirstPlayer = PhotonNetwork.PlayerList[0].UserId == PhotonNetwork.LocalPlayer.UserId ? true : false;
        var position = isFirstPlayer ? startPosition1 : startPosition2;
        var playerPrefab = isFirstPlayer ? player1 : player2;
        GameObject info = PhotonNetwork.Instantiate(playerInfo.name, position.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, position.position, position.rotation);
        player.GetComponent<PlayerController>().playerInfo = info.transform;
        player.GetComponent<PlayerController>().gameManager = GetComponent<GameManager>();
        player.GetComponent<PlayerController>().scoreText = score;
        info.GetComponentInChildren<Text>().text = DataHolder.playerName;
        GetComponent<GameManager>().SendPlayerInfo(info.GetComponent<PhotonView>().ViewID, DataHolder.playerName);
        GetComponent<GameManager>().player = player.GetComponent<PlayerController>();
        joystick.playerController = player.GetComponent<PlayerController>();
        fireButton.playerController = player.GetComponent<PlayerController>();
    }
}
