using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject kumamonPanel;
    public GameObject patamonPanel;
    public GameObject guilmonPanel;
    public GameObject renamonPanel;
    public GameObject agumonPanel;
    public GameObject monmonPanel;
    public GameObject veemonPanel;
    public GameObject kotemonPanel;
    bool active = false;

    void Start()
    {
      kumamonPanel.transform.gameObject.SetActive(false);
      patamonPanel.transform.gameObject.SetActive(false);
      guilmonPanel.transform.gameObject.SetActive(false);
      renamonPanel.transform.gameObject.SetActive(false);
      agumonPanel.transform.gameObject.SetActive(false);
      monmonPanel.transform.gameObject.SetActive(false);
      veemonPanel.transform.gameObject.SetActive(false);
      kotemonPanel.transform.gameObject.SetActive(false);
    }

    public void KumamonPanel()
    {
      if (active == false){
        kumamonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        kumamonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void PatamonPanel()
    {
      if (active == false){
        patamonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        patamonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void GuilmonPanel()
    {
      if (active == false){
        guilmonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        guilmonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void RenamonPanel()
    {
      if (active == false){
        renamonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        renamonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void AgumonPanel()
    {
      if (active == false){
        agumonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        agumonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void MonmonPanel()
    {
      if (active == false){
        monmonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        monmonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void VeemonPanel()
    {
      if (active == false){
        veemonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        veemonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void KotemonPanel()
    {
      if (active == false){
        kotemonPanel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        kotemonPanel.transform.gameObject.SetActive(false);
        active = false;
      }
    }
}
