using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Skill")]
public class Skillset : ScriptableObject
{
  public string skillName;
  public Sprite skillImage;

  [Header("Effect")]
  public int damage;
  public int manaCost;
  public string specialEffect;
}
