/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#nullable enable

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Oculus.Avatar2
{
    /// <summary>
    /// A debug visualizer to highlight the bones and joints in a character rig
    /// </summary>
    public class RigVisualizerUtility : MonoBehaviour
    {
        private Transform? _root;
        private Color _color = Color.red;

        public void Initialize(Transform root, Color visualColor)
        {
            _color = visualColor;
            _root = root;
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (_root == null)
            {
                return;
            }

            DrawTransform(_root, _color);
        }

        private void DrawTransform(Transform? transform, Color color)
        {
            if (transform == null)
            {
                return;
            }

            if (transform != _root && transform.parent != null)
            {
                Handles.color = color;
                Handles.DrawLine(transform.position, transform.parent.position);
            }

            transform.DebugDrawInEditor(0.03f);

            for (int i = 0; i < transform.childCount; ++i)
            {
                DrawTransform(transform.GetChild(i), color);
            }
        }
    }
}
#endif
