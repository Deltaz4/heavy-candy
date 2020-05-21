using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    private Dictionary<int, Band> bands = new Dictionary<int, Band>();

    private Band selectedBand;
    private bool candyDeliverySelected;
    public Headquarters hq;

    private List<Button> selectionButtons = new List<Button>();

    public int candySellingPrice = 10;
    public float cameraSpeed = 4;
    private int startingBands = 9;
    public int capacityOfDeliverer = 10; // Amount of candy the CandyDelivery will send
    public GameObject bandPrefab;
    public GameObject candyDelivererPrefab;

    public int money;

    // Use this for initialization
    void Start()
    {
        CreateInitialBands();
        BindSelectionButtons();
        candyDeliverySelected = false;
    }

    void CreateInitialBands()
    {
        Vector3 spawnLocation = hq.transform.position;
        for (int i = 1; i <= startingBands; ++i)
        {
            GameObject newBand = Instantiate(bandPrefab, spawnLocation - 10 * Vector3.right, Quaternion.identity);
            bands.Add(i, newBand.GetComponent<Band>());
            if (i <= 3)
            {
                newBand.GetComponent<Band>().SetGenre(FactionLogic.Genre.HIP_HOP);
            }
            else if (i >= 4 && i <= 6)
            {
                newBand.GetComponent<Band>().SetGenre(FactionLogic.Genre.METAL);
            }
            else
            {
                newBand.GetComponent<Band>().SetGenre(FactionLogic.Genre.OPERA);
            }
            newBand.SetActive(false);
        }
    }


    void BindSelectionButtons()
    {
        for (int i = 1; i <= 9; ++i)
        {
            Button button = transform.Find("Canvas/ActionButtons/Selection" + i).GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
            // The following block is stupid.
            // Was previously button.onClick.AddListener(() => ChangeSelection(i)
            // But then, i = 10 for all 
            switch (i)
            {
                case 1: button.onClick.AddListener(() => ChangeSelection(1)); break;
                case 2: button.onClick.AddListener(() => ChangeSelection(2)); break;
                case 3: button.onClick.AddListener(() => ChangeSelection(3)); break;
                case 4: button.onClick.AddListener(() => ChangeSelection(4)); break;
                case 5: button.onClick.AddListener(() => ChangeSelection(5)); break;
                case 6: button.onClick.AddListener(() => ChangeSelection(6)); break;
                case 7: button.onClick.AddListener(() => ChangeSelection(7)); break;
                case 8: button.onClick.AddListener(() => ChangeSelection(8)); break;
                case 9: button.onClick.AddListener(() => ChangeSelection(9)); break;
            }
            selectionButtons.Add(button);
        }
    }

    void ChangeSelection(int bandNumber)
    {
        selectedBand = bands[bandNumber];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
        UpdateMouse();
        UpdateSelection();
    }

    void UpdateCamera()
    {
        Vector3 cameraMovement = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            cameraMovement.z += cameraSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            cameraMovement.x -= cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            cameraMovement.z -= cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            cameraMovement.x += cameraSpeed;
        }
        transform.Translate(cameraMovement);
    }

    void UpdateMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                House target = hit.transform.gameObject.GetComponent<House>();
                if (target && candyDeliverySelected && selectedBand == null && target.hasPerformingBand)
                {
                    GameObject newObject = Instantiate(candyDelivererPrefab,
                        hq.transform.position - 10 * Vector3.right, Quaternion.identity);
                    CandyDelivery deployedDeliverer = newObject.GetComponent<CandyDelivery>();

                    deployedDeliverer.transform.parent = gameObject.transform;
                    deployedDeliverer.SetHouse(target);
                    deployedDeliverer.SetCandyCount(capacityOfDeliverer);
                }
                else if (selectedBand && target && !target.hasPerformingBand)
                {
                    if (!selectedBand.playing)
                    {
                        selectedBand.gameObject.SetActive(true);
                    }
                    if (selectedBand.playing)
                    {
                        selectedBand.house.StopMusic();
                    }
                    selectedBand.SetHouse(target);
                }
            }
        }
    }

    void UpdateSelection()
    {
        if (bands.ContainsKey(1) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedBand = bands[1];
        }
        if (bands.ContainsKey(2) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedBand = bands[2];
        }
        if (bands.ContainsKey(3) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedBand = bands[3];
        }
        if (bands.ContainsKey(4) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedBand = bands[4];
        }
        if (bands.ContainsKey(5) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedBand = bands[5];
        }
        if (bands.ContainsKey(6) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectedBand = bands[6];
        }
        if (bands.ContainsKey(7) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectedBand = bands[7];
        }
        if (bands.ContainsKey(8) && Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectedBand = bands[8];
        }
        if (bands.ContainsKey(9) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            selectedBand = bands[9];
        }
        if (bands.ContainsKey(0) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedBand = bands[0];
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            selectedBand = null; // Great solution
            candyDeliverySelected = true;
        }
    }
}