using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public InputField newPlayerName;
    public GameObject mainMenu;
    public GameObject settings;
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
        /*if (PhotonNetwork.PlayerList.Length == maxPlayers)*/ PhotonNetwork.LoadLevel("Game");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.PlayerList.Length == maxPlayers) PhotonNetwork.LoadLevel("Game");
    }

    public void OpenSettingsPanel()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void ChangePlayerName()
    {
        if (!string.IsNullOrEmpty(newPlayerName.text))
        {
            DataHolder.playerName = newPlayerName.text;
        }
    }
}