using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TypeWriteEffect : MonoBehaviour
{
	public TextMeshProUGUI textComponent;
	public TextMeshProUGUI subtitlesComponent;
    public string fullText = "En un mundo donde la tecnología avanza más rápido que la ética, algunas empresas han cruzado límites que nunca debieron romperse."; 
    public float typingSpeed = 0.05f;
    public float cursorBlinkSpeed = 0.5f;
    public float blinkDurationAfterTyping = 3f;
    public float erasingSpeed = 0.03f; 
    public AudioClip typingAudio; 
	public AudioClip voiceOverAudio;
	public string nextSceneName;

	public List<string> subtitles;
	public List<float> subtitleTimings;

    private string currentText = "";
    private AudioSource audioSource; 
    private bool isTyping = true;
   void Start()
   {
	   Cursor.lockState = CursorLockMode.Locked;
	   Cursor.visible = false;
	   audioSource = GetComponent<AudioSource>();
	   audioSource.clip = typingAudio;
	   audioSource.loop = true;
	   audioSource.Play();
	   StartCoroutine(TypeText());
   }

   IEnumerator TypeText()
   {
	   for (int i = 0; i < fullText.Length; i++)
	   {
		   currentText += fullText[i];
		   textComponent.text = currentText + "|";
		   yield return new WaitForSeconds(typingSpeed);
	   }

	   audioSource.Stop();
	   isTyping = false;
	   yield return StartCoroutine(CursorBlink(blinkDurationAfterTyping));
	   StartCoroutine(EraseText());
   }

   IEnumerator CursorBlink(float duration)
   {
	   float timer = 0f;
	   while (timer < duration)
	   {
		   textComponent.text = currentText + "|";
		   yield return new WaitForSeconds(cursorBlinkSpeed);
		   textComponent.text = currentText;
		   yield return new WaitForSeconds(cursorBlinkSpeed);
		   timer += cursorBlinkSpeed * 2;
	   }
   }

   IEnumerator EraseText()
   {
	   while (currentText.Length > 0)
	   {
		   currentText = currentText.Substring(0, currentText.Length - 1);
		   textComponent.text = currentText + "|";
		   yield return new WaitForSeconds(erasingSpeed);
	   }

	   textComponent.text = "";
	   PlayVoiceOver();
   }

   void PlayVoiceOver()
   {
	   audioSource.clip = voiceOverAudio;
	   audioSource.loop = false;
	   audioSource.Play();

	   StartCoroutine(DisplaySubtitles());
	   StartCoroutine(WaitForVoiceOverToFinish());
   }

   IEnumerator DisplaySubtitles()
   {
	   if (subtitles.Count != subtitleTimings.Count)
	   {
		   Debug.LogError("El numero de subtitulos no coincide con el numero de tiempos");
		   yield break;
	   }
	   subtitlesComponent.text = "";

	   for (int i = 0; i < subtitles.Count; i++)
	   {
		   subtitlesComponent.text = subtitles[i];
		   yield return new WaitForSeconds(subtitleTimings[i]);
	   }
	   subtitlesComponent.text = "";
   }

   IEnumerator  WaitForVoiceOverToFinish()
   {
	   while (audioSource.isPlaying)
	   {
		   yield return null;
	   }

	   SceneManager.LoadScene(nextSceneName);
   }
}
