Shader "Unlit/AlwaysVisible"
{
    Properties  // ������ �̸��� ������ �Ʒ� SubShader���� �������־�� ��
    {
        _Color("SilhouetteColor", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "Queue" = "Geometry+90" }
        LOD 100

        Pass
        {
            ZWrite off
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(1,0,0,1);
            }
            ENDCG
        }
    }
}
