using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{
    [SerializeField] protected string textToAdd;
    [SerializeField] protected Vector3 punchScale = new Vector3(0.2f, 0.2f, 0.2f);
    protected TextMeshProUGUI counterText;

    protected void Awake()
    {
        counterText = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateCounter(string counterTextValue)
    {
        counterText.text = textToAdd + counterTextValue;
        float punchDuration = 0.25f;
        counterText.transform.DOPunchScale(punchScale, punchDuration);
    }
}