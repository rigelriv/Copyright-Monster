using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
  public ChangeScene changeScene;
  public AudioSource connectSuccess;

    void Start()
    {
      PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
      PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
      connectSuccess.Play();
      changeScene.LoadStart();
    }
}
