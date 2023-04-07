using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
}
