using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FadeObstracles : MonoBehaviour
{
    //private Actor _Player;
    //private bool _Flag;

    //private void Start() {
    //    _Player = PlayerController.Instance.Unit;
    //    _Flag = false;
    //}

    //void Update()
    //{
    //    var dir = _Player.transform.position - transform.position;
    //    var hits = Physics.RaycastAll(transform.position, dir, dir.magnitude, Constants.Layers.Masks.DamageObstracle);
    //    Debug.DrawLine(transform.position, transform.position + dir);
    //    if (!_Flag) {
    //        foreach (var hit in hits) {
    //            var materials = hit.collider.gameObject.GetComponent<Renderer>().materials;
    //            StartCoroutine(FadeRoutine(materials, 0.5f));
    //            Debug.Log(hit.collider.gameObject.name);
    //            _Flag = true;
    //        }
    //    }
    //}

    //private void SetMaterialTransparent(Material[] materials) {
    //    foreach(var m in materials) {
    //        m.SetFloat("_Mode", 3);
    //        m.SetInt("SrcBlend", (int)BlendMode.SrcAlpha);
    //        m.SetInt("DstBlend", (int)BlendMode.OneMinusSrcAlpha);
    //        m.SetInt("ZWrite", 0);
    //        m.DisableKeyword("_ALPHATEST_ON");
    //        m.EnableKeyword("_ALPHABLEND_ON");
    //        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //        m.renderQueue = 3000;
    //    }
    //}

    //private void SetMaterialOpaque(GameObject go) {
    //    foreach (var m in go.GetComponent<Renderer>().materials) {
    //        m.SetFloat("_Mode", 0);
    //        //m.SetInt("SrcBlend", (int)BlendMode.One);
    //        //m.SetInt("DstBlend", (int)BlendMode.Zero);
    //        //m.SetInt("ZWrite", 1);
    //        //m.DisableKeyword("_ALPHATEST_ON");
    //        //m.DisableKeyword("_ALPHABLEND_ON");
    //        //m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
    //        //m.renderQueue = -1;
    //    }
    //}

    //private Material CreateMaterial(Material source, Shader shader, Action<Material> materialPostprocessor) {
    //    var result = new Material(source);
    //    if (source.shader.name == MainShipShader) {
    //        result.shader = shader;
    //        if (materialPostprocessor != null) {
    //            materialPostprocessor(result);
    //        }
    //        return result;
    //    }
    //    else {
    //        result.shader = Shader.Find(EmptyShader);
    //        return result;
    //    }
    //}

    //private IEnumerator FadeRoutine(Material[] materials, float fadeTime) {
    //    SetMaterialTransparent(materials);
    //    var timer = fadeTime;
    //    while (timer > 0) {
    //        timer -= Time.unscaledDeltaTime;
    //        var a = timer / fadeTime;
    //        Mathf.Clamp(a, 0, 1);
    //        foreach(var m in materials) {
    //            m.color = new Color(m.color.r, m.color.g, m.color.b, a);
    //        }
    //        yield return null;
    //    }
    //}
}
