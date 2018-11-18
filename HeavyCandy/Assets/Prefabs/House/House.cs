using UnityEngine;

public class House : MonoBehaviour {

    public bool hasPerformingBand;
    public FactionLogic.Genre genre;
    int candyCount;

	Color colorStart = Color.red;
	Color colorEnd = Color.green;
	float colorDuration = 1.0f;
	Renderer colorRend;
	float colorSlowDown = 1.0f;
	Color initialColor;
	float initialColorDuration;

	private GameObject rippleA;
	private GameObject rippleB;

	public float vibPower = 0.7f; // Power of vibration
	public float vibDuration = 1.0f; // Duration of vibration
	public float vibSlowDown = 1.0f; // Set to 1 vibrate vibDuration seconds. Set to 0 to never stop vibrating
	[SerializeField]
	private bool shouldVib = false; // Should it vibrate
	Vector3 initialPosition; // Position before vibration
	float initialVibDuration; // vibDuration value before vibrating


	void Start () {
        hasPerformingBand = false;
        candyCount = 0;

        rippleA = (GameObject)transform.Find("RippleA").gameObject;
        rippleB = (GameObject)transform.Find("RippleB").gameObject;

        colorRend = GetComponent<Renderer>();
		initialColor = colorRend.material.color;
		colorSlowDown = vibSlowDown;
		colorDuration = vibDuration;
		initialColorDuration = colorDuration;

		initialPosition = transform.localPosition;
		initialVibDuration = vibDuration;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.V))
		{
			shouldVib = true;
		}

		if (shouldVib)
		{
			HouseColor();
			Vibrate();
		}
	}

    public void PlayMusic(FactionLogic.Genre genre) {
        this.genre = genre;
        hasPerformingBand = true;
        shouldVib = true;
        rippleA.SetActive(true);
        rippleB.SetActive(true);
    }

    public void StopMusic() {
        hasPerformingBand = false;
        shouldVib = false;
        transform.localPosition = initialPosition;
        colorRend.material.color = initialColor;
        rippleA.SetActive(false);
        rippleB.SetActive(false);
    }

    public void IncreaseCandyCount(int amount = 1) {
        candyCount += amount;
    }

    public void DecreaseCandyCount(int amount = 1) {
        candyCount -= amount;
    }

    public bool HasCandy() {
        return (candyCount != 0);
    }

    public void ConfiscateCandy() {
        candyCount = 0;
    }

	void HouseColor()
	{
		if (colorDuration > 0)
		{
			float colorLerp = Mathf.PingPong(Time.time, colorDuration) / colorDuration;
			colorRend.material.color = Color.Lerp(colorStart, colorEnd, colorLerp);
			colorDuration -= Time.deltaTime * colorSlowDown;
		}
		else
		{
			colorDuration = initialColorDuration;
			colorRend.material.color = initialColor;
		}
	}

	void Vibrate () // Vibration
	{
		if (shouldVib)
		{
			if (vibDuration > 0)
			{
                transform.localPosition = initialPosition + Random.insideUnitSphere * vibPower;
				vibDuration -= Time.deltaTime * vibSlowDown;
			}
			else
			{
				vibDuration = initialVibDuration;
                transform.localPosition = initialPosition;
			}
		}
	}
}
