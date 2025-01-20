using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEditor.EditorGUILayout;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(LayoutCollection)), CanEditMultipleObjects]
public class LayoutEditorScript : Editor {
    const float addObjectButtonWidth = 150, addObjectButtonHeight = 25,
    addLayoutButtonWidth = 150, addLayoutButtonHeight = 25,
    loadLayoutButtonWidth = 150, loadLayoutButtonHeight = 25,
    lockObjectButtonWidth=50, lockObjectButtonHeight = 25,
    deleteObjectButtonWidth=50, deleteObjectButtonHeight=25,
    lockLayoutButtonWidth = 50, lockLayoutButtonHeight = 25,
    deleteLayoutButtonWidth = 50, deleteLayoutButtonHeight = 25; 
    bool showObjects, addObject, showLayouts, addLayout;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        LayoutCollection m = (LayoutCollection) target;

        //Selected Objects Current Position
        showObjects = Foldout(showObjects, "Objects - " + m.objects.Count);

        if (showObjects) {
            addObject = GUILayout.Button("Add Object", GUILayout.Width(addObjectButtonWidth), GUILayout.Height(addObjectButtonHeight));
            DisplaySelectedObjects(m);
        }

        showLayouts = Foldout(showLayouts, "Layouts - " + m.layouts.Count);

        if (showLayouts) {
            addLayout = GUILayout.Button("Add Layout", GUILayout.Width(addLayoutButtonWidth), GUILayout.Height(addLayoutButtonHeight));
            DisplayLayoutCollection(m);
        }

        if (addObject) {
            m.objects.Add(null);
            m.lockObject.Add(true);
            foreach (ObjectLayout l in m.layouts) {
                l.Scales.Add(Vector3.zero);
                l.Positions.Add(Vector3.zero);
            }
        }

        if (addLayout) {
            ObjectLayout l = new ObjectLayout();
            m.layouts.Add(l);
            m.lockLayout.Add(true);
            m.showLayout.Add(true);
            for (int i = 0; i < m.objects.Count; i++) {
                if (m.objects[i] == null) {
                    l.Positions.Add(Vector3.zero);
                    l.Scales.Add(Vector3.zero);
                } else {
                    l.Positions.Add(m.objects[i].transform.position);
                    l.Scales.Add(m.objects[i].transform.localScale);
                }
            }
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(m);
            EditorSceneManager.MarkSceneDirty(m.gameObject.scene);
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DisplaySelectedObjects(LayoutCollection m) {
        for (int i = 0; i < m.objects.Count; i++) {
            bool doLock;
            bool delete = false;

            BeginHorizontal();
            
            if (m.objects[i] == null) {
                m.objects[i] = (GameObject) ObjectField("O" + i, null, typeof(GameObject), true);
            } else {
                m.objects[i] = (GameObject) ObjectField(m.objects[i].name, m.objects[i], typeof(GameObject), true);
            }
            doLock = GUILayout.Button("Lock", GUILayout.Width(lockObjectButtonWidth), GUILayout.Height(lockObjectButtonHeight));
            if (doLock) m.lockObject[i] = !m.lockObject[i];

            if (!m.lockObject[i]) {
                delete = GUILayout.Button("Delete", GUILayout.Width(deleteObjectButtonWidth), GUILayout.Height(deleteObjectButtonHeight));
            }

            EndHorizontal();
            
            if (delete) {
                m.objects.RemoveAt(i);
                m.lockObject.RemoveAt(i);
                foreach (ObjectLayout l in m.layouts) {
                    l.Scales.RemoveAt(i);
                    l.Positions.RemoveAt(i);
                }
                i--;
            }
        }
    }

    private void DisplayLayoutCollection(LayoutCollection m) {
        for (int i = 0; i < m.layouts.Count; i++) {
            BeginHorizontal();

            bool doLock;
            bool delete = false;
            m.showLayout[i] = Foldout(m.showLayout[i], "Layout " + i);
            bool loadLayout = GUILayout.Button("Load Layout", GUILayout.Width(loadLayoutButtonWidth), GUILayout.Height(loadLayoutButtonHeight));
            doLock = GUILayout.Button("Lock", GUILayout.Width(lockLayoutButtonWidth), GUILayout.Height(lockLayoutButtonHeight));
            if (doLock) m.lockLayout[i] = !m.lockLayout[i];

            if (!m.lockLayout[i]) {
                delete = GUILayout.Button("Delete", GUILayout.Width(deleteLayoutButtonWidth), GUILayout.Height(deleteLayoutButtonHeight));
            }

            EndHorizontal();

            if (loadLayout) {
                LoadLayout(m.layouts[i], m);
            }

            if (m.showLayout[i]) {
                DisplayLayout(m.layouts[i], m);
            }

            if (delete) {
                m.layouts.RemoveAt(i);
                m.lockLayout.RemoveAt(i);
                m.showLayout.RemoveAt(i);
                i--;
            }
        }
    }

    private void DisplayLayout(ObjectLayout layout, LayoutCollection m) {

        for (int i = 0; i < m.objects.Count; i++) {
            if (m.objects[i] == null) {
                LabelField("O" + i);
            } else {
                LabelField(m.objects[i].name);
            }
            layout.Scales[i] = Vector3Field("Scale", layout.Scales[i]);
            layout.Positions[i] = Vector3Field("Position", layout.Positions[i]);
        }
    }

    private void LoadLayout(ObjectLayout layout, LayoutCollection m) {
        for (int i = 0; i < m.objects.Count; i++) {
            if (m.objects[i] == null) continue;

            m.objects[i].transform.localScale = layout.Scales[i];
            m.objects[i].transform.position = layout.Positions[i];
        }
    }
}
