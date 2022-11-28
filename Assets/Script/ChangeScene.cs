using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  public Animator transition;
  public float transitionTime = 1f;

  public void LoadMonsterpedia()
  {
    StartCoroutine(Scene("Monsterpedia"));
  }

  public void LoadBattle()
  {
      StartCoroutine(Scene("Battle"));
  }

  public void LoadStart()
  {
    StartCoroutine(Scene("Start"));
  }

  public void LoadSetTeam()
  {
    StartCoroutine(Scene("SetTeamOne"));
  }

  public void LoadSetTeamTwo()
  {
    StartCoroutine(Scene("SetTeamTwo"));
  }

  public void LoadLobby()
  {
    StartCoroutine(Scene("Lobby"));
  }

  public void LoadCredit()
  {
    StartCoroutine(Scene("Credit"));
  }

  IEnumerator Scene(string sceneName)
  {
    transition.SetTrigger("Start");
    yield return new WaitForSeconds(transitionTime);
    SceneManager.LoadScene(sceneName);
  }

}
