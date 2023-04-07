using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject joystick;
    public GameObject fireButton;
    public GameObject endGameScreen;
    public Text winnerName;
    public Text winnerScore;
    public PlayerController player;
    private PhotonView view;
    private RectTransform canvas;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        canvas = GetComponent<PlayersSpawn>().canvas;
    }

    public void SendPlayerInfo(int viewId, string name)
    {
        view.RPC("SetAnotherPlayerInfo", RpcTarget.AllBuffered, viewId, name);
    }

    [PunRPC]
    private void SetAnotherPlayerInfo(int viewId, string name)
    {
        GameObject playerInfo = PhotonNetwork.GetPhotonView(viewId).gameObject;
        playerInfo.GetComponentInChildren<Text>().text = name;
        playerInfo.transform.SetParent(canvas, true);
    }

    public void SendPlayerHealth(int viewId, float hp)
    {
        view.RPC("SetHealthPoints", RpcTarget.OthersBuffered, viewId, hp);
    }

    [PunRPC]
    private void SetHealthPoints(int viewId, float hp)
    {
        Image health = PhotonNetwork.GetPhotonView(viewId).gameObject.GetComponentInChildren<Image>();
        health.fillAmount = hp;
        if (health.fillAmount < 0.7) health.color = Color.yellow;
        if (health.fillAmount < 0.4) health.color = Color.red;
    }

    public void EndGame(bool looser)
    {
        joystick.SetActive(false);
        fireButton.SetActive(false);
        endGameScreen.SetActive(true);
        if (looser) view.RPC("WinGame", RpcTarget.Others);
    }

    [PunRPC]
    private void WinGame()
    {
        EndGame(false);
        winnerName.text = DataHolder.playerName;
        winnerScore.text += player.score;
        view.RPC("LoseGame", RpcTarget.Others, DataHolder.playerName, player.score);
    }

    [PunRPC]
    private void LoseGame(string name, int score)
    {
        winnerName.text = name;
        winnerScore.text += score;
    }

    public void BackToLobby()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Lobby");
    }
}
