using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum BattleState {START, PLAYERTURN, PLAYERATTACK, PLAYERSUPPORT, ENEMYTURN, ENEMYATTACK, WIN, LOSE}

public class BattleSystem : MonoBehaviourPunCallbacks
{
    [Header("Monster Unit")]
    //public DontDestroy firstMonsterUnit;
    public BattleHud playerHUD;
    // public DontDestroy secondMonsterUnit;
    // public DontDestroy thirdMonsterUnit;
    // public DontDestroyTwo playerTwoUnit;

    [Header("Game Object")]
    public GameObject firstMonsterUnitGO;
    public GameObject secondMonsterUnitGO;
    public GameObject thirdMonsterUnitGO;
    public GameObject fourthMonsterUnitGO;
    public GameObject fifthMonsterUnitGO;
    public GameObject sixthMonsterUnitGO;
    public GameObject floatingPoint;

    Animator firstAnimator;
    Animator secondAnimator;
    Animator thirdAnimator;
    Animator fourthAnimator;
    Animator fifthAnimator;
    Animator sixthAnimator;
    public GameObject playerPartyGO;
    public GameObject playerTwoPartyGO;
    BattleUnit firstUnit, secondUnit, thirdUnit;
    public BattleUnit fourthMonsterUnit;
    public BattleUnit fifthMonsterUnit;
    public BattleUnit sixthMonsterUnit;
    Text damageText;
    Text statReduceText;
    Text burnEffectText;
    int takenDamage;

    [Header("Audio")]
    public AudioSource audioOne;
    public AudioSource audioTwo;
    public AudioSource audioThree;
    public AudioSource audioFour;
    public AudioSource audioHeal;
    public AudioSource audioPowerUp;

    [Header("Battle State and Turns")]
    public BattleState state;
    bool firstMonsterTurn = false;
    bool secondMonsterTurn = false;
    bool thirdMonsterTurn = false;
    bool isFirstSelected = false;
    bool isSecondSelected = false;
    bool isThirdSelected = false;
    bool isFourthSelected = false;
    bool isFifthSelected = false;
    bool isSixthSelected = false;
    bool firstAttackerTurn = false;
    bool secondAttackerTurn = false;
    bool thirdAttackerTurn = false;
    bool isFirstDead = false;
    bool isSecondDead = false;
    bool isThirdDead = false;
    bool isFourthDead = false;
    bool isFifthDead = false;
    bool isSixthDead = false;
    int currentMove;
    public int totalTurn;

    [Header("Special Effect Text Pop Up")]
    public GameObject statReduce;
    public GameObject burnEffect;

    [Header("Material")]
    public Material flashEffect;
    public Material healFlashEffect;
    public Material powerUpFlashEffect;

    PhotonView photonView;
    PhotonView partyView;
    PhotonView partyTwoView;

    void Start()
    {
      photonView = GetComponent<PhotonView>();
      //state
      state = BattleState.START;

      totalTurn = 0;
      SetupBattle();
      SetupFirstAnimation();
      SetupSecondAnimation();
      SetupThirdAnimation();
      SetupFourthAnimation();
      SetupFifthAnimation();
      SetupSixthAnimation();
      flashBeginning();

      StartCoroutine(PlayMusic());

      playerHUD.SetPlayerData();

      //Hide Moves Text Field
      playerHUD.firstMove.transform.gameObject.SetActive(false);
      playerHUD.secondMove.transform.gameObject.SetActive(false);
      playerHUD.thirdMove.transform.gameObject.SetActive(false);
      playerHUD.fourthMove.transform.gameObject.SetActive(false);

      PlayerTurn();

    }

    void flashBeginning()
    {
      firstMonsterUnitGO.GetComponent<Image>().material = flashEffect;
      secondMonsterUnitGO.GetComponent<Image>().material = flashEffect;
      thirdMonsterUnitGO.GetComponent<Image>().material = flashEffect;
      fourthMonsterUnitGO.GetComponent<Image>().material = flashEffect;
      fifthMonsterUnitGO.GetComponent<Image>().material = flashEffect;
      sixthMonsterUnitGO.GetComponent<Image>().material = flashEffect;
    }

    void CheckTurn()
    {
      if (firstMonsterTurn == false && secondMonsterTurn == false && thirdMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isThirdDead == true && firstMonsterTurn == false && secondMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isSecondDead == true && firstMonsterTurn == false && thirdMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isFirstDead == true && thirdMonsterTurn == false && secondMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isSecondDead == true && isThirdDead == true && firstMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isFirstDead == true && isThirdDead == true && secondMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isFirstDead == true && isSecondDead == true && thirdMonsterTurn == false){
        state = BattleState.ENEMYTURN;
        EnemyTurn();
      }
      else if (isFourthDead == true && isFifthDead == true && isSixthDead == true){
        state = BattleState.WIN;
        playerHUD.nameText.text = "Player One Win !";
      }
      else {
        return;
      }
    }

    void EnemyCheckTurn()
    {
      if(firstAttackerTurn == false && secondAttackerTurn == false && thirdAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else if(isFourthDead == true && secondAttackerTurn == false && thirdAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else if(isFifthDead == true && firstAttackerTurn == false && thirdAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else if(isSixthDead == true && firstAttackerTurn == false && secondAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else if(isFourthDead == true && isFifthDead == true && thirdAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else if(isFourthDead == true && isSixthDead == true && secondAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else if(isFifthDead == true && isSixthDead == true && firstAttackerTurn == false){
        state = BattleState.PLAYERTURN;
        PlayerTurn();
      }
      else{return;}
    }

    void CheckDie()
    {
      if (isFourthDead == true && isFifthDead == true && isSixthDead == true){
        state = BattleState.WIN;
        playerHUD.nameText.text = "Player One Win !";
      }
      else if (isFirstDead == true && isSecondDead == true && isThirdDead == true){
        state = BattleState.LOSE;
        playerHUD.nameText.text = "Player Two Win !";
      }
      else {return;}
    }


    //Stateeeee
    public void PlayerTurn()
    {
      totalTurn += 1;
      state = BattleState.PLAYERTURN;
      playerHUD.nameText.text = "Player One Turn";
      firstMonsterTurn = true;
      secondMonsterTurn = true;
      thirdMonsterTurn = true;
      StartCoroutine(DebuffCheck(firstMonsterUnitGO, firstUnit, "FirstUnit", 0.7f, firstAnimator, isFirstDead, firstMonsterTurn));
      playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
      StartCoroutine(DebuffCheck(secondMonsterUnitGO, secondUnit, "SecondUnit", 0.7f, secondAnimator, isSecondDead, secondMonsterTurn));
      playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
      StartCoroutine(DebuffCheck(thirdMonsterUnitGO, thirdUnit, "ThirdUnit", 0.7f, thirdAnimator, isThirdDead, thirdMonsterTurn));
      playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
    }

    public void EnemyTurn()
    {
      totalTurn += 1;
      firstAttackerTurn = true;
      secondAttackerTurn = true;
      thirdAttackerTurn = true;
      playerHUD.nameText.text = "Player Two Turn";
      StartCoroutine(DebuffCheck(fourthMonsterUnitGO, fourthMonsterUnit, "Canvas", 0.7f, fourthAnimator, isFourthDead, firstAttackerTurn));
      playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
      StartCoroutine(DebuffCheck(fifthMonsterUnitGO, fifthMonsterUnit, "Canvas2", 0.7f, fifthAnimator, isFifthDead, secondAttackerTurn));
      playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
      StartCoroutine(DebuffCheck(sixthMonsterUnitGO, sixthMonsterUnit, "Canvas3", 0.7f, sixthAnimator, isSixthDead, thirdAttackerTurn));
      playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);

      //StartCoroutine(EnemyAttack());
    }




    //Monster selection
    [PunRPC]
    public void FirstMonsterSelected()
    {
      if (state == BattleState.PLAYERTURN && firstMonsterTurn == true){
        audioOne.Play();
        playerHUD.HideText();
        playerHUD.OpenFirstMoveSet();
        //firstMonsterTurn = false;
        isFirstSelected = true;
        isSecondSelected = false;
        isThirdSelected = false;
      }

      //Heal
      else if (state == BattleState.PLAYERATTACK && isFirstSelected == true && firstUnit.monster.skillSet[currentMove].skillName == "Heal"){
        firstMonsterTurn = false;
        audioHeal.Play();
        firstUnit.Support(firstUnit.monster, firstUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(firstMonsterUnitGO));
        StartCoroutine(HitAnimation(firstAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.firstHpBar, DontDestroy.party[0]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isSecondSelected == true && secondUnit.monster.skillSet[currentMove].skillName == "Heal"){
        secondMonsterTurn = false;
        audioHeal.Play();
        firstUnit.Support(firstUnit.monster, secondUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(firstMonsterUnitGO));
        StartCoroutine(HitAnimation(firstAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.firstHpBar, DontDestroy.party[0]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isThirdSelected == true && thirdUnit.monster.skillSet[currentMove].skillName == "Heal"){
        thirdMonsterTurn = false;
        audioHeal.Play();
        firstUnit.Support(firstUnit.monster, thirdUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(firstMonsterUnitGO));
        StartCoroutine(HitAnimation(firstAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.firstHpBar, DontDestroy.party[0]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }

      //Power Up
      else if (state == BattleState.PLAYERATTACK && isFirstSelected == true && firstUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        firstMonsterTurn = false;
        audioPowerUp.Play();
        firstUnit.Support(firstUnit.monster, firstUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(firstMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(firstUnit.monster.skillSet[currentMove], "FirstUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isSecondSelected == true && secondUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        secondMonsterTurn = false;
        audioPowerUp.Play();
        firstUnit.Support(firstUnit.monster, secondUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(firstMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(secondUnit.monster.skillSet[currentMove], "FirstUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isThirdSelected == true && thirdUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        thirdMonsterTurn = false;
        audioPowerUp.Play();
        firstUnit.Support(firstUnit.monster, thirdUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(firstMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(thirdUnit.monster.skillSet[currentMove], "FirstUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }

      else if (state == BattleState.ENEMYATTACK){
        audioTwo.Play();
        if(isFourthSelected == true){
          firstAttackerTurn = false;
          isFirstDead = firstUnit.TakeDamage(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = firstUnit.GetDamageValue(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[currentMove], "FirstUnit"));
          if(isFirstDead){
            firstMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        else if(isFifthSelected == true){
          secondAttackerTurn = false;
          isFirstDead = firstUnit.TakeDamage(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = firstUnit.GetDamageValue(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[currentMove], "FirstUnit"));
          if(isFirstDead){
            firstMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        else if(isSixthSelected == true){
          thirdAttackerTurn = false;
          isFirstDead = firstUnit.TakeDamage(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = firstUnit.GetDamageValue(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[currentMove], "FirstUnit"));
          if(isFirstDead){
            firstMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        if(isFirstDead == false)
        {
          StartCoroutine(HitFlashEffect(firstMonsterUnitGO));
          StartCoroutine(HitAnimation(firstAnimator));
          damageText.text = takenDamage.ToString();
          GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
          floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("FirstUnit").transform, false);
          state = BattleState.ENEMYTURN;
          playerHUD.UpdateHP(playerHUD.firstHpBar, DontDestroy.party[0]);
          playerHUD.nameText.text = "Player Two Turn";
          EnemyCheckTurn();
        }
      }
    }

    public void ExecuteFirstMonsterSelected()
    {
      photonView.RPC("FirstMonsterSelected", RpcTarget.All);
    }

    [PunRPC]
    public void SecondMonsterSelected()
    {
      if (state == BattleState.PLAYERTURN && secondMonsterTurn == true){
        audioOne.Play();
        playerHUD.HideText();
        playerHUD.OpenSecondMoveSet();
        //secondMonsterTurn = false;
        isFirstSelected = false;
        isSecondSelected = true;
        isThirdSelected = false;
      }

      //Heal
      else if (state == BattleState.PLAYERATTACK && isFirstSelected == true && firstUnit.monster.skillSet[currentMove].skillName == "Heal"){
        firstMonsterTurn = false;
        audioHeal.Play();
        secondUnit.Support(secondUnit.monster, firstUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(secondMonsterUnitGO));
        StartCoroutine(HitAnimation(secondAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.secondHpBar, DontDestroy.party[1]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isSecondSelected == true && secondUnit.monster.skillSet[currentMove].skillName == "Heal"){
        secondMonsterTurn = false;
        audioHeal.Play();
        secondUnit.Support(secondUnit.monster, secondUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(secondMonsterUnitGO));
        StartCoroutine(HitAnimation(secondAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.secondHpBar, DontDestroy.party[1]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isThirdSelected == true && thirdUnit.monster.skillSet[currentMove].skillName == "Heal"){
        thirdMonsterTurn = false;
        audioHeal.Play();
        secondUnit.Support(secondUnit.monster, thirdUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(secondMonsterUnitGO));
        StartCoroutine(HitAnimation(secondAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.secondHpBar, DontDestroy.party[1]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }

      //Power Up
      else if (state == BattleState.PLAYERATTACK && isFirstSelected == true && firstUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        firstMonsterTurn = false;
        audioPowerUp.Play();
        secondUnit.Support(secondUnit.monster, firstUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(secondMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(firstUnit.monster.skillSet[currentMove], "SecondUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isSecondSelected == true && secondUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        secondMonsterTurn = false;
        audioPowerUp.Play();
        secondUnit.Support(secondUnit.monster, secondUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(secondMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(secondUnit.monster.skillSet[currentMove], "SecondUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isThirdSelected == true && thirdUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        thirdMonsterTurn = false;
        audioPowerUp.Play();
        secondUnit.Support(secondUnit.monster, thirdUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(secondMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(thirdUnit.monster.skillSet[currentMove], "SecondUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }

      else if (state == BattleState.ENEMYATTACK){
        audioTwo.Play();
        if(isFourthSelected == true){
          firstAttackerTurn = false;
          isSecondDead = secondUnit.TakeDamage(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = secondUnit.GetDamageValue(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[currentMove], "SecondUnit"));
          if(isSecondDead){
            secondMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        else if(isFifthSelected == true){
          secondAttackerTurn = false;
          isSecondDead = secondUnit.TakeDamage(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = secondUnit.GetDamageValue(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[currentMove], "SecondUnit"));
          if(isSecondDead){
            secondMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        else if(isSixthSelected == true){
          thirdAttackerTurn = false;
          isSecondDead = secondUnit.TakeDamage(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = secondUnit.GetDamageValue(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[currentMove], "SecondUnit"));
          if(isSecondDead){
            secondMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        if(isSecondDead == false)
        {
          StartCoroutine(HitFlashEffect(secondMonsterUnitGO));
          StartCoroutine(HitAnimation(secondAnimator));
          damageText.text = takenDamage.ToString();
          GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
          floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("SecondUnit").transform, false);
          state = BattleState.ENEMYTURN;
          playerHUD.UpdateHP(playerHUD.secondHpBar, DontDestroy.party[1]);
          playerHUD.nameText.text = "Player Two Turn";
          EnemyCheckTurn();
        }
      }
    }

    public void ExecuteSecondMonsterSelected()
    {
      photonView.RPC("SecondMonsterSelected", RpcTarget.All);
    }

    [PunRPC]
    public void ThirdMonsterSelected()
    {
      if (state == BattleState.PLAYERTURN && thirdMonsterTurn == true){
        audioOne.Play();
        playerHUD.HideText();
        playerHUD.OpenThirdMoveSet();
        //thirdMonsterTurn = false;
        isFirstSelected = false;
        isSecondSelected = false;
        isThirdSelected = true;
      }

      //Heal
      else if (state == BattleState.PLAYERATTACK && isFirstSelected == true && firstUnit.monster.skillSet[currentMove].skillName == "Heal"){
        firstMonsterTurn = false;
        audioHeal.Play();
        thirdUnit.Support(thirdUnit.monster, firstUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(HitAnimation(thirdAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.thirdHpBar, DontDestroy.party[2]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isSecondSelected == true && secondUnit.monster.skillSet[currentMove].skillName == "Heal"){
        secondMonsterTurn = false;
        audioHeal.Play();
        thirdUnit.Support(thirdUnit.monster, secondUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(HitAnimation(thirdAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.thirdHpBar, DontDestroy.party[2]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isThirdSelected == true && thirdUnit.monster.skillSet[currentMove].skillName == "Heal"){
        thirdMonsterTurn = false;
        audioHeal.Play();
        thirdUnit.Support(thirdUnit.monster, thirdUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(HitAnimation(thirdAnimator));
        state = BattleState.PLAYERTURN;
        playerHUD.UpdateHP(playerHUD.thirdHpBar, DontDestroy.party[2]);
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }

      //Power Up
      else if (state == BattleState.PLAYERATTACK && isFirstSelected == true && firstUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        firstMonsterTurn = false;
        audioPowerUp.Play();
        thirdUnit.Support(thirdUnit.monster, firstUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(firstUnit.monster.skillSet[currentMove], "ThirdUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isSecondSelected == true && secondUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        secondMonsterTurn = false;
        audioPowerUp.Play();
        thirdUnit.Support(thirdUnit.monster, secondUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(secondUnit.monster.skillSet[currentMove], "ThirdUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }
      else if (state == BattleState.PLAYERATTACK && isThirdSelected == true && thirdUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        thirdMonsterTurn = false;
        audioPowerUp.Play();
        thirdUnit.Support(thirdUnit.monster, thirdUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(thirdUnit.monster.skillSet[currentMove], "ThirdUnit"));
        state = BattleState.PLAYERTURN;
        playerHUD.nameText.text = "Player One Turn";
        CheckTurn();
      }


      else if (state == BattleState.ENEMYATTACK){
        audioTwo.Play();
        if(isFourthSelected == true){
          firstAttackerTurn = false;
          isThirdDead = thirdUnit.TakeDamage(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = thirdUnit.GetDamageValue(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[currentMove], "ThirdUnit"));
          if(isThirdDead){
            thirdMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        else if(isFifthSelected == true){
          secondAttackerTurn = false;
          isThirdDead = thirdUnit.TakeDamage(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = thirdUnit.GetDamageValue(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[currentMove], "ThirdUnit"));
          if(isThirdDead){
            thirdMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        else if(isSixthSelected == true){
          thirdAttackerTurn = false;
          isThirdDead = thirdUnit.TakeDamage(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
          takenDamage = thirdUnit.GetDamageValue(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
          StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[currentMove], "ThirdUnit"));
          if(isThirdDead){
            thirdMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.ENEMYTURN;
            playerHUD.nameText.text = "Player Two Turn";
            EnemyCheckTurn();
          }
          if(isFirstDead == true && isSecondDead == true  && isThirdDead == true){
            state = BattleState.LOSE;
            playerHUD.nameText.text = "Player Two Win !";
          }
        }
        if(isThirdDead == false)
        {
          StartCoroutine(HitFlashEffect(thirdMonsterUnitGO));
          StartCoroutine(HitAnimation(thirdAnimator));
          damageText.text = takenDamage.ToString();
          GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
          floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("ThirdUnit").transform, false);
          state = BattleState.ENEMYTURN;
          playerHUD.UpdateHP(playerHUD.thirdHpBar, DontDestroy.party[2]);
          playerHUD.nameText.text = "Player Two Turn";
          EnemyCheckTurn();
        }
      }
    }

    public void ExecuteThirdMonsterSelected()
    {
      photonView.RPC("ThirdMonsterSelected", RpcTarget.All);
    }



    //Move selection
    [PunRPC]
    public void FirstMoveSelected()
    {
      if(state == BattleState.PLAYERTURN){
        audioOne.Play();
        currentMove = 0;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.PLAYERATTACK;
      }
      else if(state == BattleState.ENEMYTURN){
        audioOne.Play();
        currentMove = 0;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.ENEMYATTACK;
      }
    }

    public void ExecuteFirstMoveSelected()
    {
      photonView.RPC("FirstMoveSelected", RpcTarget.All);
    }

    [PunRPC]
    public void SecondMoveSelected()
    {
      if(state == BattleState.PLAYERTURN){
        audioOne.Play();
        currentMove = 1;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.PLAYERATTACK;
      }
      else if(state == BattleState.ENEMYTURN){
        audioOne.Play();
        currentMove = 1;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.ENEMYATTACK;
      }
    }

    public void ExecuteSecondMoveSelected()
    {
      photonView.RPC("SecondMoveSelected", RpcTarget.All);
    }

    [PunRPC]
    public void ThirdMoveSelected()
    {
      if(state == BattleState.PLAYERTURN){
        audioOne.Play();
        currentMove = 2;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.PLAYERATTACK;
      }
      else if(state == BattleState.ENEMYTURN){
        audioOne.Play();
        currentMove = 2;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.ENEMYATTACK;
      }
    }

    public void ExecuteThirdMoveSelected()
    {
      photonView.RPC("ThirdMoveSelected", RpcTarget.All);
    }

    [PunRPC]
    public void FourthMoveSelected()
    {
      if(state == BattleState.PLAYERTURN){
        audioOne.Play();
        currentMove = 3;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.PLAYERATTACK;
      }
      else if(state == BattleState.ENEMYTURN){
        audioOne.Play();
        currentMove = 3;
        playerHUD.HideMoveSet();
        playerHUD.nameText.text = "Select a target";
        playerHUD.ShowText();
        state = BattleState.ENEMYATTACK;
      }
    }

    public void ExecuteFourthMoveSelected()
    {
      photonView.RPC("FourthMoveSelected", RpcTarget.All);
    }



    //Skill Launcher Bitch
    [PunRPC]
    public void LaunchMoveTargetOne(int CurrentMove)
    {
      CurrentMove = currentMove;
      if (state == BattleState.PLAYERATTACK)
      {
        audioTwo.Play();
        if(isFirstSelected == true){
          firstMonsterTurn = false;
          isFourthDead = fourthMonsterUnit.TakeDamage(DontDestroy.party[0], DontDestroy.party[0].skillSet[CurrentMove]);
          takenDamage = fourthMonsterUnit.GetDamageValue(DontDestroy.party[0], DontDestroy.party[0].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[0].skillSet[CurrentMove], "Canvas"));
          if(isFourthDead){
            fourthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        else if(isSecondSelected == true){
          secondMonsterTurn = false;
          isFourthDead = fourthMonsterUnit.TakeDamage(DontDestroy.party[1], DontDestroy.party[1].skillSet[CurrentMove]);
          takenDamage = fourthMonsterUnit.GetDamageValue(DontDestroy.party[1], DontDestroy.party[1].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[1].skillSet[CurrentMove], "Canvas"));
          if(isFourthDead){
            fourthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        else if(isThirdSelected == true){
          thirdMonsterTurn = false;
          isFourthDead = fourthMonsterUnit.TakeDamage(DontDestroy.party[2], DontDestroy.party[2].skillSet[CurrentMove]);
          takenDamage = fourthMonsterUnit.GetDamageValue(DontDestroy.party[2], DontDestroy.party[2].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[2].skillSet[CurrentMove], "Canvas"));
          if(isFourthDead){
            fourthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        if(isFourthDead == false)
        {
          StartCoroutine(HitFlashEffect(fourthMonsterUnitGO));
          StartCoroutine(HitAnimation(fourthAnimator));
          damageText.text = takenDamage.ToString();
          GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
          floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
          state = BattleState.PLAYERTURN;
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.nameText.text = "Player One Turn";
          CheckTurn();
        }
      }

      else if (state == BattleState.ENEMYTURN && firstAttackerTurn == true){
        audioOne.Play();
        playerHUD.HideText();
        playerHUD.EnemyFirstMoveSet();
        //firstAttackerTurn = false;
        isFourthSelected = true;
        isFifthSelected = false;
        isSixthSelected = false;
      }

      //Heal
      else if (state == BattleState.ENEMYATTACK && isFourthSelected == true && fourthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        firstAttackerTurn = false;
        audioHeal.Play();
        fourthMonsterUnit.Support(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(fourthMonsterUnitGO));
        StartCoroutine(HitAnimation(fourthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.fourthHpBar, DontDestroyTwo.party[0]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isFifthSelected == true && fifthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        secondAttackerTurn = false;
        audioHeal.Play();
        fourthMonsterUnit.Support(fourthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(fifthMonsterUnitGO));
        StartCoroutine(HitAnimation(fifthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.fifthHpBar, DontDestroyTwo.party[1]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isSixthSelected == true && sixthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        thirdAttackerTurn = false;
        audioHeal.Play();
        fourthMonsterUnit.Support(fourthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(sixthMonsterUnitGO));
        StartCoroutine(HitAnimation(sixthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.sixthHpBar, DontDestroyTwo.party[2]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }

      //Power Up
      else if (state == BattleState.ENEMYATTACK && isFourthSelected == true && fourthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        firstAttackerTurn = false;
        audioPowerUp.Play();
        fourthMonsterUnit.Support(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(fourthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[currentMove], "Canvas"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isFifthSelected == true && fifthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        secondAttackerTurn = false;
        audioPowerUp.Play();
        fourthMonsterUnit.Support(fourthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(fourthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[currentMove], "Canvas"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isSixthSelected == true && sixthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        thirdAttackerTurn = false;
        audioPowerUp.Play();
        fourthMonsterUnit.Support(fourthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(fourthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[currentMove], "Canvas"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }


    }

    public void ExecuteLaunchMoveTargetOne()
    {
      photonView.RPC("LaunchMoveTargetOne", RpcTarget.All, currentMove);
    }

    [PunRPC]
    public void LaunchMoveTargetTwo(int CurrentMove)
    {
      CurrentMove = currentMove;
      if(state == BattleState.PLAYERATTACK)
      {
        audioTwo.Play();
        if(isFirstSelected == true){
          firstMonsterTurn = false;
          isFifthDead = fifthMonsterUnit.TakeDamage(DontDestroy.party[0], DontDestroy.party[0].skillSet[CurrentMove]);
          takenDamage = fifthMonsterUnit.GetDamageValue(DontDestroy.party[0], DontDestroy.party[0].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[0].skillSet[CurrentMove], "Canvas2"));
          if(isFifthDead){
            fifthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        else if(isSecondSelected == true){
          secondMonsterTurn = false;
          isFifthDead = fifthMonsterUnit.TakeDamage(DontDestroy.party[1], DontDestroy.party[1].skillSet[CurrentMove]);
          takenDamage = fifthMonsterUnit.GetDamageValue(DontDestroy.party[1], DontDestroy.party[1].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[1].skillSet[CurrentMove], "Canvas2"));
          if(isFifthDead){
            fifthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        else if(isThirdSelected == true){
          thirdMonsterTurn = false;
          isFifthDead = fifthMonsterUnit.TakeDamage(DontDestroy.party[2], DontDestroy.party[2].skillSet[CurrentMove]);
          takenDamage = fifthMonsterUnit.GetDamageValue(DontDestroy.party[2], DontDestroy.party[2].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[2].skillSet[CurrentMove], "Canvas2"));
          if(isFifthDead){
            fifthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        if(isFifthDead == false)
        {
          StartCoroutine(HitFlashEffect(fifthMonsterUnitGO));
          StartCoroutine(HitAnimation(fifthAnimator));
          damageText.text = takenDamage.ToString();
          GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
          floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas2").transform, false);
          state = BattleState.PLAYERTURN;
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.nameText.text = "Player One Turn";
          CheckTurn();
        }
      }
      else if (state == BattleState.ENEMYTURN && secondAttackerTurn == true){
        audioOne.Play();
        playerHUD.HideText();
        playerHUD.EnemySecondMoveSet();
        //secondAttackerTurn = false;
        isFourthSelected = false;
        isFifthSelected = true;
        isSixthSelected = false;
      }

      //Heal
      else if (state == BattleState.ENEMYATTACK && isFourthSelected == true && fourthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        firstAttackerTurn = false;
        audioHeal.Play();
        fifthMonsterUnit.Support(fifthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(fourthMonsterUnitGO));
        StartCoroutine(HitAnimation(fourthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.fourthHpBar, DontDestroyTwo.party[0]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isFifthSelected == true && fifthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        secondAttackerTurn = false;
        audioHeal.Play();
        fifthMonsterUnit.Support(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(fifthMonsterUnitGO));
        StartCoroutine(HitAnimation(fifthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.fifthHpBar, DontDestroyTwo.party[1]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isSixthSelected == true && sixthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        thirdAttackerTurn = false;
        audioHeal.Play();
        fifthMonsterUnit.Support(fifthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(sixthMonsterUnitGO));
        StartCoroutine(HitAnimation(sixthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.sixthHpBar, DontDestroyTwo.party[2]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }

      //Power Up
      else if (state == BattleState.ENEMYATTACK && isFourthSelected == true && fourthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        firstAttackerTurn = false;
        audioPowerUp.Play();
        fifthMonsterUnit.Support(fifthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(fifthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[currentMove], "Canvas2"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isFifthSelected == true && fifthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        secondAttackerTurn = false;
        audioPowerUp.Play();
        fifthMonsterUnit.Support(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(fifthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[currentMove], "Canvas2"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isSixthSelected == true && sixthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        thirdAttackerTurn = false;
        audioPowerUp.Play();
        fifthMonsterUnit.Support(fifthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(fifthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[currentMove], "Canvas2"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }

    }

    public void ExecuteLaunchMoveTargetTwo()
    {
      photonView.RPC("LaunchMoveTargetTwo", RpcTarget.All, currentMove);
    }

    [PunRPC]
    public void LaunchMoveTargetThree(int CurrentMove)
    {
      CurrentMove = currentMove;
      if(state == BattleState.PLAYERATTACK)
      {
        audioTwo.Play();
        if(isFirstSelected == true){
          firstMonsterTurn = false;
          isSixthDead = sixthMonsterUnit.TakeDamage(DontDestroy.party[0], DontDestroy.party[0].skillSet[CurrentMove]);
          takenDamage = sixthMonsterUnit.GetDamageValue(DontDestroy.party[0], DontDestroy.party[0].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[0].skillSet[CurrentMove], "Canvas3"));
          if(isSixthDead){
            sixthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        else if(isSecondSelected == true){
          secondMonsterTurn = false;
          isSixthDead = sixthMonsterUnit.TakeDamage(DontDestroy.party[1], DontDestroy.party[1].skillSet[CurrentMove]);
          takenDamage = sixthMonsterUnit.GetDamageValue(DontDestroy.party[1], DontDestroy.party[1].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[1].skillSet[CurrentMove], "Canvas3"));
          if(isSixthDead){
            sixthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        else if(isThirdSelected == true){
          thirdMonsterTurn = false;
          isSixthDead = sixthMonsterUnit.TakeDamage(DontDestroy.party[2], DontDestroy.party[2].skillSet[CurrentMove]);
          takenDamage = sixthMonsterUnit.GetDamageValue(DontDestroy.party[2], DontDestroy.party[2].skillSet[CurrentMove]);
          StartCoroutine(SpecialEffectCheckOne(DontDestroy.party[2].skillSet[CurrentMove], "Canvas3"));
          if(isSixthDead){
            sixthMonsterUnitGO.transform.gameObject.SetActive(false);
            state = BattleState.PLAYERTURN;
            playerHUD.nameText.text = "Player One Turn";
            CheckTurn();
          }
          if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
            state = BattleState.WIN;
            playerHUD.nameText.text = "Player One Win !";
          }
        }
        if(isSixthDead == false)
        {
          StartCoroutine(HitFlashEffect(sixthMonsterUnitGO));
          StartCoroutine(HitAnimation(sixthAnimator));
          damageText.text = takenDamage.ToString();
          GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
          floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas3").transform, false);
          state = BattleState.PLAYERTURN;
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          playerHUD.nameText.text = "Player One Turn";
          CheckTurn();
        }
      }
      else if (state == BattleState.ENEMYTURN && thirdAttackerTurn == true){
        audioOne.Play();
        playerHUD.HideText();
        playerHUD.EnemyThirdMoveSet();
        //thirdAttackerTurn = false;
        isFourthSelected = false;
        isFifthSelected = false;
        isSixthSelected = true;
      }

      //Heal
      else if (state == BattleState.ENEMYATTACK && isFourthSelected == true && fourthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        firstAttackerTurn = false;
        audioHeal.Play();
        sixthMonsterUnit.Support(sixthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(fourthMonsterUnitGO));
        StartCoroutine(HitAnimation(fourthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.fourthHpBar, DontDestroyTwo.party[0]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isFifthSelected == true && fifthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        secondAttackerTurn = false;
        audioHeal.Play();
        sixthMonsterUnit.Support(sixthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(fifthMonsterUnitGO));
        StartCoroutine(HitAnimation(fifthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.fifthHpBar, DontDestroyTwo.party[1]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isSixthSelected == true && sixthMonsterUnit.monster.skillSet[currentMove].skillName == "Heal"){
        thirdAttackerTurn = false;
        audioHeal.Play();
        sixthMonsterUnit.Support(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(HealthFlashEffect(sixthMonsterUnitGO));
        StartCoroutine(HitAnimation(sixthAnimator));
        state = BattleState.ENEMYTURN;
        playerHUD.UpdateHP(playerHUD.sixthHpBar, DontDestroyTwo.party[2]);
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }

      //Power Up
      else if (state == BattleState.ENEMYATTACK && isFourthSelected == true && fourthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        firstAttackerTurn = false;
        audioPowerUp.Play();
        sixthMonsterUnit.Support(sixthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(sixthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[currentMove], "Canvas3"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isFifthSelected == true && fifthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        secondAttackerTurn = false;
        audioPowerUp.Play();
        sixthMonsterUnit.Support(sixthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(sixthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[currentMove], "Canvas3"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
      else if (state == BattleState.ENEMYATTACK && isSixthSelected == true && sixthMonsterUnit.monster.skillSet[currentMove].skillName == "Power Up"){
        thirdAttackerTurn = false;
        audioPowerUp.Play();
        sixthMonsterUnit.Support(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[currentMove]);
        StartCoroutine(PowerFlashEffect(sixthMonsterUnitGO));
        StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[currentMove], "Canvas3"));
        state = BattleState.ENEMYTURN;
        playerHUD.nameText.text = "Player Two Turn";
        EnemyCheckTurn();
      }
    }

    public void ExecuteLaunchMoveTargetThree()
    {
      photonView.RPC("LaunchMoveTargetThree", RpcTarget.All, currentMove);
    }




    // Setup
    public void SetupBattle()
    {
      //Getting Party from another Scene
      // partyView = PhotonView.Find(18);
      // partyTwoView = PhotonView.Find(17);
      // playerPartyGO = GameObject.Find("Dont Destroy Party");
      // playerTwoPartyGO = GameObject.Find("Dont Destroy Party Two");
      // firstMonsterUnit = playerPartyGO.GetComponent<DontDestroy>();
      // secondMonsterUnit = playerPartyGO.GetComponent<DontDestroy>();
      // thirdMonsterUnit = playerPartyGO.GetComponent<DontDestroy>();
      // playerTwoUnit = playerTwoPartyGO.GetComponent<DontDestroyTwo>();

      //Getting BattleUnit from each GameObject
      firstUnit = firstMonsterUnitGO.GetComponent<BattleUnit>();
      secondUnit = secondMonsterUnitGO.GetComponent<BattleUnit>();
      thirdUnit = thirdMonsterUnitGO.GetComponent<BattleUnit>();
      fourthMonsterUnit = fourthMonsterUnitGO.GetComponent<BattleUnit>();
      fifthMonsterUnit = fifthMonsterUnitGO.GetComponent<BattleUnit>();
      sixthMonsterUnit = sixthMonsterUnitGO.GetComponent<BattleUnit>();

      //Reset Initial Stats
      DontDestroy.party[0].currentHealth = DontDestroy.party[0].maxHealth;
      DontDestroy.party[0].attack = DontDestroy.party[0].initialAttack;
      DontDestroy.party[0].defense = DontDestroy.party[0].initialDefense;
      DontDestroy.party[1].currentHealth = DontDestroy.party[1].maxHealth;
      DontDestroy.party[1].attack = DontDestroy.party[1].initialAttack;
      DontDestroy.party[1].defense = DontDestroy.party[1].initialDefense;
      DontDestroy.party[2].currentHealth = DontDestroy.party[2].maxHealth;
      DontDestroy.party[2].attack = DontDestroy.party[2].initialAttack;
      DontDestroy.party[2].defense = DontDestroy.party[2].initialDefense;
      DontDestroyTwo.party[0].currentHealth = DontDestroyTwo.party[0].maxHealth;
      DontDestroyTwo.party[0].attack = DontDestroyTwo.party[0].initialAttack;
      DontDestroyTwo.party[0].defense = DontDestroyTwo.party[0].initialDefense;
      DontDestroyTwo.party[1].currentHealth = DontDestroyTwo.party[1].maxHealth;
      DontDestroyTwo.party[1].attack = DontDestroyTwo.party[1].initialAttack;
      DontDestroyTwo.party[1].defense = DontDestroyTwo.party[1].initialDefense;
      DontDestroyTwo.party[2].currentHealth = DontDestroyTwo.party[2].maxHealth;
      DontDestroyTwo.party[2].attack = DontDestroyTwo.party[2].initialAttack;
      DontDestroyTwo.party[2].defense = DontDestroyTwo.party[2].initialDefense;


      //Setup Party
      firstUnit.monster = DontDestroy.party[0];
      secondUnit.monster = DontDestroy.party[1];
      thirdUnit.monster = DontDestroy.party[2];
      fourthMonsterUnit.monster = DontDestroyTwo.party[0];
      fifthMonsterUnit.monster = DontDestroyTwo.party[1];
      sixthMonsterUnit.monster = DontDestroyTwo.party[2];

      //Reset Debuff
      firstUnit.monster.debuff = new List<Debuff>();
      secondUnit.monster.debuff = new List<Debuff>();
      thirdUnit.monster.debuff = new List<Debuff>();
      fourthMonsterUnit.monster.debuff = new List<Debuff>();
      fifthMonsterUnit.monster.debuff = new List<Debuff>();
      sixthMonsterUnit.monster.debuff = new List<Debuff>();

      //Animator
      firstAnimator = firstMonsterUnitGO.GetComponent<Animator>();
      secondAnimator = secondMonsterUnitGO.GetComponent<Animator>();
      thirdAnimator = thirdMonsterUnitGO.GetComponent<Animator>();
      fourthAnimator = fourthMonsterUnitGO.GetComponent<Animator>();
      fifthAnimator = fifthMonsterUnitGO.GetComponent<Animator>();
      sixthAnimator = sixthMonsterUnitGO.GetComponent<Animator>();


      //Damage text
      damageText = floatingPoint.GetComponent<Text>();
      statReduceText = statReduce.GetComponent<Text>();
      burnEffectText = burnEffect.GetComponent<Text>();

      //Debuff Hide
      fourthMonsterUnit.HideDebuffSlot();
      fifthMonsterUnit.HideDebuffSlot();
      sixthMonsterUnit.HideDebuffSlot();
      firstUnit.HideDebuffSlot();
      secondUnit.HideDebuffSlot();
      thirdUnit.HideDebuffSlot();
    }

    public void SetupFirstAnimation()
    {
      if (firstUnit.monster.mName == "Kumamon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Patamon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Agumon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Guilmon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Kotemon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Monmon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Renamon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
      }
      else if (firstUnit.monster.mName == "Veemon"){
        firstAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
      }
    }

    public void SetupSecondAnimation()
    {
      if (secondUnit.monster.mName == "Kumamon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Patamon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Agumon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Guilmon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Kotemon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Monmon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Renamon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
      }
      else if (secondUnit.monster.mName == "Veemon"){
        secondAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
      }
    }

    public void SetupThirdAnimation()
    {
      if (thirdUnit.monster.mName == "Kumamon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Patamon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Agumon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Guilmon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Kotemon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Monmon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Renamon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
      }
      else if (thirdUnit.monster.mName == "Veemon"){
        thirdAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
      }
    }

    public void SetupFourthAnimation()
    {
      if (fourthMonsterUnit.monster.mName == "Kumamon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Patamon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Agumon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Guilmon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Kotemon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Monmon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Renamon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fourthMonsterUnit.monster.mName == "Veemon"){
        fourthAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
      }
    }

    public void SetupFifthAnimation()
    {
      if (fifthMonsterUnit.monster.mName == "Kumamon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Patamon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Agumon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Guilmon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Kotemon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Monmon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Renamon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
      }
      else if (fifthMonsterUnit.monster.mName == "Veemon"){
        fifthAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
      }
    }

    public void SetupSixthAnimation()
    {
      if (sixthMonsterUnit.monster.mName == "Kumamon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Kumamon/Image") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Patamon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Patamon/Idle") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Agumon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Agumon/Idle") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Guilmon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Guilmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Kotemon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Kotemon/InputAnim") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Monmon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Monmon/InputAnim") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Renamon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Renamon/InputAnim") as RuntimeAnimatorController;
      }
      else if (sixthMonsterUnit.monster.mName == "Veemon"){
        sixthAnimator.runtimeAnimatorController = Resources.Load("Animation/Veemon/Idle") as RuntimeAnimatorController;
      }
    }

    IEnumerator DebuffCheck(GameObject unitGO, BattleUnit unit, string theCanvas, float time, Animator theAnimator, bool isUnitDead, bool unitTurn)
    {
      if(unit.monster.debuff.Count == 1){
        if(unit.monster.debuff[0].debuffName == "Burn"){
          yield return new WaitForSeconds(time);
          isUnitDead = unit.BurnDamage();
          unit.GetBurnDamage();
          playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
          playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
          playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          if(isUnitDead){
            unitGO.transform.gameObject.SetActive(false);
            if(state == BattleState.PLAYERTURN && unitGO == firstMonsterUnitGO){
              firstMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == secondMonsterUnitGO){
              secondMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == thirdMonsterUnitGO){
              thirdMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fourthMonsterUnitGO){
              firstAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fifthMonsterUnitGO){
              secondAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == sixthMonsterUnitGO){
              thirdAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
          }
          if(isUnitDead == false){
            burnEffectText.text = unit.GetBurnDamage().ToString();
            GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
            floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
            StartCoroutine(HitAnimation(theAnimator));
            audioThree.Play();
          }
        }
      }
      else if(unit.monster.debuff.Count == 2){
        if(unit.monster.debuff[0].debuffName == "Burn"){
          yield return new WaitForSeconds(time);
          isUnitDead = unit.BurnDamage();
          unit.GetBurnDamage();
          playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
          playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
          playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          if(isUnitDead){
            unitGO.transform.gameObject.SetActive(false);
            if(state == BattleState.PLAYERTURN && unitGO == firstMonsterUnitGO){
              firstMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == secondMonsterUnitGO){
              secondMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == thirdMonsterUnitGO){
              thirdMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fourthMonsterUnitGO){
              firstAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fifthMonsterUnitGO){
              secondAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == sixthMonsterUnitGO){
              thirdAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
          }
          if(isUnitDead == false){
            burnEffectText.text = unit.GetBurnDamage().ToString();
            GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
            floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
            StartCoroutine(HitAnimation(theAnimator));
            audioThree.Play();
          }
        }
        if(unit.monster.debuff[1].debuffName == "Burn"){
          yield return new WaitForSeconds(time);
          isUnitDead = unit.BurnDamage();
          unit.GetBurnDamage();
          playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
          playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
          playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          if(isUnitDead){
            unitGO.transform.gameObject.SetActive(false);
            if(state == BattleState.PLAYERTURN && unitGO == firstMonsterUnitGO){
              firstMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == secondMonsterUnitGO){
              secondMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == thirdMonsterUnitGO){
              thirdMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fourthMonsterUnitGO){
              firstAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fifthMonsterUnitGO){
              secondAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == sixthMonsterUnitGO){
              thirdAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
          }
          if(isUnitDead == false){
            burnEffectText.text = unit.GetBurnDamage().ToString();
            GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
            floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
            StartCoroutine(HitAnimation(theAnimator));
            audioThree.Play();
          }
        }
      }
      else if(unit.monster.debuff.Count == 3){
        if(unit.monster.debuff[0].debuffName == "Burn"){
          yield return new WaitForSeconds(time);
          isUnitDead = unit.BurnDamage();
          unit.GetBurnDamage();
          playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
          playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
          playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          if(isUnitDead){
            unitGO.transform.gameObject.SetActive(false);
            if(state == BattleState.PLAYERTURN && unitGO == firstMonsterUnitGO){
              firstMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == secondMonsterUnitGO){
              secondMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == thirdMonsterUnitGO){
              thirdMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fourthMonsterUnitGO){
              firstAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fifthMonsterUnitGO){
              secondAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == sixthMonsterUnitGO){
              thirdAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
          }
          if(isUnitDead == false){
            burnEffectText.text = unit.GetBurnDamage().ToString();
            GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
            floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
            StartCoroutine(HitAnimation(theAnimator));
            audioThree.Play();
          }
        }
        if(unit.monster.debuff[1].debuffName == "Burn"){
          yield return new WaitForSeconds(time);
          isUnitDead = unit.BurnDamage();
          unit.GetBurnDamage();
          playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
          playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
          playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          if(isUnitDead){
            unitGO.transform.gameObject.SetActive(false);
            if(state == BattleState.PLAYERTURN && unitGO == firstMonsterUnitGO){
              firstMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == secondMonsterUnitGO){
              secondMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == thirdMonsterUnitGO){
              thirdMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fourthMonsterUnitGO){
              firstAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fifthMonsterUnitGO){
              secondAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == sixthMonsterUnitGO){
              thirdAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
          }
          if(isUnitDead == false){
            burnEffectText.text = unit.GetBurnDamage().ToString();
            GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
            floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
            StartCoroutine(HitAnimation(theAnimator));
            audioThree.Play();
          }
        }
        if(unit.monster.debuff[2].debuffName == "Burn"){
          yield return new WaitForSeconds(time);
          isUnitDead = unit.BurnDamage();
          unit.GetBurnDamage();
          playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
          playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
          playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
          playerHUD.UpdateHP(playerHUD.fourthHpBar, fourthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.fifthHpBar, fifthMonsterUnit.monster);
          playerHUD.UpdateHP(playerHUD.sixthHpBar, sixthMonsterUnit.monster);
          if(isUnitDead){
            unitGO.transform.gameObject.SetActive(false);
            if(state == BattleState.PLAYERTURN && unitGO == firstMonsterUnitGO){
              firstMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == secondMonsterUnitGO){
              secondMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.PLAYERTURN && unitGO == thirdMonsterUnitGO){
              thirdMonsterTurn = false;
              CheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fourthMonsterUnitGO){
              firstAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == fifthMonsterUnitGO){
              secondAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
            else if(state == BattleState.ENEMYTURN && unitGO == sixthMonsterUnitGO){
              thirdAttackerTurn = false;
              EnemyCheckTurn();
              CheckDie();
            }
          }
          if(isUnitDead == false){
            burnEffectText.text = unit.GetBurnDamage().ToString();
            GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
            floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
            StartCoroutine(HitAnimation(theAnimator));
            audioThree.Play();
          }
        }
      }
    }

    IEnumerator PlayerCheckTurn()
    {
      yield return new WaitForSeconds(2.2f);
      CheckTurn();
    }

    IEnumerator PlayerTwoCheckTurn()
    {
      yield return new WaitForSeconds(2.2f);
      EnemyCheckTurn();
    }

    IEnumerator SpecialEffectCheckOne(Skillset selectedMove, string theCanvas)
    {
      if(selectedMove.skillName == "Air Shot"){
        yield return new WaitForSeconds(0.4f);
        statReduceText.text = "Defense - 2";
        GameObject floatingStatReduce = Instantiate(statReduce, transform.position, Quaternion.identity) as GameObject;
        floatingStatReduce.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
      }
      else if(selectedMove.skillName == "Baby Flame"){
        yield return new WaitForSeconds(0.4f);
        burnEffectText.text = "Burn";
        GameObject floatingBurnEffect = Instantiate(burnEffect, transform.position, Quaternion.identity) as GameObject;
        floatingBurnEffect.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
      }
      else if(selectedMove.skillName == "Power Up"){
        yield return new WaitForSeconds(0.4f);
        statReduceText.text = "Attack + 2";
        GameObject floatingStatReduce = Instantiate(statReduce, transform.position, Quaternion.identity) as GameObject;
        floatingStatReduce.transform.SetParent (GameObject.FindGameObjectWithTag(theCanvas).transform, false);
      }
    }

    IEnumerator HitFlashEffect(GameObject monsterUnit)
    {
      monsterUnit.GetComponent<Image>().material = flashEffect;
      yield return new WaitForSeconds(0.2f);
      monsterUnit.GetComponent<Image>().material = null;
    }

    IEnumerator HealthFlashEffect(GameObject monsterUnit)
    {
      monsterUnit.GetComponent<Image>().material = healFlashEffect;
      yield return new WaitForSeconds(0.2f);
      monsterUnit.GetComponent<Image>().material = null;
    }

    IEnumerator PowerFlashEffect(GameObject monsterUnit)
    {
      monsterUnit.GetComponent<Image>().material = powerUpFlashEffect;
      yield return new WaitForSeconds(0.2f);
      monsterUnit.GetComponent<Image>().material = null;
    }

    IEnumerator HitAnimation(Animator theAnimator)
    {
      theAnimator.SetBool("WasAttacked", true);
      yield return new WaitForSeconds(0.5f);
      theAnimator.SetBool("WasAttacked", false);
    }

    IEnumerator EnemyAttack()
    {
      playerHUD.nameText.text = "Player Two Turn";
      int whoAttack, whoGetAttacked;
      for (int i = 0; i <= 2; i++){
        yield return new WaitForSeconds(0.8f);
        whoAttack = Random.Range(1, 4);
        whoGetAttacked = Random.Range(1, 4);
        StartCoroutine(RandomTarget(whoAttack, whoGetAttacked, 0.5f));
        if(isFirstDead && isSecondDead && isThirdDead){
          playerHUD.nameText.text = "You Lose, Learn more from Monsterpedia";
          state = BattleState.LOSE;
        }
        else if(firstAttackerTurn == false && secondAttackerTurn == false && thirdAttackerTurn == false){
          PlayerTurn();
        }
        else if(isFourthDead == true && isFifthDead == true  && isSixthDead == true){
          state = BattleState.WIN;
          playerHUD.nameText.text = "Player One Win !";
        }
      }
    }

    IEnumerator PlayMusic()
    {
      yield return new WaitForSeconds(0.5f);
      audioFour.Play();
      firstMonsterUnitGO.GetComponent<Image>().material = null;
      secondMonsterUnitGO.GetComponent<Image>().material = null;
      thirdMonsterUnitGO.GetComponent<Image>().material = null;
      fourthMonsterUnitGO.GetComponent<Image>().material = null;
      fifthMonsterUnitGO.GetComponent<Image>().material = null;
      sixthMonsterUnitGO.GetComponent<Image>().material = null;
    }

    IEnumerator RandomTarget(int attacker, int defender, float waitTime)
    {
      int randomMove;
      if(attacker == 1 && defender == 1 && firstAttackerTurn == true && isFirstDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isFirstDead = firstUnit.TakeDamage(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = firstUnit.GetDamageValue(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("FirstUnit").transform, false);
        StartCoroutine(HitFlashEffect(firstMonsterUnitGO));
        StartCoroutine(HitAnimation(firstAnimator));
        StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[randomMove], "FirstUnit"));
        playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
        if(isFirstDead){
          firstMonsterUnitGO.transform.gameObject.SetActive(false);
          isFirstDead = true;
        }
        firstAttackerTurn = false;
      }
      else if(attacker == 1 && defender == 2 && firstAttackerTurn == true && isSecondDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isSecondDead = secondUnit.TakeDamage(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = secondUnit.GetDamageValue(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("SecondUnit").transform, false);
        StartCoroutine(HitFlashEffect(secondMonsterUnitGO));
        StartCoroutine(HitAnimation(secondAnimator));
        StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[randomMove], "SecondUnit"));
        playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
        if(isSecondDead){
          secondMonsterUnitGO.transform.gameObject.SetActive(false);
          isSecondDead = true;
        }
        firstAttackerTurn = false;
      }
      else if(attacker == 1 && defender == 3 && firstAttackerTurn == true && isThirdDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isThirdDead = thirdUnit.TakeDamage(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = thirdUnit.GetDamageValue(fourthMonsterUnit.monster, fourthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("ThirdUnit").transform, false);
        StartCoroutine(HitFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(HitAnimation(thirdAnimator));
        StartCoroutine(SpecialEffectCheckOne(fourthMonsterUnit.monster.skillSet[randomMove], "ThirdUnit"));
        playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
        if(isThirdDead){
          thirdMonsterUnitGO.transform.gameObject.SetActive(false);
          isThirdDead = true;
        }
        firstAttackerTurn = false;
      }
      else if(attacker == 2 && defender == 1 && secondAttackerTurn == true && isFirstDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isFirstDead = firstUnit.TakeDamage(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = firstUnit.GetDamageValue(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("FirstUnit").transform, false);
        StartCoroutine(HitFlashEffect(firstMonsterUnitGO));
        StartCoroutine(HitAnimation(firstAnimator));
        StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[randomMove], "FirstUnit"));
        playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
        if(isFirstDead){
          firstMonsterUnitGO.transform.gameObject.SetActive(false);
          isFifthDead = true;
        }
        secondAttackerTurn = false;
      }
      else if(attacker == 2 && defender == 2 && secondAttackerTurn == true && isSecondDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isSecondDead = secondUnit.TakeDamage(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = secondUnit.GetDamageValue(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("SecondUnit").transform, false);
        StartCoroutine(HitFlashEffect(secondMonsterUnitGO));
        StartCoroutine(HitAnimation(secondAnimator));
        StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[randomMove], "SecondUnit"));
        playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
        if(isSecondDead){
          secondMonsterUnitGO.transform.gameObject.SetActive(false);
          isSecondDead = true;
        }
        secondAttackerTurn = false;
      }
      else if(attacker == 2 && defender == 3 && secondAttackerTurn == true && isThirdDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isThirdDead = thirdUnit.TakeDamage(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = thirdUnit.GetDamageValue(fifthMonsterUnit.monster, fifthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("ThirdUnit").transform, false);
        StartCoroutine(HitFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(HitAnimation(thirdAnimator));
        StartCoroutine(SpecialEffectCheckOne(fifthMonsterUnit.monster.skillSet[randomMove], "ThirdUnit"));
        playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
        if(isThirdDead){
          thirdMonsterUnitGO.transform.gameObject.SetActive(false);
          isThirdDead = true;
        }
        secondAttackerTurn = false;
      }
      else if(attacker == 3 && defender == 1 && thirdAttackerTurn == true && isFirstDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isFirstDead = firstUnit.TakeDamage(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = firstUnit.GetDamageValue(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("FirstUnit").transform, false);
        StartCoroutine(HitFlashEffect(firstMonsterUnitGO));
        StartCoroutine(HitAnimation(firstAnimator));
        StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[randomMove], "FirstUnit"));
        playerHUD.UpdateHP(playerHUD.firstHpBar, firstUnit.monster);
        if(isFirstDead){
          firstMonsterUnitGO.transform.gameObject.SetActive(false);
          isFifthDead = true;
        }
        thirdAttackerTurn = false;
      }
      else if(attacker == 3 && defender == 2 && thirdAttackerTurn == true && isSecondDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isSecondDead = secondUnit.TakeDamage(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = secondUnit.GetDamageValue(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("SecondUnit").transform, false);
        StartCoroutine(HitFlashEffect(secondMonsterUnitGO));
        StartCoroutine(HitAnimation(secondAnimator));
        StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[randomMove], "SecondUnit"));
        playerHUD.UpdateHP(playerHUD.secondHpBar, secondUnit.monster);
        if(isSecondDead){secondMonsterUnitGO.transform.gameObject.SetActive(false);}
        thirdAttackerTurn = false;
      }
      else if(attacker == 3 && defender == 3 && thirdAttackerTurn == true && isThirdDead == false){
        randomMove = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        audioTwo.Play();
        isThirdDead = thirdUnit.TakeDamage(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[randomMove]);
        takenDamage = thirdUnit.GetDamageValue(sixthMonsterUnit.monster, sixthMonsterUnit.monster.skillSet[randomMove]);
        damageText.text = takenDamage.ToString();
        GameObject floatingDamage = Instantiate(floatingPoint, transform.position, Quaternion.identity) as GameObject;
        floatingDamage.transform.SetParent (GameObject.FindGameObjectWithTag("ThirdUnit").transform, false);
        StartCoroutine(HitFlashEffect(thirdMonsterUnitGO));
        StartCoroutine(HitAnimation(thirdAnimator));
        StartCoroutine(SpecialEffectCheckOne(sixthMonsterUnit.monster.skillSet[randomMove], "ThirdUnit"));
        playerHUD.UpdateHP(playerHUD.thirdHpBar, thirdUnit.monster);
        if(isThirdDead){
          thirdMonsterUnitGO.transform.gameObject.SetActive(false);
          isThirdDead = true;
        }
        thirdAttackerTurn = false;
      }
      else {PlayerTurn();}
    }

}
