using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnBuildingClicked : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Cross;
    public Sprite Candy;
    public Sprite Gig;

    public Vector3 ObjectPos;
    public Vector2 ViewPortPoint;
    public Vector2 MouseClickPos;
    public int Offset;

    public int Money;
    public int CandyCost;
    public int GigCost;

    public bool MouseClicked;
    public bool MouseClickedOnObject;

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

            StartCoroutine(ResetVariables());
        }
    }

    private void OnMouseDown()
    {
        if (!Menu.activeSelf)
        {
            MouseClickedOnObject = true;
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

    public void OnCandyClick()
    {
        Debug.Log("button clicked!");
        Menu.SetActive(true);
        MouseClickedOnObject = true;
        print("MouseClickedOnObject: " + MouseClickedOnObject);
        StartCoroutine(ResetVariables());

        if ((Candy = CandyGoState) && Money >= CandyCost)
        {
            Candy = CandyAbortState;
            Money = Money - CandyCost;
        }
        if (Candy = CandyAbortState)
        {
            Candy = CandyGoState;
        }
        else
        {
            print("You don't have enough money, fool!");
        }
    }

    public void OnGigClick()
    {
        Menu.SetActive(true);
        MouseClickedOnObject = true;
        StartCoroutine(ResetVariables());

        if ((Gig = GigGoState) && Money >= GigCost)
        {
            Gig = GigAbortState;
            Money = Money - GigCost;
        }
        if (Gig = GigAbortState)
        {
            Gig = GigGoState;
        }
        else
        {
            print("You don't have enough money, fool!");
        }
    }

    public void OnCrossClick()
    {
        if (Menu.activeSelf)
        {
            Menu.SetActive(false);
        }
    }

    IEnumerator ResetVariables()
    {
        yield return new WaitForSeconds(.3f);
        MouseClicked = false;
        MouseClickedOnObject = false;
    }
}