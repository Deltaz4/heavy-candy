using UnityEngine;

public class House : MonoBehaviour {

    public bool hasPerformingBand;
    public FactionLogic.Genre genre;

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


	void Start ()
	{
        hasPerformingBand = false;

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

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			shouldVib = true;
		}

		if (shouldVib)
		{
			HouseColor();
			Vibrate();
		}
		else
		{
			rippleA.SetActive(false);
			rippleB.SetActive(false);
		}
	}

    public void PlayMusic(FactionLogic.Genre genre) {
        this.genre = genre;
        hasPerformingBand = true;
        shouldVib = true;
    }

    public void StopMusic() {
        hasPerformingBand = false;
        shouldVib = false;
    }


	public void ToggleVib ()
	{
		shouldVib = !shouldVib;
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
			rippleA.SetActive(true);
			rippleB.SetActive(true);

			if (vibDuration > 0)
			{
                transform.localPosition = initialPosition + Random.insideUnitSphere * vibPower;
				vibDuration -= Time.deltaTime * vibSlowDown;
			}
			else
			{
				shouldVib = false;
				vibDuration = initialVibDuration;
                transform.localPosition = initialPosition;
			}
		}
	}
}
