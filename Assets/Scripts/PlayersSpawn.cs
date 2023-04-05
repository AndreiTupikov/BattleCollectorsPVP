using Photon.Pun;
using UnityEngine;

public class PlayersSpawn : MonoBehaviour
{
    public Transform startPosition1;
    public Transform startPosition2;
    public GameObject playerPrefab;
    public JoystickController joystick;

    private void Start()
    {
        var position = PhotonNetwork.PlayerList[0].UserId == PhotonNetwork.LocalPlayer.UserId ? startPosition1 : startPosition2;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, position.position, Quaternion.identity);
        joystick.playerController = player.GetComponent<PlayerController>();
    }
}
