using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [Header("Battle HUD UI")]
    public Text nameText, firstMove, secondMove, thirdMove, fourthMove;
    public HealthBar firstHpBar, secondHpBar, thirdHpBar, fourthHpBar, fifthHpBar, sixthHpBar;
    public BattleSystem battleSystem;
    Monster monster;


    public void SetPlayerData()
    {
      firstHpBar.SetHP((float)DontDestroy.party[0].currentHealth/DontDestroy.party[0].maxHealth);
      secondHpBar.SetHP((float)DontDestroy.party[1].currentHealth/DontDestroy.party[1].maxHealth);
      thirdHpBar.SetHP((float)DontDestroy.party[2].currentHealth/DontDestroy.party[2].maxHealth);
    }

    public void SetEnemyData(Monster monster)
    {
      fourthHpBar.SetHP((float)monster.currentHealth/monster.maxHealth);
      fifthHpBar.SetHP((float)monster.currentHealth/monster.maxHealth);
      sixthHpBar.SetHP((float)monster.currentHealth/monster.maxHealth);
    }

    public void UpdateHP(HealthBar healthBar, Monster monster)
    {
      healthBar.SetHP((float) monster.currentHealth/monster.maxHealth);
    }

    public void HideText()
    {
      nameText.transform.gameObject.SetActive(false);
      firstMove.transform.gameObject.SetActive(true);
      secondMove.transform.gameObject.SetActive(true);
      thirdMove.transform.gameObject.SetActive(true);
      fourthMove.transform.gameObject.SetActive(true);
    }

    public void ShowText()
    {
      nameText.transform.gameObject.SetActive(true);
    }

    public void HideMoveSet()
    {
      firstMove.transform.gameObject.SetActive(false);
      secondMove.transform.gameObject.SetActive(false);
      thirdMove.transform.gameObject.SetActive(false);
      fourthMove.transform.gameObject.SetActive(false);
    }

    public void OpenFirstMoveSet()
    {
      firstMove.text = DontDestroy.party[0].skillSet[0].skillName;
      secondMove.text = DontDestroy.party[0].skillSet[1].skillName;
      thirdMove.text = DontDestroy.party[0].skillSet[2].skillName;
      fourthMove.text = DontDestroy.party[0].skillSet[3].skillName;
    }

    public void OpenSecondMoveSet()
    {
      firstMove.text = DontDestroy.party[1].skillSet[0].skillName;
      secondMove.text = DontDestroy.party[1].skillSet[1].skillName;
      thirdMove.text = DontDestroy.party[1].skillSet[2].skillName;
      fourthMove.text = DontDestroy.party[1].skillSet[3].skillName;
    }

    public void OpenThirdMoveSet()
    {
      firstMove.text = DontDestroy.party[2].skillSet[0].skillName;
      secondMove.text = DontDestroy.party[2].skillSet[1].skillName;
      thirdMove.text = DontDestroy.party[2].skillSet[2].skillName;
      fourthMove.text = DontDestroy.party[2].skillSet[3].skillName;
    }

    public void EnemyFirstMoveSet()
    {
      firstMove.text = battleSystem.fourthMonsterUnit.monster.skillSet[0].skillName;
      secondMove.text = battleSystem.fourthMonsterUnit.monster.skillSet[1].skillName;
      thirdMove.text = battleSystem.fourthMonsterUnit.monster.skillSet[2].skillName;
      fourthMove.text = battleSystem.fourthMonsterUnit.monster.skillSet[3].skillName;
    }

    public void EnemySecondMoveSet()
    {
      firstMove.text = battleSystem.fifthMonsterUnit.monster.skillSet[0].skillName;
      secondMove.text = battleSystem.fifthMonsterUnit.monster.skillSet[1].skillName;
      thirdMove.text = battleSystem.fifthMonsterUnit.monster.skillSet[2].skillName;
      fourthMove.text = battleSystem.fifthMonsterUnit.monster.skillSet[3].skillName;
    }

    public void EnemyThirdMoveSet()
    {
      firstMove.text = battleSystem.sixthMonsterUnit.monster.skillSet[0].skillName;
      secondMove.text = battleSystem.sixthMonsterUnit.monster.skillSet[1].skillName;
      thirdMove.text = battleSystem.sixthMonsterUnit.monster.skillSet[2].skillName;
      fourthMove.text = battleSystem.sixthMonsterUnit.monster.skillSet[3].skillName;
    }

}
