using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LV
{
    FIRST, SECOND, THIRD
}
public class SpawnManager : Singleton<SpawnManager>
{
    public Vector3 LastKnowPosition;
    public List<GameObject> objToActivate;
    public List<GameObject> objToActivateInv;
    public LV activeLV;
    public bool isLvChanged;
    public List<GameObject> objDominance;
    public bool canActivateDominace;
    public bool StopRoomChage;
    public bool itemActive;
    public GameObject closeDominance;
    public bool abilityRunning;
    public GameObject textFeedback;
    public bool isFeedBackTextRunning;
    public bool DominanceIsActive;
    public bool isLv2;
    private void Start()
    {
        EventManager.ReactivateThings += OnReactivation;
        EventManager.DeactivateThings += OnDeactivation;

        EventManager.DominanceEnd += OnEndDominance;
        EventManager.DominanceStart += OnStartDominance;
    }


    private void OnDisable()
    {
        EventManager.ReactivateThings -= OnReactivation;
        EventManager.DeactivateThings -= OnDeactivation;

        EventManager.DominanceEnd -= OnEndDominance;
        EventManager.DominanceStart -= OnStartDominance;
    }

    private void OnStartDominance()
    {
        DominanceIsActive = true;
        textFeedback.SetActive(false);
        foreach (GameObject obj in objDominance)
        {
            obj.SetActive(false);
        }
        closeDominance.SetActive(true);
        if (!isLv2)
            SceneManager.LoadScene("DominanceVFX", LoadSceneMode.Additive);
        else
            SceneManager.LoadScene("DominanceVFX2", LoadSceneMode.Additive);

    }
    public IEnumerator FeedBackTextCoroutine()
    {

        textFeedback.SetActive(true);
        yield return new WaitForSeconds(2f);
        textFeedback.SetActive(false);
        isFeedBackTextRunning = false;
    }
    private void OnEndDominance()
    {
        DominanceIsActive = false;
        foreach (GameObject obj in objDominance)
        {
            obj.SetActive(true);
        }
        closeDominance.SetActive(false);
        if (!isLv2)
            SceneManager.UnloadSceneAsync("DominanceVFX");
        else
            SceneManager.UnloadSceneAsync("DominanceVFX2");


    }

    private void ResetEndLevel()
    {
        LastKnowPosition = Vector3.zero;
        objToActivate.Clear();
        objToActivateInv.Clear();
    }


    public void OnReactivation()
    {
        foreach (GameObject obj in objToActivateInv)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objToActivate)
        {
            obj.SetActive(true);
        }


    }
    public void OnDeactivation()
    {
        foreach (GameObject obj in objToActivateInv)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objToActivate)
        {
            obj.SetActive(false);
        }

    }
}
