using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Unit {

    FactionLogic.Genre targetGenre;

    public void setTargetGenre(FactionLogic.Genre genre) {
        targetGenre = genre;
    }

    void Update () {
        if (base.house != null && AtDestination()) {
            if (targetGenre == house.genre) {
                bool candyFound = house.HasCandy();
                house.ConfiscateCandy();
                PoliceStation policeStation = (PoliceStation)transform.parent.GetComponent(typeof(PoliceStation));
                policeStation.DestinationReached(targetGenre, candyFound);
            }
            Destroy(gameObject);
        }
    }
}
