using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    private byte maxPlayers = 2;

    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInput.text, options);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.PlayerList.Length == maxPlayers) PhotonNetwork.LoadLevel("Game");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.PlayerList.Length == maxPlayers) PhotonNetwork.LoadLevel("Game");
    }
}