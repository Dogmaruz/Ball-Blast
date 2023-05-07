using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructible))]
public class StoneHitpointsText : MonoBehaviour
{
    [SerializeField] private Text _hitpointsText; //Текст колличества жизни камня.

    private Destructible _destructible;

    void Awake()
    {
        _destructible = GetComponent<Destructible>();

        _destructible.OnDamageEvent.AddListener(OnChangeHitpoints);
    }

    private void OnDestroy()
    {
        _destructible.OnDamageEvent.RemoveListener(OnChangeHitpoints);
    }

    //Отрисовка жизни на камне.
    private void OnChangeHitpoints()
    {
        int hitPoints = _destructible.GetHealth();
        
        if (hitPoints >= 1000)
        {
            _hitpointsText.text = hitPoints / 1000 + "K";

        } else
        {
            _hitpointsText.text = hitPoints.ToString();
        }
    }
}
