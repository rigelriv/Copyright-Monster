using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDisplay : MonoBehaviour
{
  public Skillset skill;
  public Monster panelMonster;
  public string sName;
  public Text description;
  public Text status;
  public Image image;

  void Start()
  {
    image.sprite = skill.skillImage;
    sName = skill.skillName;
    description.text = "Skill Name : " + sName + "\nDamage : " + skill.damage + "  Mana : " + skill.manaCost + "\nSpecial Effect : " + skill.specialEffect;
    status.text = "Status : " + "\nAttack : " + panelMonster.attack + "\nDefense : " + panelMonster.defense + "\nHealth : " + panelMonster.maxHealth;
  }
}
