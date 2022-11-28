using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateOrJoin : MonoBehaviourPunCallbacks
{
  public InputField createInput;
  public InputField joinInput;
  public Text textOne;
  public Text textTwo;
  public Text textThree;
  public AudioSource roomCreated;
  Animator animTwo;
  TypingTwo typing;

  void Start()
  {
    typing = textOne.GetComponent<TypingTwo>();
    textTwo.transform.gameObject.SetActive(false);
    textThree.transform.gameObject.SetActive(false);
    animTwo = textTwo.GetComponent<Animator>();
  }

  void Update()
  {
    if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
    {
      PhotonNetwork.LoadLevel("Draft Pick");
    }
  }

  public void CreateRoom()
  {
    PhotonNetwork.CreateRoom(createInput.text);
    textTwo.transform.gameObject.SetActive(true);
  }

  public void JoinRoom()
  {
    textThree.transform.gameObject.SetActive(true);
    PhotonNetwork.JoinRoom(joinInput.text);
  }

  public void JoinRandom()
  {
    textThree.transform.gameObject.SetActive(true);
    PhotonNetwork.JoinRandomRoom();
  }

  public override void OnJoinedRoom()
  {
    textTwo.transform.gameObject.SetActive(false);
    textThree.transform.gameObject.SetActive(false);
    roomCreated.Play();
    StartCoroutine(typing.ShowText());
  }


}
