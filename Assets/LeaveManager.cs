using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveManager : Photon.PunBehaviour{
     void ButtonPressed() 
    {

        PhotonNetwork.LeaveRoom();
        
        
    
    }

    public override void OnLeftRoom()
    {

        PhotonNetwork.LoadLevel(0);


    }



}
