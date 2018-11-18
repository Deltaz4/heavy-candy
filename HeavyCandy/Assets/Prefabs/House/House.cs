using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    public bool hasPerformingBand;
    private Band playingBand;
    public FactionLogic.Genre genre;
    int candyCount;

	public List<AudioClip> songList = new List<AudioClip>();
	private AudioSource audio;

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
    Vector3 basePosition;
    float initialVibDuration; // vibDuration value before vibrating

    public float setupTime = 5.0f;
    private bool isSettingUp = false;
    private bool isTearingDown = false;
    private float tearTimeLeft;
    public float riseHeight = 4.5f;
    private float initHeight = 1.0f;
    private float maxHeight = 10.0f;

    void Start () {
        hasPerformingBand = false;
        candyCount = 0;

		audio = GetComponent<AudioSource>();

        rippleA = (GameObject)transform.Find("RippleA").gameObject;
        rippleB = (GameObject)transform.Find("RippleB").gameObject;
        rippleA.SetActive(false);
        rippleB.SetActive(false);

        colorRend = GetComponent<Renderer>();
		initialColor = colorRend.material.color;
		colorSlowDown = vibSlowDown;
		colorDuration = vibDuration;
		initialColorDuration = colorDuration;

		initialPosition = transform.localPosition;
        basePosition = initialPosition;
        initialVibDuration = vibDuration;

        HouseController houseController = gameObject.GetComponentInParent<HouseController>();
        houseController.AddHouse(this);
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

        if (isSettingUp)
        {
            tearTimeLeft += Time.deltaTime;
            if (tearTimeLeft >= setupTime)
            {
                tearTimeLeft = setupTime;
                hasPerformingBand = true;
                shouldVib = true;
                rippleA.SetActive(true);
                rippleB.SetActive(true);
                isSettingUp = false;
            }
            scaleHeight();
        }
        else if (isTearingDown)
        {
            tearTimeLeft -= Time.deltaTime;
            if (tearTimeLeft <= 0.0f)
            {
                tearTimeLeft = 0.0f;
                hasPerformingBand = false;
                shouldVib = false;
                transform.localPosition = initialPosition;
                colorRend.material.color = initialColor;
                rippleA.SetActive(false);
                rippleB.SetActive(false);
                playingBand.gameObject.SetActive(true);
                playingBand.StopPlaying();
                playingBand.setDestination();
                isTearingDown = false;
            }
            scaleHeight();
        }
	}

    private void scaleHeight()
    {
        basePosition.y = initialPosition.y + riseHeight * tearTimeLeft / setupTime / 2.0f;
        transform.localPosition = basePosition;
        Debug.Log(transform.localPosition);
        transform.localScale = new Vector3(transform.localScale.x, (maxHeight - initHeight) * tearTimeLeft / setupTime + initHeight, transform.localScale.z);
    }

    public void PlayMusic(Band band)
    {
        this.genre = band.genre;
        playingBand = band;
        isSettingUp = true;
        tearTimeLeft = 0.0f;
		if (genre == FactionLogic.Genre.HIP_HOP)
		{
			audio.clip = songList[0];
		}
		else if (genre == FactionLogic.Genre.METAL)
		{
			audio.clip = songList[1];
		}
		else if (genre == FactionLogic.Genre.OPERA)
		{
			audio.clip = songList[2];
		}

		audio.Play();
    }

    public void StopMusic()
    {
        isSettingUp = false;
        if (playingBand && playingBand.playing)
        {
            isTearingDown = true;
        }

		audio.Stop();
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
                transform.localPosition = basePosition + Random.insideUnitSphere * vibPower;
				vibDuration -= Time.deltaTime * vibSlowDown;
			}
			else
			{
				vibDuration = initialVibDuration;
                transform.localPosition = basePosition;
			}
		}
	}
}
