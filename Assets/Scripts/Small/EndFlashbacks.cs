using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndFlashbacks : MonoBehaviour
{
    public AudioClip flashbackClip;
    private AudioSource flashbackEndSFX;
    private Image flashObject;
    private bool triggered;

    public IEnumerator FlashbackCo()
    {
        flashObject.CrossFadeAlpha(155, 2f, false);
        yield return new WaitForSeconds(2f);
        flashObject.CrossFadeAlpha(0f, 2f, false);
        yield return new WaitForSeconds(flashbackClip.length);
        Destroy(this.gameObject);
    }

	void Awake ()
    {
        triggered = false;
        flashbackEndSFX = GameObject.Find("FlashbackSFX").GetComponent<AudioSource>();
        flashObject = GameObject.Find("MemoryFlashObj").GetComponent<Image>();
	}
	
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            StartCoroutine("FlashbackCo");
            if (flashbackClip != null)
            {
                flashbackEndSFX.clip = flashbackClip;
                flashbackEndSFX.Play();
            }
        }
    }
}
