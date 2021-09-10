using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VillagerDialogue : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] TextMeshProUGUI textComp;

    [Header("Generic Elements")]
    [SerializeField] string[] dialogueLines;

    [Header("Floats")]
    [SerializeField] float textSpeed;

    [Header("Integers")]
    [SerializeField] int randomIndex;

	private void OnEnable()
	{
        randomIndex = Random.Range(0, dialogueLines.Length);
    }
	private void Start()
	{
        textComp.text = string.Empty;
        gameObject.SetActive(false);
	}
	public void StartDialogue()
	{
        textComp.text = dialogueLines[randomIndex];
	}

    public void NextLine()
	{
        gameObject.SetActive(false);
	}
}
