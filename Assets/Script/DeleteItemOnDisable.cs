using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItemOnDisable : MonoBehaviour
{
    //public string tagToDelete;
    
    public void DeleteItems(string tagToDelete)
    {
        GameObject[] itemsToDelete; itemsToDelete = GameObject.FindGameObjectsWithTag(tagToDelete);
        if(itemsToDelete.Length!=0)
        {
            foreach (GameObject itemDel in itemsToDelete)
            {
                Destroy(itemDel);
            }
        }
        //Debug.Log("HOLA "+tagToDelete+"-"+itemsToDelete.Length);
    }
}
