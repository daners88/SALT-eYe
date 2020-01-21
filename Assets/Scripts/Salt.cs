using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : MonoBehaviour
{
    public int slotNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.running)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FoodSlot temp = other.gameObject.GetComponent<FoodSlot>();
        if (temp != null)
        {
            temp.saltLevel += 10;
            GameManager.Instance.DecrementSlot(slotNum);
            Destroy(this.gameObject);
        }
    }
}
