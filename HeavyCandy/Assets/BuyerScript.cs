using UnityEngine;

public class BuyerScript : MonoBehaviour {

	public float walkSpeed = 5.0f;
	public float walkDuration;
	[HideInInspector]
	public float walkSlowDown = 1.0f;
	float walkRandomX;
	float walkRandomZ;
	[SerializeField]
	bool shouldWalkRandom;

    private GameObject sprite;

    void Start ()
	{
		walkRandomX = Random.Range(-1, 2);
		walkRandomZ = Random.Range(-1, 2);
		walkDuration = Random.Range(1, 6);
		shouldWalkRandom = true;
        sprite = transform.Find("Sprite").gameObject;
    }
	
	void Update ()
	{
		WalkRandom();
    }

    private void LateUpdate()
    {
        sprite.transform.forward = Camera.main.transform.forward;
    }


    void SetWalkDuration ()
	{
		walkDuration = Random.Range(1, 6);
	}

	void WalkRandom ()
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
				shouldWalkRandom = false;
				walkRandomX = Random.Range(-1, 2);
				walkRandomZ = Random.Range(-1, 2);
				SetWalkDuration();
			}
			shouldWalkRandom = true;
		}
	}
}
