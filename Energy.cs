using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public List<Image> myImage;
    public TextMeshProUGUI maxText;
    public TextMeshProUGUI curText;
    public int maxNum;
    public int curNum;

    void Start()
    {
        loadEnergy();
    }

    private void Update()
    {
        
    }

    public void changeEnergyCur(int i)
    {
        curNum += i;
        loadEnergy();
    }

    public void changeEnergyMax(int i)
    {
        maxNum += i;
        if(maxNum>10)
        {
            maxNum = 10;
        }
        loadEnergy();
    }

    public void TurnOn()
    {
        curNum = maxNum;
        loadEnergy();
    }
    public void loadEnergy()
    {
        maxText.text = maxNum.ToString();
        curText.text = curNum.ToString();
        for (int i = 0; i < 10; ++i)
        {
            myImage[i].sprite = Resources.Load("unLocked", typeof(Sprite)) as Sprite;     // Image/pic 在 Assets/Resources/目录下 
        }
        for (int i = 0; i < maxNum; ++i)
        {
            myImage[i].sprite = Resources.Load("runOut", typeof(Sprite)) as Sprite;     // Image/pic 在 Assets/Resources/目录下 
        }
        for (int i = 0; i < curNum; ++i)
        {
            myImage[i].sprite = Resources.Load("full", typeof(Sprite)) as Sprite;     // Image/pic 在 Assets/Resources/目录下 
        }
    }
}
