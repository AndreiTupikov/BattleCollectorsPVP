using Photon.Pun;
using System;
using UnityEngine;

public class MoneySpawn : MonoBehaviour
{
    public int count;
    public GameObject[] money;
    private float borderX = 10.5f;
    private float borderY = 4.5f;

    private void Start()
    {
        if (PhotonNetwork.PlayerList[0].UserId == PhotonNetwork.LocalPlayer.UserId)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 position = new Vector3(UnityEngine.Random.Range(-borderX, borderX), UnityEngine.Random.Range(-borderY, borderY), 0);
                if (Math.Abs(position.x) > 8 && Math.Abs(position.y) < 2)
                {
                    i--;
                    continue;
                }
                PhotonNetwork.Instantiate(money[UnityEngine.Random.Range(0, 3)].name, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
            }
        }
    }
}
