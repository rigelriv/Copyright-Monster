using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum PickTurn {PLAYERONEDRAFT, PLAYERTWODRAFT}

public class NewPanelController : MonoBehaviourPunCallbacks
{
  [Header("Game Object")]
  public GameObject patamonGO, kumamonGO, agumonGO, guilmonGO, veemonGO, monmonGO, renamonGO, kotemonGO;
  public GameObject panel, canvas;
  public GameObject slotOne, slotTwo, slotThree, slotFour, slotFive, slotSix;
  public ChangeScene gantiPlisWork;

  [Header("Monster")]
  public Monster patamon, kumamon, agumon, guilmon, veemon, monmon, renamon, kotemon;
  Monster currentMonster;
  public Image firstImage;
  public Text skillName;
  public PickTurn draftTurn;

  [Header("Animator")]
  Animator slotOneAnimator, slotTwoAnimator, slotThreeAnimator, slotFourAnimator, slotFiveAnimator, slotSixAnimator;

  bool active = false;
  PhotonView viewDraft;


    void Start()
    {
      viewDraft = GetComponent<PhotonView>();
      panel.transform.gameObject.SetActive(false);
      slotOne.transform.gameObject.SetActive(false);
      slotTwo.transform.gameObject.SetActive(false);
      slotThree.transform.gameObject.SetActive(false);
      slotFour.transform.gameObject.SetActive(false);
      slotFive.transform.gameObject.SetActive(false);
      slotSix.transform.gameObject.SetActive(false);
      slotOneAnimator = slotOne.GetComponent<Animator>();
      slotTwoAnimator = slotTwo.GetComponent<Animator>();
      slotThreeAnimator = slotThree.GetComponent<Animator>();
      slotFourAnimator = slotFour.GetComponent<Animator>();
      slotFiveAnimator = slotFive.GetComponent<Animator>();
      slotSixAnimator = slotSix.GetComponent<Animator>();
      draftTurn = PickTurn.PLAYERONEDRAFT;
      DontDestroy.party = new List<Monster>();
      DontDestroyTwo.party = new List<Monster>();
    }

    void Update()
    {
      if(DontDestroyTwo.party.Count == 3){
        gantiPlisWork.LoadBattle();
      }
    }

    [PunRPC]
    public void PatamonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(patamon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          patamonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERTWODRAFT;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERTWODRAFT;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERTWODRAFT;
          }
        }
      }

      else if (draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(patamon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          patamonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecutePatamonToParty()
    {
      viewDraft.RPC("PatamonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void KumamonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(kumamon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          kumamonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERTWODRAFT;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERTWODRAFT;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERTWODRAFT;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(kumamon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          kumamonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteKumamonToParty()
    {
      viewDraft.RPC("KumamonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void AgumonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(agumon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          agumonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(agumon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          agumonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteAgumonToParty()
    {
      viewDraft.RPC("AgumonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void VeemonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(veemon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          veemonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(veemon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          veemonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteVeemonToParty()
    {
      viewDraft.RPC("VeemonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void MonmonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(monmon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          monmonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(monmon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          monmonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteMonmonToParty()
    {
      viewDraft.RPC("MonmonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void KotemonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(kotemon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          kotemonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(kotemon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          kotemonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteKotemonToParty()
    {
      viewDraft.RPC("KotemonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void RenamonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(renamon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          renamonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(renamon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          renamonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteRenamonToParty()
    {
      viewDraft.RPC("RenamonToParty", RpcTarget.All);
    }

    [PunRPC]
    public void GuilmonToParty()
    {
      if(draftTurn == PickTurn.PLAYERONEDRAFT){
        if(DontDestroy.party.Count <= 2){
          DontDestroy.party.Add(guilmon);
          draftTurn = PickTurn.PLAYERTWODRAFT;
          guilmonGO.transform.gameObject.SetActive(false);
          if(DontDestroy.party.Count == 1){
            slotOne.transform.gameObject.SetActive(true);
            slotOneAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 2){
            slotTwo.transform.gameObject.SetActive(true);
            slotTwoAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
          }
          else if (DontDestroy.party.Count == 3){
            slotThree.transform.gameObject.SetActive(true);
            slotThreeAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
          }
        }
      }

      else if(draftTurn == PickTurn.PLAYERTWODRAFT){
        if(DontDestroyTwo.party.Count <= 2){
          DontDestroyTwo.party.Add(guilmon);
          draftTurn = PickTurn.PLAYERONEDRAFT;
          guilmonGO.transform.gameObject.SetActive(false);
          if(DontDestroyTwo.party.Count == 1){
            slotFour.transform.gameObject.SetActive(true);
            slotFourAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 2){
            slotFive.transform.gameObject.SetActive(true);
            slotFiveAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
          else if (DontDestroyTwo.party.Count == 3){
            slotSix.transform.gameObject.SetActive(true);
            slotSixAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
            draftTurn = PickTurn.PLAYERONEDRAFT;
          }
        }
      }

      else{return;}
    }

    public void ExecuteGuilmonToParty()
    {
      viewDraft.RPC("GuilmonToParty", RpcTarget.All);
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
      if (DontDestroy.party.Count == 1){
        DontDestroy.party.Remove(DontDestroy.party[0]);
        slotOne.transform.gameObject.SetActive(false);
      }
      else if (DontDestroy.party.Count == 2){
        DontDestroy.party.Remove(DontDestroy.party[0]);
        slotOneAnimator.runtimeAnimatorController = slotTwoAnimator.runtimeAnimatorController;
        slotTwo.transform.gameObject.SetActive(false);
      }
      else if (DontDestroy.party.Count == 3){
        DontDestroy.party.Remove(DontDestroy.party[0]);
        slotOneAnimator.runtimeAnimatorController = slotTwoAnimator.runtimeAnimatorController;
        slotTwoAnimator.runtimeAnimatorController = slotThreeAnimator.runtimeAnimatorController;
        slotThree.transform.gameObject.SetActive(false);
      }
    }

    public void PartyManagementTwo()
    {
      if (DontDestroy.party.Count == 2){
        DontDestroy.party.Remove(DontDestroy.party[1]);
        slotTwo.transform.gameObject.SetActive(false);
      }
      else if (DontDestroy.party.Count == 3){
        DontDestroy.party.Remove(DontDestroy.party[1]);
        slotTwoAnimator.runtimeAnimatorController = slotThreeAnimator.runtimeAnimatorController;
        slotThree.transform.gameObject.SetActive(false);
      }
    }

    public void PartyManagementThree()
    {
      if (DontDestroy.party.Count == 3){
        DontDestroy.party.Remove(DontDestroy.party[2]);
        slotThree.transform.gameObject.SetActive(false);
      }
    }

}
