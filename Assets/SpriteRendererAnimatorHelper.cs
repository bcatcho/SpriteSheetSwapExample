using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class SpriteRendererAnimatorHelper : MonoBehaviour, IPointerDownHandler {
	public int CurrentSheetIndex = 0;
	private SpriteSheetVariant _currentSheet {
		get {
			return SheetVariants[CurrentSheetIndex];
		}
	}
	public SpriteSheetVariant[] SheetVariants;
	public Sprite CurrentSprite;
	private int _lastSpriteId;
	private SpriteRenderer _spriteRenderer;
	private SpriteRenderer CachedSpriteRenderer {
		get {
			if (_spriteRenderer == null) {
				_spriteRenderer = GetComponent<SpriteRenderer>();
			}

			return _spriteRenderer;
		}
	}

	public void LateUpdate () {
		if (_lastSpriteId != CurrentSprite.GetInstanceID())
		{
			var nextSprite = _currentSheet.Sprites.First( s => s.name == CurrentSprite.name);
			CachedSpriteRenderer.sprite = nextSprite;
			_lastSpriteId = nextSprite.GetInstanceID();
		}
	}

	#region IPointerClickHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		CurrentSheetIndex = (CurrentSheetIndex+1) % SheetVariants.Length;
	}

	#endregion
}

[System.Serializable]
public class SpriteSheetVariant
{
	public List<Sprite> Sprites;
}