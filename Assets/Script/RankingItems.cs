using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankingItems : MonoBehaviour
{
    public GameObject item;
    public GameObject container;
    public int option = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        //colocamos un if que permita seleccionar que debe realizar el script
        switch (option)
        {
            case 1:
                //Ranking
                break;
            case 2:
                //Progreso
                break;
            case 3:
                //Logro
                break;
            default:
                break;
        }
        for (int i = 0; i < 10; i++)
        {
            GameObject card = Instantiate(item) as GameObject;
            card.transform.SetParent(container.transform, false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
