using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    //player health
    public int healthPoint;
    public int maxHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    //player keys
    public int numOfKey;
    public int playerMaxKeyLimit;
    public Image[] keys;
    public Sprite haveKeySlot;
    public Sprite emptyKeySlot;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealthBar();
        PlayerKeyInventory();
    }

    private void PlayerHealthBar()
    {
        if (healthPoint > maxHearts)
        {
            healthPoint = maxHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < healthPoint)
            {
                hearts[i].sprite = fullHeart;

            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < maxHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void PlayerKeyInventory()
    {
        if (numOfKey > playerMaxKeyLimit)
        {
            numOfKey = playerMaxKeyLimit;
            //to do .... pops up dialog prevent player taking more keys
            // write sth --- not finish
        }

        for (int k = 0; k < keys.Length; k++)
        {
            if (k < numOfKey)
            {
                keys[k].sprite = haveKeySlot;

            }
            else
            {
                keys[k].sprite = emptyKeySlot;
            }


            if (k < playerMaxKeyLimit)
            {
                keys[k].enabled = true;
            }
            else
            {
                keys[k].enabled = false;
            }
        }
    }
}
