using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSlot : MonoBehaviour
{
    public string Name = null;
    public int saltLevel = 0;
    public bool wantsSalt = false;
    bool lookingGood = false;
    float timeHolder = 0f;
    public float reductionTime = 7.5f;
    int saltNeeded = 50;

    public GameObject indicator = null;

    Color green = Color.green;
    Color yellow = Color.yellow;
    Color red = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        //if(!wantsSalt)
        //{
        //    indicator.GetComponent<UnityEngine.UI.Image>().color = green;
        //}
        //else
        //{
        //    indicator.GetComponent<UnityEngine.UI.Image>().color = yellow;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(name != "Trash")
        {
            if (!GameManager.Instance.running)
            {

                indicator.GetComponent<UnityEngine.UI.Image>().color = Color.clear;
                saltLevel = 0;
            }
            else
            {
                timeHolder += Time.deltaTime;
                if (timeHolder > reductionTime)
                {
                    if (saltLevel > 0)
                    {
                        saltLevel -= 10;
                    }
                    timeHolder = 0;
                }
                if (!wantsSalt)
                {
                    if (saltLevel > 0)
                    {
                        indicator.GetComponent<UnityEngine.UI.Image>().color = red;
                        lookingGood = false;
                    }
                    else
                    {
                        indicator.GetComponent<UnityEngine.UI.Image>().color = green;
                        lookingGood = true;
                    }
                }
                else
                {
                    if (saltLevel < saltNeeded)
                    {
                        indicator.GetComponent<UnityEngine.UI.Image>().color = yellow;
                        lookingGood = false;
                    }
                    else if (saltLevel > saltNeeded)
                    {
                        indicator.GetComponent<UnityEngine.UI.Image>().color = red;
                        lookingGood = false;
                    }
                    else
                    {
                        indicator.GetComponent<UnityEngine.UI.Image>().color = green;
                        lookingGood = true;
                    }
                }
            }
        }

    }

    public bool LookingGood()
    {
        return lookingGood;
    }
}
