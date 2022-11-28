using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
  public Monster monster;
  //public Text description;
  public string monsterName;
  public Image image;
  public int monsterHp;
  public int monsterMp;
  public int monsterAttack;
  public int monsterDefense;
  public int monsterSpeed;
    // Start is called before the first frame update
    void Start()
    {
      monsterName = monster.mName;
      monsterHp = monster.maxHealth;
      monsterMp = monster.mana;
      monsterAttack = monster.attack;
      monsterDefense = monster.defense;
      monsterSpeed = monster.speed;
      image.sprite = monster.monsterImage;
      //description.text ="Name = " + monsterName + "\n HP = " + monsterHp + "\n MP = " + monsterMp +"\n Attack = " + monsterAttack + "\n Defense = " + monsterDefense + "\n Speed = " + monsterSpeed;
    }

}
