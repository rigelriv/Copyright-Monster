using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Create New Monster")]
public class Monster : ScriptableObject
{
    public string mName;
    public Sprite monsterImage;
    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    [Header("Base Stat")]
    public int maxHealth;
    public int currentHealth;
    public int mana;
    public int attack;
    public int initialAttack;
    public int defense;
    public int initialDefense;
    public int speed;

    public List<Skillset> skillSet = new List<Skillset>();

    public List<Skillset> learnableSkill = new List<Skillset>();

    public List<Debuff> debuff = new List<Debuff>();

}
