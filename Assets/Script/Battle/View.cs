using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class View : MonoBehaviour
{
  PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
      view = GetComponent<PhotonView>();
    }
}
