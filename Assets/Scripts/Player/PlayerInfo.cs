//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

//public class PlayerInfo : MonoBehaviour
//{
//    //Variables del blur
//    DepthOfField dofComponent;
//    public float blur;

//    private void Awake()
//    {
//    }

//    void Start()
//    {
       

//    }

//    void Update()
//    {
//        SetBlurValue(blur);
//    }

//    public void SetBlurValue(float _blur)
//    {
//        Volume volume = GameManager.instance.globalVolume.GetComponent<Volume>();
//        DepthOfField tmp;
//        if (volume.profile.TryGet<DepthOfField>(out tmp))
//        {
//            dofComponent = tmp;
//        }
//        blur = _blur;
//        dofComponent.aperture.value = _blur;
//    }
//}
