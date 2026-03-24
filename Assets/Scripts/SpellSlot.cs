using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Spell spellData;
    public bool hasSpell;
    // private SpellbookManager manager;

    private Button slotButton;
    private Image image;

    [Header("Hotbar Slot")]
    public bool isHotbar;
    public Button originalSpellSlot;
    public Spell emptySpell;

    private Vector2 descriptionPosition;

    void Awake()
    {
        slotButton = GetComponent<Button>();
        image = GetComponent<Image>();
        image.sprite = spellData.spellIcon;
        if (!hasSpell && !isHotbar)
        {
            slotButton.interactable = false;
        }
        
        // manager = SpellbookManager.instance;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hasSpell)
        {
            if (isHotbar)
            {
                descriptionPosition.x = transform.localPosition.x;
                descriptionPosition.y = transform.localPosition.y - 300;
            }
            else
            {
                descriptionPosition.x = transform.localPosition.x + 250;
                descriptionPosition.y = transform.localPosition.y;
            }
            SpellbookManager.instance.showDescription(spellData, descriptionPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hasSpell)
        {
            SpellbookManager.instance.hideDescription();
        }
    }

    public void addSpelltoHotbar()
    {
        SpellbookManager.instance.addSpelltoHotbar(spellData, slotButton);
        
    }

    public void removeSpellfromHotbar()
    {
        if (hasSpell)
        {
            SpellbookManager.instance.removeSpellfromHotbar();
            originalSpellSlot.interactable = true;
            clear();
        }
    }

    public void setSpell(Spell spell)
    {
        spellData = spell;
        image.sprite = spell.spellIcon;
        hasSpell = true;
    }
    public void clear()
    {
        spellData = emptySpell;
        image.sprite = emptySpell.spellIcon;
        originalSpellSlot = null;
        hasSpell = false;
    }

}
