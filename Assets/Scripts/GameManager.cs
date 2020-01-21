using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawnSlots = null;
    public List<GameObject> foodSlots = null;
    public GameObject saltPrefab = null;
    public float delayTime = 5.0f;
    List<Vector2> slotsFilled = new List<Vector2>(); 
    System.Random rand = new System.Random();
    float timeHolder = 0f;
    float difficultyTimeCheck = 0.0f;
    public float spawnSpeed = 2.0f;

    public float difficultyMultiplier = 1.0f;

    public bool running = false;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            slotsFilled.Add(new Vector2(0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(running)
        {
            difficultyTimeCheck += Time.deltaTime;
            if (difficultyTimeCheck >= 60f)
            {
                ReduceDifficulty();
                difficultyTimeCheck = 0f;
            }
            bool harder = true;
            foreach (var slot in foodSlots)
            {
                harder = slot.GetComponent<FoodSlot>().LookingGood();
                if(!harder)
                {
                    break;
                }
            }
            if (harder)
            {
                IncreaseDifficulty();
                difficultyTimeCheck = 0f;
            }
            timeHolder += Time.deltaTime;
            if (timeHolder >= spawnSpeed)
            {
                timeHolder = 0f;
                int slotToTry = rand.Next(0, spawnSlots.Count);
                if (slotsFilled[slotToTry].x == 0 || Time.time - slotsFilled[slotToTry].y >= delayTime)
                {
                    var spawnSlot = spawnSlots[slotToTry];
                    Vector3 pos = new Vector3();
                    pos = spawnSlot.transform.position;
                    var salt = Instantiate(saltPrefab, pos, Quaternion.identity, spawnSlot.transform);
                    Vector3 scaleAdjustment = new Vector3(salt.transform.localScale.x - (salt.transform.localScale.x * difficultyMultiplier), salt.transform.localScale.y - (salt.transform.localScale.y * difficultyMultiplier), salt.transform.localScale.z - (salt.transform.localScale.z * difficultyMultiplier));
                    salt.transform.localScale -= scaleAdjustment;
                    salt.GetComponent<Salt>().slotNum = slotToTry;
                    Vector2 temp = slotsFilled[slotToTry];
                    temp.x += 1;
                    temp.y = Time.time;
                    slotsFilled[slotToTry] = temp;
                }
            }
        }
        else
        {
            difficultyMultiplier = 1.0f;
        }
    }

    public void DecrementSlot(int slot)
    {
        Vector2 temp = slotsFilled[slot];
        temp.x -= 1;
        slotsFilled[slot] = temp;
    }

    void IncreaseDifficulty()
    {
        difficultyMultiplier *= 0.75f;
        foreach(var slot in foodSlots)
        {
            slot.GetComponent<FoodSlot>().saltLevel = 0;
        }
    }

    void ReduceDifficulty()
    {
        difficultyMultiplier *= 1.25f;
        foreach (var slot in foodSlots)
        {
            slot.GetComponent<FoodSlot>().saltLevel = 0;
        }
    }
}
