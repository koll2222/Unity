using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    private void Awake()
    {
        base.Initialize();
    }

    public void ChangeScene(int i)
    {
        StartCoroutine(Loading(i));
    }

    IEnumerator Loading(int i)
    {
        yield return SceneManager.LoadSceneAsync(2);    // 로딩 신이 끝나기 전까지 대기!
        AsyncOperation op = SceneManager.LoadSceneAsync(i);
        // 로딩 씬 활성화 금지
        op.allowSceneActivation = false;

        Slider slider = FindObjectOfType<Slider>();

        while(!op.isDone)
        {
            slider.value = op.progress / 0.9f;
            if(Mathf.Approximately(slider.value, 1f))
            {
                yield return new WaitForSeconds(1f);    // 눈으로 보기 위해 딜레이를 줌
                op.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.5f);  // 눈으로 보기 위해 딜레이를 줌
        }
    }
}
