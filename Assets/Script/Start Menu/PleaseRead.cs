using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseRead : MonoBehaviour
{
  public TypingTwo bugText;

    void Start()
    {
      bugText.transform.gameObject.SetActive(false);
    }

    public void SansPlay()
    {
      bugText.transform.gameObject.SetActive(true);
      StartCoroutine(bugText.ShowText());
    }
}
