using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGameMode : MonoBehaviour
{
    public List<GameObject> GameBones = new List<GameObject>();
    public GameObject SnapLocation;

    private List<Rigidbody> _rb = new List<Rigidbody>();
    private List<BoxCollider> _bcollider = new List<BoxCollider>();
    private List<GameObject> _handGrab = new List<GameObject>();
    private List<GameObject> _snapInteractor = new List<GameObject>();

    void Start() {
        for (int i = 0; i < GameBones.Count; i++)
        {
            _rb.Add(GameBones[i].GetComponent<Rigidbody>());
            _bcollider.Add(GameBones[i].GetComponent<BoxCollider>());
            _handGrab.Add(GameBones[i].transform.GetChild(0).gameObject);
            _snapInteractor.Add(GameBones[i].transform.GetChild(1).gameObject);
        }
    }


    public void EnterGameMode()
    {
        SnapLocation.SetActive(false);
        for (int i = 0; i < GameBones.Count; i++)
        {
            _bcollider[i].isTrigger = false;
            _handGrab[i].SetActive(false);
            _snapInteractor[i].SetActive(false);
            _rb[i].velocity = Vector3.zero;
            _rb[i].useGravity = true;
            _rb[i].isKinematic = false;     
        }
    }

    public void ExitGameMode()
    {
        for (int i = 0; i < GameBones.Count; i++)
        {
            _rb[i].isKinematic = true;
            _rb[i].useGravity = false;
            _bcollider[i].isTrigger = true;
            _handGrab[i].SetActive(true);
            _snapInteractor[i].SetActive(true);
            
        }
        SnapLocation.SetActive(true);
        Debug.Log("hello");
    }
}
