using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{

    public Text MyMessage;



    void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();
    }

}
