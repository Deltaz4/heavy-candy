using UnityEngine;

public class House : MonoBehaviour {

	Color colorStart = Color.red;
	Color colorEnd = Color.green;
	float colorDuration = 1.0f;
	Renderer colorRend;
	float colorSlowDown = 1.0f;
	Color initialColor;
	float initialColorDuration;

	[HideInInspector]
	public GameObject rippleA;
	[HideInInspector]
	public GameObject rippleB;

	public float vibPower = 0.7f; // Power of vibration
	public float vibDuration = 1.0f; // Duration of vibration
	public Transform house;
	public float vibSlowDown = 1.0f; // Set to 1 vibrate vibDuration seconds. Set to 0 to never stop vibrating
	[SerializeField]
	private bool shouldVib = false; // Should it vibrate
	Vector3 initialPosition; // Position before vibration
	float initialVibDuration; // vibDuration value before vibrating


	void Start ()
	{
		colorRend = GetComponent<Renderer>();
		initialColor = colorRend.material.color;
		colorSlowDown = vibSlowDown;
		colorDuration = vibDuration;
		initialColorDuration = colorDuration;

		initialPosition = house.localPosition;
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
				house.localPosition = initialPosition + Random.insideUnitSphere * vibPower;
				vibDuration -= Time.deltaTime * vibSlowDown;
			}
			else
			{
				shouldVib = false;
				vibDuration = initialVibDuration;
				house.localPosition = initialPosition;
			}
		}
	}
}
