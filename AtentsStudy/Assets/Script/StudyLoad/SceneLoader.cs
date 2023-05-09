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
        yield return SceneManager.LoadSceneAsync(2);    // �ε� ���� ������ ������ ���!
        AsyncOperation op = SceneManager.LoadSceneAsync(i);
        // �ε� �� Ȱ��ȭ ����
        op.allowSceneActivation = false;

        Slider slider = FindObjectOfType<Slider>();

        while(!op.isDone)
        {
            slider.value = op.progress / 0.9f;
            if(Mathf.Approximately(slider.value, 1f))
            {
                yield return new WaitForSeconds(1f);    // ������ ���� ���� �����̸� ��
                op.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.5f);  // ������ ���� ���� �����̸� ��
        }
    }
}
