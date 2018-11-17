using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour {

    public Police police;
    public PoliceController policeController;
    public Transform policeSpawnPoint;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void DeployPoliceSquad(FactionLogic.Genre genre) {

        Police deployedSquad = (Police) Instantiate(police, 
            policeSpawnPoint.position, 
            policeSpawnPoint.rotation);

        // deployedSquad.SetHouse(); TODO implement choice of house
        // deployedSquad.SetDestination(); TODO ..or a destination for it to patrol!
        deployedSquad.transform.parent = gameObject.transform;
        police.setTargetGenre(genre);
    }

    public void DestinationReached(FactionLogic.Genre genre, bool candyFound) {
        policeController.DestinationReached(genre, candyFound);
    }
}
