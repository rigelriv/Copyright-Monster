using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuniorMovement : MonoBehaviour
{
  public Transform pos2;
  public float speed;
  public Transform startPos;

  Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
      nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = Vector3.MoveTowards(transform.position, nextPos, speed*Time.deltaTime);
      if(transform.position == startPos.position){
        nextPos = pos2.position;
      }
    }
}
