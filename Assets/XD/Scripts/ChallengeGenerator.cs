using UnityEngine;
using System.Collections;
using XD.Scripts;

public class ChallengeGenerator : MonoBehaviour {
    public GameObject firstChallenge;

    ChallengeInfo lastChallenge;
	// Use this for initialization
	void Start () {
        //generate first 3 random challenges
        if (!firstChallenge)
            Debug.Log("Please assign all the variables");
        int RandomInt = Random.Range(0, SystemParam.Instance.GetGuanKaCount());
        GameObject initObj;
        lastChallenge = firstChallenge.GetComponent<ChallengeInfo>();
        for (int i = 0; i < 3; i++) {
            RandomInt = Random.Range(0, SystemParam.Instance.GetGuanKaCount());
            initObj = Instantiate(SystemParam.Instance.GetGuanKa(RandomInt), lastChallenge.GetEndPos(), Quaternion.identity) as GameObject;
            initObj.transform.parent = transform;
            lastChallenge = initObj.transform.GetComponent<ChallengeInfo>();
        }
	}

    public Vector3 GetlastChallengeStartPos()
    {
        return lastChallenge.GetStartPos();
    }

    public void RestToStartPos()
    {
        Transform currentChild;
        for (int i = 0; i < transform.childCount; ++i)
        {
            currentChild = transform.GetChild(i);
            currentChild.position = currentChild.GetComponent<ChallengeInfo>().GetStartPos();
        }
    }

    // Update is called once per frame
	void Update () {
        //Generate and Destroy random challenges and scroll
        int RandomInt;
        GameObject initObj;
        Transform currentChild;
        for (int i = 0; i < transform.childCount; ++i)
        {
            currentChild = transform.GetChild(i);
            currentChild.position -= Vector3.right * SystemParam.Instance.MoveSpeed * Time.deltaTime;
            if (currentChild.GetComponent<ChallengeInfo>().GetEndPos().x <= -15.0f)
            Destroy(currentChild.gameObject);
            if (lastChallenge && lastChallenge.GetEndPos().x <= 15.0f) {
                RandomInt = Random.Range(0,SystemParam.Instance.GetGuanKaCount());
                initObj = Instantiate(SystemParam.Instance.GetGuanKa(RandomInt), lastChallenge.GetEndPos(), Quaternion.identity) as GameObject;
                initObj.transform.parent = transform;
                lastChallenge = initObj.transform.GetComponent<ChallengeInfo>();
                return;
            }
          }
	}
}
