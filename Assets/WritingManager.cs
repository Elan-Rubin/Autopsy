using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WritingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private string textContent = "";
    [SerializeField] private GameObject hand;
    private Vector2 handCpos, handTpos;
    [SerializeField] private Vector2 startPos, endPos; 
    private int index;

    private static WritingManager instance;
    public static WritingManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    void Start()
    {
        startPos += (Vector2)transform.position;
        endPos += (Vector2)transform.position;
        handCpos = handTpos = hand.transform.position;
        DoText("notes taken by the player will show up on this notepad.");
    }

    void Update()
    {
        hand.transform.position = handCpos = Vector2.Lerp(handCpos, handTpos, Time.deltaTime * 10f);
    }

    public void DoText(string content) => StartCoroutine(nameof(DoTextCoroutine),content);

    private IEnumerator DoTextCoroutine(string content)
    {
        handCpos = hand.transform.position = endPos;
        handTpos = startPos;
        while (Vector2.Distance(handCpos, handTpos) > 0.1f) yield return null;
        text.text = textContent = "";
        while (index < content.Length)
        {
            var newchar = content[index];
            text.text = textContent += newchar;

            /*TMP_TextInfo textInfo = text.textInfo;
            TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
            Debug.Log(textInfo.characterInfo[index]);
            Vector3 bottomRight = charInfo.bottomRight;
            Debug.Log(bottomRight);
            hand.transform.position = bottomRight;*/
            if (newchar != ' ') handTpos = (Vector3)GetPositionOfLastLetterAsGameObject(text) + Vector3.right * 0.5f;
            else yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(0.1f);
            index++;
        }
        yield return new WaitForSeconds(0.5f);
        handTpos = endPos;
    }

    public Vector2 GetPositionOfLastLetterAsGameObject(TextMeshProUGUI tmp_text)
    {

        tmp_text.ForceMeshUpdate();

        Vector3[] vertices = tmp_text.mesh.vertices;
        TMP_CharacterInfo charInfo = tmp_text.textInfo.characterInfo[tmp_text.textInfo.characterCount - 1];
        int vertexIndex = charInfo.vertexIndex;

        Vector2 charMidTopLine = new Vector2((vertices[vertexIndex + 0].x + vertices[vertexIndex + 2].x) / 2, (charInfo.bottomLeft.y + charInfo.topLeft.y) / 2);
        Vector3 worldPos = tmp_text.transform.TransformPoint(charMidTopLine);

        GameObject charPositionGameObj = new("PositionOfLastChar");
        charPositionGameObj.transform.position = worldPos;
        Destroy(charPositionGameObj, 0.1f);
        return charPositionGameObj.transform.position;
    }
}
