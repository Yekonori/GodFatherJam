using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private Sprite dialogueSprite;
    [SerializeField] private GameObject dialogue;

    #endregion

    #region Unity Methods

    private void Start()
    {
        dialogue.SetActive(false);

        SpriteRenderer spriteRenderer = dialogue.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dialogueSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogue.SetActive(false);
        }
    }

    #endregion
}
