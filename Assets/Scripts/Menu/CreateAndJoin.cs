using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class CreateAndJoin : MonoBehaviourPunCallbacks
{

    public TMP_InputField input_Create;
    public TMP_InputField input_Join;


    public void CreateRoom()
    {

        PhotonNetwork.CreateRoom(input_Create.text);

    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(input_Join.text);

    }

    public void JoinRoomList(string RoomName)
    {

        PhotonNetwork.JoinRoom(RoomName);

    }


    public override void OnJoinedRoom()
    {
        
        print(PhotonNetwork.CountOfPlayersInRooms);
        PhotonNetwork.LoadLevel("Game");
    }

}
