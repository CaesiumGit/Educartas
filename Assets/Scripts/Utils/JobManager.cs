using UnityEngine;
using System.Collections;

public class JobManager : MonoBehaviour
{
    private static JobManager _instance;
    public static JobManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject jobObject = GameObject.Find("JobObject");
                if (jobObject == null)
                {
                    jobObject = new GameObject("JobObject");
                    jobObject.AddComponent<JobManager>();
                }
                _instance = jobObject.GetComponent<JobManager>();
            }

            return _instance;
        }
    }
}
