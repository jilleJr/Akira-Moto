﻿using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;

[CustomEditor(typeof(SpriteRandomizerScript))]
public class _SpriteRandomizerScript : Editor {

	private ReorderableList list;

	private void OnEnable() {
		
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("sprites"), false, true, true, true);

		list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;

			EditorGUI.PropertyField(rect, element);
		};
		list.onSelectCallback = (ReorderableList l) => {
			var prefab = l.serializedProperty.GetArrayElementAtIndex(l.index).FindPropertyRelative("target");
			if (prefab != null && prefab.propertyType == SerializedPropertyType.ObjectReference)
				EditorGUIUtility.PingObject(prefab.objectReferenceValue as Sprite);
		};
		list.elementHeight = (list.count == 0 ? 1f : 2.5f) * EditorGUIUtility.singleLineHeight;
		list.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Sprites");
		};
	}

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		serializedObject.Update();
		list.elementHeight = (list.count == 0 ? 1f : 2.5f) * EditorGUIUtility.singleLineHeight;
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}

}
