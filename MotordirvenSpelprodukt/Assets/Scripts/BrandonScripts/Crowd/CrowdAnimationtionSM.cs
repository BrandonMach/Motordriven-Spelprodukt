using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CrowdAnim.Animation
{
    public class CrowdAnimationtionSM : StateMachineBehaviour
    {
        private float _lastPoseChangeDuration = float.PositiveInfinity;
        private float _poseChangeFrequency = 2f;
        private float _startPose;
        private float _endPose;
        float emotion = 0;
        public EntertainmentManager _etpManager;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _etpManager = GameObject.Find("Canvas").GetComponent<EntertainmentManager>();
            animator.SetFloat("Pose", Random.Range(0f, 1f));
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _lastPoseChangeDuration += Time.deltaTime;

            //if (_lastPoseChangeDuration >= _poseChangeFrequency && stateInfo.length >= 1)
            //{
            //    _startPose = animator.GetFloat("Pose");
            //    _endPose = Random.Range(0f, 1f);

            //    _lastPoseChangeDuration = 0;
            //}
           
            

            //if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            //{
            //    _startPose = animator.GetFloat("Pose");
            //    _endPose = Random.Range(0f, 1f);

            //    _lastPoseChangeDuration = 0;
            //    animator.SetFloat("Pose", Mathf.Lerp(_startPose, _endPose, _lastPoseChangeDuration / _poseChangeFrequency));

            //}


            animator.SetFloat("ETP", _etpManager.CrowdBehaviour);
            


            //if (Input.GetKeyDown(KeyCode.C))
            //{
            //    _startPose = animator.GetFloat("Pose");
            //    _endPose = Random.Range(0f, 1f);
            //    animator.SetFloat("Pose", Random.Range(0f, 1f));
            //    Debug.LogError("Strike a Pose");
            //}

            

           
            base.OnStateUpdate(animator, stateInfo, layerIndex);
        }

       
    }
}

