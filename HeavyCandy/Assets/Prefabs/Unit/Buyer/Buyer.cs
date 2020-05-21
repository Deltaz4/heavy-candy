using UnityEngine;

public class Buyer : Unit
{

    public float walkSpeed = 5.0f;
    public float walkDuration;
    [HideInInspector]
    public float walkSlowDown = 1.0f;
    float walkRandomX;
    float walkRandomZ;
    [SerializeField]
    bool shouldWalkRandom;

    Player player;

    private GameObject sprite;

    void Start()
    {
        walkRandomX = Random.Range(-1, 2);
        walkRandomZ = Random.Range(-1, 2);
        walkDuration = Random.Range(1, 6);
        shouldWalkRandom = true;
        sprite = transform.Find("Sprite").gameObject;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        WalkRandom();

        if (!shouldWalkRandom && base.house != null && AtDestination())
        {
            // bool candyFound = house.HasCandy();
            house.DecreaseCandyCount(1);
            player.money += player.candySellingPrice;
            HouseController houseController = (HouseController)transform.parent.GetComponent(typeof(HouseController));
            houseController.DestinationReached(this);
            Destroy(gameObject);
        }
    }

    public bool IsWalkingRandomly()
    {
        return shouldWalkRandom;
    }

    override public void SetHouse(House house)
    {
        base.SetHouse(house);
        shouldWalkRandom = false;
    }

    private void LateUpdate()
    {
        sprite.transform.forward = Camera.main.transform.forward;
    }


    void SetWalkDuration()
    {
        walkDuration = Random.Range(1, 6);
    }

    void WalkRandom()
    {
        if (shouldWalkRandom)
        {
            if (walkDuration > 0)
            {
                transform.Translate(new Vector3(walkRandomX, 0, walkRandomZ) * walkSpeed * Time.deltaTime);
                walkDuration -= Time.deltaTime * walkSlowDown;
            }
            else if (walkDuration <= 0)
            {
                walkRandomX = Random.Range(-1, 2);
                walkRandomZ = Random.Range(-1, 2);
                SetWalkDuration();
            }
        }
    }
}
