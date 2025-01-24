using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
	        Destroy(BagCtrl.Instance.gameObject);
            StartCoroutine(Exittuto());
        }
    }

    private IEnumerator Exittuto()
    {
        AsyncOperation aload = SceneManager.LoadSceneAsync("lobbySample_Working1");
        while (aload != null && !aload.isDone)
        {
            yield return null;
        }
    }
}
