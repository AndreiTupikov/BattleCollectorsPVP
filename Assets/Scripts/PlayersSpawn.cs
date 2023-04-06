using Photon.Pun;
using UnityEngine;

public class PlayersSpawn : MonoBehaviour
{
    public Transform startPosition1;
    public Transform startPosition2;
    public GameObject player1;
    public GameObject player2;
    public JoystickController joystick;
    public FireButton fireButton;

    private void Start()
    {
        bool isFirstPlayer = PhotonNetwork.PlayerList[0].UserId == PhotonNetwork.LocalPlayer.UserId ? true : false;
        var position = isFirstPlayer ? startPosition1 : startPosition2;
        var playerPrefab = isFirstPlayer ? player1 : player2;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, position.position, Quaternion.identity);
        joystick.playerController = player.GetComponent<PlayerController>();
        fireButton.playerController = player.GetComponent<PlayerController>();
    }
}
