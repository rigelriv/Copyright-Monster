using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPanelControllerTwo : MonoBehaviour
{
  public GameObject panel;
  public GameObject slotOne, slotTwo, slotThree;
  public Monster patamon, kumamon, agumon, guilmon, veemon, monmon, renamon, kotemon;
  Monster currentMonster;
  public Image firstImage;
  public Text skillName;
  // public DontDestroyTwo dontDestroyTwo;
  Animator slotOneAnimator, slotTwoAnimator, slotThreeAnimator;

  bool active = false;


    void Start()
    {
      panel.transform.gameObject.SetActive(false);
      slotOne.transform.gameObject.SetActive(false);
      slotTwo.transform.gameObject.SetActive(false);
      slotThree.transform.gameObject.SetActive(false);
      slotOneAnimator = slotOne.GetComponent<Animator>();
      slotTwoAnimator = slotTwo.GetComponent<Animator>();
      slotThreeAnimator = slotThree.GetComponent<Animator>();
    }

    public void PatamonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(patamon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void KumamonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(kumamon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void AgumonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(agumon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void VeemonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(veemon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void MonmonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(monmon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void KotemonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(kotemon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void RenamonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(renamon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void GuilmonToParty()
    {
      if(DontDestroyTwo.party.Count <= 2){
        DontDestroyTwo.party.Add(guilmon);
        if(DontDestroyTwo.party.Count == 1){
          slotOne.transform.gameObject.SetActive(true);
          slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 2){
          slotTwo.transform.gameObject.SetActive(true);
          slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
        }
        else if (DontDestroyTwo.party.Count == 3){
          slotThree.transform.gameObject.SetActive(true);
          slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
        }
      }
      else{return;}
    }

    public void AddFirstSkill()
    {
      if (currentMonster.skillSet.Count <= 3){
        currentMonster.skillSet.Add(currentMonster.learnableSkill[0]);
      }
      else if (currentMonster.skillSet.Count > 3){
        return;
      }
    }

    public void Patamon()
    {
      firstImage.sprite = patamon.learnableSkill[0].skillImage;
      skillName.text = patamon.learnableSkill[0].skillName;
      currentMonster = patamon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Agumon()
    {
      firstImage.sprite = agumon.learnableSkill[0].skillImage;
      skillName.text = agumon.learnableSkill[0].skillName;
      currentMonster = agumon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Renamon()
    {
      firstImage.sprite = renamon.learnableSkill[0].skillImage;
      skillName.text = renamon.learnableSkill[0].skillName;
      currentMonster = renamon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Kumamon()
    {
      firstImage.sprite = kumamon.learnableSkill[0].skillImage;
      skillName.text = kumamon.learnableSkill[0].skillName;
      currentMonster = kumamon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Guilmon()
    {
      firstImage.sprite = guilmon.learnableSkill[0].skillImage;
      skillName.text = guilmon.learnableSkill[0].skillName;
      currentMonster = guilmon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Kotemon()
    {
      firstImage.sprite = kotemon.learnableSkill[0].skillImage;
      skillName.text = kotemon.learnableSkill[0].skillName;
      currentMonster = kotemon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Monmon()
    {
      firstImage.sprite = monmon.learnableSkill[0].skillImage;
      skillName.text = monmon.learnableSkill[0].skillName;
      currentMonster = monmon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void Veemon()
    {
      firstImage.sprite = veemon.learnableSkill[0].skillImage;
      skillName.text = veemon.learnableSkill[0].skillName;
      currentMonster = veemon;
      if (active == false){
        panel.transform.gameObject.SetActive(true);
        active = true;
      }
      else {
        panel.transform.gameObject.SetActive(false);
        active = false;
      }
    }

    public void PartyManagementOne()
    {
      if (DontDestroyTwo.party.Count == 1){
        DontDestroyTwo.party.Remove(DontDestroyTwo.party[0]);
        slotOne.transform.gameObject.SetActive(false);
      }
      else if (DontDestroyTwo.party.Count == 2){
        DontDestroyTwo.party.Remove(DontDestroyTwo.party[0]);
        slotOneAnimator.runtimeAnimatorController = slotTwoAnimator.runtimeAnimatorController;
        slotTwo.transform.gameObject.SetActive(false);
      }
      else if (DontDestroyTwo.party.Count == 3){
        DontDestroyTwo.party.Remove(DontDestroyTwo.party[0]);
        slotOneAnimator.runtimeAnimatorController = slotTwoAnimator.runtimeAnimatorController;
        slotTwoAnimator.runtimeAnimatorController = slotThreeAnimator.runtimeAnimatorController;
        slotThree.transform.gameObject.SetActive(false);
      }
    }

    public void PartyManagementTwo()
    {
      if (DontDestroyTwo.party.Count == 2){
        DontDestroyTwo.party.Remove(DontDestroyTwo.party[1]);
        slotTwo.transform.gameObject.SetActive(false);
      }
      else if (DontDestroyTwo.party.Count == 3){
        DontDestroyTwo.party.Remove(DontDestroyTwo.party[1]);
        slotTwoAnimator.runtimeAnimatorController = slotThreeAnimator.runtimeAnimatorController;
        slotThree.transform.gameObject.SetActive(false);
      }
    }

    public void PartyManagementThree()
    {
      if (DontDestroyTwo.party.Count == 3){
        DontDestroyTwo.party.Remove(DontDestroyTwo.party[2]);
        slotThree.transform.gameObject.SetActive(false);
      }
    }

}
