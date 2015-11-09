﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthGUIScript : MonoBehaviour {

	public HealthScript script;
	public RectTransform uiParent;
	[Space]
	public Sprite filledHeart;
	public Sprite emptyHeart;

	private List<Image> elements = new List<Image>();

	void Start() {
		UpdateUIElements ();
	}

	// Called from HealthScript
	void OnHealthChange() {
		UpdateUIElements ();
	}

	// Create UI elements from scratch
	private void CreateUIElements() {
		// Remove any existing
		foreach (var img in elements) {
			Destroy(img.gameObject);
		}

		// Create new objects
		for (int hp=1; hp<=script.maxHealth; hp++) {
			// Create it
			GameObject obj = new GameObject();
			Image img = obj.AddComponent<Image>();

			// Set parent
			obj.transform.SetParent(uiParent,false);

			// Calculate scale & position
			float size = uiParent.rect.height; // 1:1 ratio
			float x = size * (hp - .5f - script.maxHealth/2f);

			img.rectTransform.offsetMin = new Vector2(x - size/2f, 0);
			img.rectTransform.offsetMax = new Vector2(x + size/2f, 0);
			img.rectTransform.anchorMin = new Vector2(0f,0f);
			img.rectTransform.anchorMax = new Vector2(1f,1f);

			img.preserveAspect = true;

			// Add to list
			elements.Add(img);
		}
	}

	// Update existing UI elements
	private void UpdateUIElements() {
		if (elements.Count != script.maxHealth) {
			CreateUIElements();
		}

		// Loop through each one
		for (int i=0; i<elements.Count; i++) {
			// Get image referance
			Image img = elements[i];

			// Update image
			img.sprite = i < script.health ? filledHeart : emptyHeart;
		}
	}

}
