using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    public BattleSystem sistemBattle;
    public Monster monster;
    public bool isPlayerOne;
    Skillset skillset;
    public Debuff burn;

    [Header("Debuff Image")]
    public Image debuffOne;
    public Image debuffTwo;
    public Image debuffThree;

    public void HideDebuffSlot()
    {
      debuffOne.transform.gameObject.SetActive(false);
      debuffTwo.transform.gameObject.SetActive(false);
      debuffThree.transform.gameObject.SetActive(false);
    }

    public bool TakeDamage(Monster attacker, Skillset moveSelected)
    {
      monster.currentHealth -= (int)(attacker.attack*moveSelected.damage/monster.defense*0.9);
      if(moveSelected.skillName == "Air Shot"){
        monster.defense -= 1;
        if(monster.defense <= 1){monster.defense = 1;}
      }

      else if(moveSelected.skillName == "Baby Flame"){
        if(monster.debuff.Count <= 2){
          monster.debuff.Add(burn);
          if(monster.debuff.Count == 1){
            debuffOne.sprite = burn.debuffImage;
            debuffOne.transform.gameObject.SetActive(true);
          }
          else if(monster.debuff.Count == 2){
            debuffTwo.sprite = burn.debuffImage;
            debuffTwo.transform.gameObject.SetActive(true);
          }
          else if(monster.debuff.Count == 3){
            debuffThree.sprite = burn.debuffImage;
            debuffThree.transform.gameObject.SetActive(true);
          }
        }
      }

      if(monster.currentHealth <= 0){return true;}
      else{return false;}
    }

    public void Support(Monster supportTarget, Skillset supportMove)
    {
      //Heal
      if(supportMove.skillName == "Heal"){
        supportTarget.currentHealth += (int)(supportTarget.maxHealth*0.15);
        if(supportTarget.currentHealth >= supportTarget.maxHealth){supportTarget.currentHealth = supportTarget.maxHealth;}
      }
      else if(supportMove.skillName == "Heal" && sistemBattle.totalTurn >= 4 && sistemBattle.totalTurn < 8){
        supportTarget.currentHealth += (int)(supportTarget.maxHealth*0.20);
        if(supportTarget.currentHealth >= supportTarget.maxHealth){supportTarget.currentHealth = supportTarget.maxHealth;}
      }
      else if(supportMove.skillName == "Heal" && sistemBattle.totalTurn >= 8 && sistemBattle.totalTurn < 12){
        supportTarget.currentHealth += (int)(supportTarget.maxHealth*0.25);
        if(supportTarget.currentHealth >= supportTarget.maxHealth){supportTarget.currentHealth = supportTarget.maxHealth;}
      }
      else if(supportMove.skillName == "Heal" && sistemBattle.totalTurn >= 12){
        supportTarget.currentHealth += (int)(supportTarget.maxHealth*0.30);
        if(supportTarget.currentHealth >= supportTarget.maxHealth){supportTarget.currentHealth = supportTarget.maxHealth;}
      }
      //Power Up
      else if(supportMove.skillName == "Power Up"){
        supportTarget.attack += 2;
      }
    }

    public bool BurnDamage()
    {
      monster.currentHealth -= (int)(monster.maxHealth*0.1 - monster.defense*0.2);
      if(monster.currentHealth <= 0){return true;}
      else{return false;}
    }

    public int GetBurnDamage()
    {
      int burnDamage = (int)(monster.maxHealth*0.1 - monster.defense*0.2);
      return burnDamage;
    }

    public int GetDamageValue(Monster attacker, Skillset moveSelected)
    {
      int damageValue = (int)(attacker.attack*moveSelected.damage/monster.defense*0.9);
      return damageValue;
    }


}
