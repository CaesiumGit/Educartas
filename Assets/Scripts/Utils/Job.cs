using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Job
{
    /// <summary>
    /// Event called when job is complete or killed.
    /// </summary>
    public event System.Action JobComplete;

    private bool _running;
    /// <summary>
    /// Determines is the job is still running.
    /// </summary>
    public bool Running { get { return _running; } }

    private bool _paused;
    /// <summary>
    /// Determines if the job is paused.
    /// </summary>
    public bool Paused { get { return _paused; } }

    //The coroutine belonging to the job.
    private IEnumerator _coroutine;
    private List<Job> _childrenJobs;

    public Job(IEnumerator coroutine)
    {
        _coroutine = coroutine;
    }

    #region Start
    /// <summary>
    /// Starts the main job as a coroutine.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartAsCoroutine()
    {
        _running = true;
        yield return JobManager.Instance.StartCoroutine(doCoroutine());
    }

    /// <summary>
    /// Starts the main job.
    /// </summary>
    public void Start()
    {
        _running = true;
        JobManager.Instance.StartCoroutine(doCoroutine());
    } 
    #endregion

    #region Children
    /// <summary>
    /// Creates and add a child job that will be started when the main job is finished.
    /// </summary>
    public Job CreateChildJob(IEnumerator coroutine)
    {
        Job child = new Job(coroutine);
        this.AddChildJob(child);
        return child;
    }

    /// <summary>
    /// Adds a new child job.
    /// </summary>
    public void AddChildJob(Job childJob)
    {
        if (_childrenJobs == null)
            _childrenJobs = new List<Job>();

        _childrenJobs.Add(childJob);
    } 
    #endregion

    #region Flow control
    /// <summary>
    /// Kill the job and his children.
    /// </summary>
    public void Kill()
    {
        _running = false;
        _paused = false;
    }

    /// <summary>
    /// Pauses the job and his children.
    /// </summary>
    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    } 
    #endregion

    private IEnumerator doCoroutine()
    {
        while (_running)
        {
            if (_paused)
            {
                yield return null;
            }
            else
            {
                if (_coroutine.MoveNext())
                {
                    yield return _coroutine.Current;
                }
                    //if the main job is finished
                else
                {
                    //start the child jobs
                    if (_childrenJobs != null)
                    {
                        foreach (Job child in _childrenJobs)
                        {
                            yield return JobManager.Instance.StartCoroutine(child.StartAsCoroutine());
                        }
                    }

                    _running = false;
                }
            }
        }

        //when job is finished or killed, the event is called.
        if (JobComplete != null)
            JobComplete();
    }
}
