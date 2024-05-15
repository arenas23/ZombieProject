using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image ProgressImage;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private UnityEvent<float> OnProgress;
    [SerializeField] private UnityEvent OnCompleted;
    [SerializeField] private Coroutine AnimationCoroutine;
    [SerializeField] private Gradient gradient;


    private void Start()
    {
        if (ProgressImage.type != Image.Type.Filled)
        {
            Debug.LogError($"{name}'s ProgressImage only supports Filled type");
            this.enabled = false;
        }
    }


    public void SetProgress(float progress)
    {
        SetProgress(progress, lerpSpeed);
    }

    public void SetProgress(float progress, float speed)
    {
        if (progress < 0 || progress > 1)
        {
            //Debug.LogWarning($"{progress} progress must be between 0 and 1");
            progress = Mathf.Clamp01(progress);
        }
        if (progress != ProgressImage.fillAmount)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            if(!GameManager.Instance.IsPaused) AnimationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }
    }

    private IEnumerator AnimateProgress(float progress, float speed)
    {
        if (gameObject.name == "ProgressBar")
        {
            float time = 0f;
            float initialProgress = ProgressImage.fillAmount;

            while (time < 1f)
            {
                ProgressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
                //Debug.Log("Fill amount: " + progress);
                time += Time.deltaTime * speed;

                ProgressImage.color = gradient.Evaluate(ProgressImage.fillAmount);

                OnProgress?.Invoke(ProgressImage.fillAmount);
                yield return null;
            }
        }
        ProgressImage.fillAmount = progress;
        ProgressImage.color = gradient.Evaluate(ProgressImage.fillAmount);
        OnProgress?.Invoke(progress);
        OnCompleted?.Invoke();
    }
}
