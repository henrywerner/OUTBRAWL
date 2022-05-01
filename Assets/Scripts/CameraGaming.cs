using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraGaming : MonoBehaviour
{
    public float shot1Duration, shot2Duration, shot3Duration;
    public float shot1Distance, shot2Distance, shot3Distance;
    public Transform shot1Subject, shot2Subject, shot3Subject;
    [SerializeField] private Camera _cam1, _cam2, _cam3;
    private void Awake()
    {
        _cam1.gameObject.SetActive(true);
    }

    void Start()
    {
        StartCoroutine(CameraMovement());
    }

    private IEnumerator CameraMovement()
    {
        float duration = shot1Duration;
        float time = 0;
        
        float startValue = _cam1.transform.localPosition.x;
        float endValue = startValue + shot1Distance;
        
        while (time < duration)
        {
            var pos = _cam1.transform.position; // just so I don't have to write the whole thing everytime
            
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            
            pos = new Vector3(Mathf.Lerp(startValue, endValue, t), pos.y, pos.z);
            _cam1.transform.position = pos;
            
            _cam1.transform.LookAt(shot1Subject);

            time += Time.deltaTime;
            yield return null;
        }

        _cam1.transform.position = new Vector3(endValue, _cam1.transform.position.y, _cam1.transform.position.y);
        
        // cut to cam 2
        _cam2.gameObject.SetActive(true);
        _cam1.gameObject.SetActive(false);
        
        time = 0; // reset time
        
        // set values to next camera
        duration = shot2Duration;
        startValue = _cam2.transform.position.x;
        endValue = startValue + shot2Distance;
        
        while (time < duration)
        {
            var pos = _cam2.transform.position; // just so I don't have to write the whole thing everytime
            
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            
            pos = new Vector3(Mathf.Lerp(startValue, endValue, t), pos.y, pos.z);
            _cam2.transform.position = pos;
            
            _cam2.transform.LookAt(shot2Subject);

            time += Time.deltaTime;
            yield return null;
        }

        _cam2.transform.position = new Vector3(endValue, _cam2.transform.position.y, _cam2.transform.position.y);
        
        // cut to cam 3
        _cam3.gameObject.SetActive(true);
        _cam2.gameObject.SetActive(false);
        
        time = 0; // reset time
        
        // set values to next camera
        duration = shot3Duration;
        startValue = _cam3.transform.position.x;
        endValue = startValue + shot3Distance;
        
        while (time < duration)
        {
            var pos = _cam3.transform.position; // just so I don't have to write the whole thing everytime
            
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            
            pos = new Vector3(Mathf.Lerp(startValue, endValue, t), pos.y, pos.z);
            _cam3.transform.position = pos;
            
            _cam3.transform.LookAt(shot3Subject);

            time += Time.deltaTime;
            yield return null;
        }

        _cam3.transform.position = new Vector3(endValue, _cam3.transform.position.y, _cam3.transform.position.y);
        
        // kill camera 3
        _cam3.gameObject.SetActive(false);
    }
}
