using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public GameObject playerBody;
    public int bodySelected = 0;

    [SerializeField]
    Image playerIconCanvas;

    [SerializeField]
    Text playerNameCanvas;

    private void Start()
    {
        SwitchPlayerBody();
    }
    public void SwitchPlayerBody()
    {
        int count = 0;

        foreach (Transform body in playerBody.transform)
        {
            if (bodySelected == count)
            {
                body.gameObject.SetActive(true);
                BodyData tempBodyData = body.GetComponent<BodyConfig>().GetData();
                Updatecanvas(tempBodyData);
            }
            else
            {
                body.gameObject.SetActive(false);
            }
            count++;
        }
    }

    void Updatecanvas(BodyData bd)
    {
        playerIconCanvas.sprite = bd.characterIcon;
        playerNameCanvas.text = bd.nationName;
    }

    public void BtnNext()
    {
        bodySelected++;
        if(bodySelected > playerBody.transform.childCount -1)
        {
            bodySelected = 0;
        }
        SwitchPlayerBody();
    }

    public void BtnPrev()
    {
        bodySelected--;
        if (bodySelected < 0)
        {
            bodySelected = playerBody.transform.childCount - 1;
        }
        SwitchPlayerBody();
    }

}
