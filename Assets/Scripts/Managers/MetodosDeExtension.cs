using System.Collections;
using UnityEngine;

public static class MetodosDeExtension
{
    //La idea de metodos de extension viene de:
    //https://www.youtube.com/watch?v=E7b3ZNmhbnU&t=12s 


    //-------------------------------------------------------------------------------------------------------------------
    #region Transform
    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    #endregion


    //-------------------------------------------------------------------------------------------------------------------
    #region Vector3
    //Codigo sacado de: https://www.youtube.com/watch?v=E7b3ZNmhbnU
    public static Vector3 CambiarX(this Vector3 v, float nuevoValor)
    {
        return new Vector3(nuevoValor, v.y, v.z);
    }

    public static Vector3 CambiarY(this Vector3 v, float nuevoValor)
    {
        return new Vector3(v.x, nuevoValor, v.z);
    }

    public static Vector3 CambiarZ(this Vector3 v, float nuevoValor)
    {
        return new Vector3(v.x, v.z, nuevoValor);
    }
    #endregion


    //-------------------------------------------------------------------------------------------------------------------
    #region Vector2
    public static Vector2 CambiarX(this Vector2 v, float nuevoValor)
    {
        return new Vector2(nuevoValor, v.y);
    }

    public static Vector2 CambiarY(this Vector2 v, float nuevoValor)
    {
        return new Vector2(v.x, nuevoValor);
    }
    #endregion
}
