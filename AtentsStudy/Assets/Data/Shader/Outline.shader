Shader "Unlit/Outline"
{
    Properties
    {
        _Color("Outline Color", Color) = (1, 0, 0, 1)
        _Thickness("Line Thickness", Range(0.0, 0.05)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry+99"}
        LOD 100

        Pass
        {
            ZWrite off
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _Color;
            float _Thickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // v.normal이 float3이기에 UNITY_MATRIX_IT_MV도 3x3행렬로 변환, UNITY_MATRIX_IT_MV의 기본은 4x4 행렬
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(normal.xy);
                o.pos.xy += offset * _Thickness;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
