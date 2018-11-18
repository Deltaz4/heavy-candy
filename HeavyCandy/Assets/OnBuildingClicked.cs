using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnBuildingClicked : MonoBehaviour
{
    public GameObject Menu;
    public Image Candy;
    public Image Gig;

    public Vector3 ObjectPos;
    public Vector2 ViewPortPoint;
    public Vector2 MouseClickPos;
    public int Offset;

    public int Money;
    public int CandyCost;
    public int GigCost;

    public bool MouseClicked;
    public bool MouseClickedOnObject;

    public bool CandyIsClicked;
    public bool GigIsClicked;

    public Sprite CandyGoState;
    public Sprite CandyGoStateHighLighted;
    public Sprite CandyAbortState;
    public Sprite CandyAbortStateHighlighted;

    public Sprite GigGoState;
    public Sprite GigGoStateHighLighted;
    public Sprite GigAbortState;
    public Sprite GigAbortStateHighlighted;

    // Use this for initialization
    void Start()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClickPos = Camera.main.WorldToScreenPoint(Input.mousePosition);

            MouseClicked = true;

            StartCoroutine(Wait());
            if (MouseClicked && !MouseClickedOnObject)
            {
                if ((MouseClickPos.x > ViewPortPoint.x + Offset || MouseClickPos.x < ViewPortPoint.x - Offset) &&
                    (MouseClickPos.y > ViewPortPoint.y + Offset || MouseClickPos.y < ViewPortPoint.y - Offset))

                Menu.SetActive(false);
                StartCoroutine(ResetVariables());
            }
            else
            {
                StartCoroutine(ResetVariables());
            }
        }
    }

    private void OnMouseDown()
    {
        if (!Menu.activeSelf)
        {
            MouseClickedOnObject = true;
            print("!!");
            ObjectPos = (gameObject.transform.position);
            Menu.SetActive(true);
            ViewPortPoint = Camera.main.WorldToScreenPoint(new Vector3(ObjectPos.x + Offset, ObjectPos.y, ObjectPos.z + Offset));
            Menu.transform.position = ViewPortPoint;
        }
        else
        {
            return;
        }
    }

    public void OnCandyClick ()
    {
        print("CLICKED");
        Menu.SetActive(true);
        StartCoroutine(ResetVariables());

        if ((Candy.sprite = CandyGoState) && Money >= CandyCost)
        {
            Candy.sprite = CandyAbortState;
            Money = Money - CandyCost;
        }
        if (Candy.sprite = CandyAbortState)
        {
            Candy.sprite = CandyGoState;
        }
        else
        {
            print("You don't have enough money, fool!");
        }
    }

    public void OnGigClick ()
    {
        Menu.SetActive(true);
        StartCoroutine(ResetVariables());

        if ((Gig.sprite = GigGoState) && Money >= GigCost)
        {
            Gig.sprite = GigAbortState;
            Money = Money - GigCost;
        }
        if (Gig.sprite = GigAbortState)
        {
            Gig.sprite = GigGoState;
        }
        else
        {
            print("You don't have enough money, fool!");
        }
    }

    IEnumerator ResetVariables()
    {
        yield return new WaitForSeconds(.3f);
        MouseClicked = false;
        MouseClickedOnObject = false;
    }

    IEnumerator Wait ()
    {
        yield return new WaitForSeconds(.1f);
    }
}