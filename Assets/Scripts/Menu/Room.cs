using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public Text Name;

    public void JoinRoom()
    {
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomList(Name.text);
    }

}
