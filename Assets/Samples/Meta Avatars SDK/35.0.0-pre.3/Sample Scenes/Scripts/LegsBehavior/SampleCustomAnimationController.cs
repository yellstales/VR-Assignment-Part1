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

#if USING_XR_MANAGEMENT && USING_XR_SDK_OCULUS && !OVRPLUGIN_UNSUPPORTED_PLATFORM
#define USING_XR_SDK
#endif

using Oculus.Avatar2.Experimental;
using UnityEngine;

namespace Oculus.Avatar2
{
    /// <summary>
    /// A sample script that demonstrates how to trigger transition between default (non-custom) / custom avatar animation
    /// </summary>
    public class SampleCustomAnimationController : MonoBehaviour
    {
        [Tooltip("The default (non-custom)/custom state transition parameter id as defined in the custom animation graph")]
        public string AnimationTransitionParamId = "TransitionToCustom";
        public OVRInput.Button TransitionButton = OVRInput.Button.Four;

        private OvrAvatarAnimationBehavior? _animationBehavior;


        private void Awake()
        {
            if (!TryGetComponent<OvrAvatarAnimationBehavior>(out _animationBehavior))
            {
                OvrAvatarLog.LogError("AnimationBehavior cannot be null. Drop in the avatar aniamtion behavior that is attached to the avatar game object");
            }
        }

        private void Update()
        {
            if (
#if UNITY_EDITOR || !USING_XR_SDK
            // Pressing the alpha 0 key when running in editor triggers the state transition in the animation graph
            Input.GetKeyUp(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0)
#else
            OVRInput.GetUp(TransitionButton)
#endif
            )
            {
                if (_animationBehavior!.CustomAnimationBlendFactor <= 0.5f)
                {
                    TransitionToCustomAnimation();
                }
                else
                {
                    TransitionToDefaultAnimation();
                }
            }
        }

        private void TransitionToCustomAnimation()
        {
            if (_animationBehavior!.CustomAnimator == null)
            {
                OvrAvatarLog.LogError("Unable to transition to custom animation, custom animator not found");
                return;
            }

            if (Mathf.Approximately(_animationBehavior!.CustomAnimationBlendFactor, 1))
            {
                OvrAvatarLog.LogWarning("Already running custom animation");
                return;
            }

            _animationBehavior!.CustomAnimator!.SetBool(AnimationTransitionParamId, true);
        }

        private void TransitionToDefaultAnimation()
        {
            if (_animationBehavior!.CustomAnimator == null)
            {
                OvrAvatarLog.LogError("Unable to transition to default animation, custom animator not found");
                return;
            }

            if (Mathf.Approximately(_animationBehavior!.CustomAnimationBlendFactor, 0))
            {
                OvrAvatarLog.LogWarning("Already running default (non-custom) animation");
                return;
            }

            _animationBehavior!.CustomAnimator!.SetBool(AnimationTransitionParamId, false);
        }
    }
}
