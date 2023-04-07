using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public Text oldPLayerName;
    public InputField newPlayerName;
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject soundOffButon;
    public GameObject soundOnButon;
    public GameObject waitingScreen;
    public AudioSource tapSound;
    private byte maxPlayers = 2;

    public void CreateRoom()
    {
        if (DataHolder.isSoundOn) tapSound.Play();
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInput.text, options);
    }

    public void JoinRoom()
    {
        if (DataHolder.isSoundOn) tapSound.Play();
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.PlayerList.Length == maxPlayers) PhotonNetwork.LoadLevel("Game");
        else
        {
            mainMenu.SetActive(false);
            waitingScreen.SetActive(true);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.PlayerList.Length == maxPlayers) PhotonNetwork.LoadLevel("Game");
    }

    public void WaitingCancel()
    {
        PhotonNetwork.LeaveRoom();
        mainMenu.SetActive(true);
        waitingScreen.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        if (DataHolder.isSoundOn) tapSound.Play();
        mainMenu.SetActive(false);
        settings.SetActive(true);
        newPlayerName.text = null;
        oldPLayerName.text = DataHolder.playerName;
    }

    public void CloseSettingsPanel()
    {
        if (DataHolder.isSoundOn) tapSound.Play();
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void ChangePlayerName()
    {
        if (DataHolder.isSoundOn) tapSound.Play();
        if (!string.IsNullOrEmpty(newPlayerName.text))
        {
            DataHolder.playerName = newPlayerName.text;
            oldPLayerName.text = newPlayerName.text;
            newPlayerName.text = null;
        }
    }

    public void SoundOff()
    {
        soundOnButon.SetActive(true);
        soundOffButon.SetActive(false);
        DataHolder.isSoundOn = false;
    }

    public void SoundOn()
    {
        tapSound.Play();
        soundOnButon.SetActive(false);
        soundOffButon.SetActive(true);
        DataHolder.isSoundOn = true;
    }
}