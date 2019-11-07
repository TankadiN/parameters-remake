using System.Collections;
using UnityEngine;

public class LevelDataTest : MonoBehaviour
{
    public static LevelData LD;

    public PlaceInfo[] Places;
    public GameObject[] targetplace;

    //private int i = 0;

    private IEnumerator GetThePlaces()
    {
        targetplace = GameObject.FindGameObjectsWithTag("Place");
        Places = new PlaceInfo[targetplace.Length];
        yield return new WaitForSeconds(2f);
        GetData();
    }

    public void GetData()
    {
        /*foreach (GameObject o in targetplace)
        {
            Places[i].rectX = o.GetComponent<RectTransform>().rect.x;
            Places[i].rectY = o.GetComponent<RectTransform>().rect.y;
            Places[i].rectWidth = o.GetComponent<RectTransform>().rect.width;
            Places[i].rectHeight = o.GetComponent<RectTransform>().rect.height;
            if (o.GetComponent<Place>())
            {
                Places[i].PlaceType = PlaceInfo.Type.Place;

                Places[i].SilverLock = o.GetComponent<Place>().SilverLock;
                Places[i].GoldLock = o.GetComponent<Place>().GoldLock;

                Places[i].ProgressNeeded = o.GetComponent<Place>().ProgressNeeded;
                Places[i].EnergyNeeded = o.GetComponent<Place>().EnergyNeeded;

                Places[i].MinExpOngoing = o.GetComponent<Place>().MinExpOngoing;
                Places[i].MaxExpOngoing = o.GetComponent<Place>().MaxExpOngoing;

                Places[i].MinGoldOngoing = o.GetComponent<Place>().MinGoldOngoing;
                Places[i].MaxGoldOngoing = o.GetComponent<Place>().MaxGoldOngoing;

                Places[i].MinGoldCompleted = o.GetComponent<Place>().MinGoldCompleted;
                Places[i].MaxGoldCompleted = o.GetComponent<Place>().MaxGoldCompleted;
                Debug.Log("Places Fetched");
            }
            i++;
        }
        i = 0;*/
        for(int i = 0; i < targetplace.Length; i++)
        {
            Places[i].rect.x = targetplace[i].GetComponent<RectTransform>().rect.x;
            Places[i].rect.y = targetplace[i].GetComponent<RectTransform>().rect.y;
            Places[i].rect.width= targetplace[i].GetComponent<RectTransform>().rect.width;
            Places[i].rect.height = targetplace[i].GetComponent<RectTransform>().rect.height;
            if (targetplace[i].GetComponent<Place>())
            {
                Places[i].PlaceType = PlaceInfo.Type.Place;

                Places[i].SilverLock = targetplace[i].GetComponent<Place>().SilverLock;
                Places[i].GoldLock = targetplace[i].GetComponent<Place>().GoldLock;

                Places[i].ProgressNeeded = targetplace[i].GetComponent<Place>().ProgressNeeded;
                Places[i].EnergyNeeded = targetplace[i].GetComponent<Place>().EnergyNeeded;

                Places[i].MinExpOngoing = targetplace[i].GetComponent<Place>().MinExpOngoing;
                Places[i].MaxExpOngoing = targetplace[i].GetComponent<Place>().MaxExpOngoing;

                Places[i].MinGoldOngoing = targetplace[i].GetComponent<Place>().MinGoldOngoing;
                Places[i].MaxGoldOngoing = targetplace[i].GetComponent<Place>().MaxGoldOngoing;

                Places[i].MinGoldCompleted = targetplace[i].GetComponent<Place>().MinGoldCompleted;
                Places[i].MaxGoldCompleted = targetplace[i].GetComponent<Place>().MaxGoldCompleted;
                Debug.Log("Places Fetched");
            }
        }
        Debug.Log("Done Succesfully.");
    }

    public void GetPlacesButton()
    {
        StartCoroutine(GetThePlaces());
    }
}