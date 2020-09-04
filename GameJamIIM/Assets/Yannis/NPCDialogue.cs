using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    #region Script Parameters

    [SerializeField] private List<Sprite> narciNPC;
    [SerializeField] private List<Sprite> badBasic;
    [SerializeField] private List<Sprite> badSmall;
    [SerializeField] private List<Sprite> badBig;
    [SerializeField] private List<Sprite> badBalloon;
    [SerializeField] private GameObject dialogue;

    private int rndIndex;

    #endregion

    #region Unity Methods

    private void Start()
    {
        dialogue.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            eForms playerForm = collision.gameObject.GetComponent<PlayerChangeForm>().CurrentForm;

            List<Sprite> dialogues = new List<Sprite>();

            switch (playerForm)
            {
                //case eForms.BASE:
                //    dialogues = narciNPC.Concat(badBasic).ToList();
                //    Debug.Log("BASE");
                //    break;
                case eForms.BIG:
                    dialogues = narciNPC.Concat(badBig).ToList();
                    Debug.Log("BIG");
                    break;
                case eForms.BALLOON:
                    dialogues = narciNPC.Concat(badBalloon).ToList();
                    Debug.Log("BALLOON");
                    break;
                case eForms.NAIN:
                    dialogues = narciNPC.Concat(badSmall).ToList();
                    Debug.Log("NAIN");
                    break;
                default:
                    break;
            }

            SpriteRenderer spriteRenderer = dialogue.GetComponent<SpriteRenderer>();

            if (dialogues.Count > 0)
            {
                int rnd = Random.Range(0, dialogues.Count - 1);
                spriteRenderer.sprite = dialogues[rnd];

                dialogue.SetActive(true);
            }
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
