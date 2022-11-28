using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingTwo : MonoBehaviour
{
  public string fullText;
  private string currentText;
  public AudioSource type;
  public float time;
    // Start is called before the first frame update

    public IEnumerator ShowText()
    {
      for (int i = 0; i<fullText.Length + 1; i++){
        currentText = fullText.Substring(0,i);
        this.GetComponent<Text>().text = currentText;
        type.Play();
        yield return new WaitForSeconds(time);
      }
    }
}
