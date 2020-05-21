using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour
{

    public Police police;
    public PoliceController policeController;
    public Transform policeSpawnPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeployPoliceSquad(FactionLogic.Genre genre, House targetHouse)
    {

        Police deployedSquad = (Police)Instantiate(police,
            policeSpawnPoint.position,
            policeSpawnPoint.rotation);

        deployedSquad.transform.parent = gameObject.transform;
        deployedSquad.setTargetGenre(genre);
        deployedSquad.SetHouse(targetHouse);
    }

    public void DestinationReached(FactionLogic.Genre genre, bool candyFound)
    {
        policeController.DestinationReached(genre, candyFound);
    }
}
