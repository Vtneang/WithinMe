Shader "Custom/DarknessShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+1"
            "RenderType" = "Transparent"
        }
        
        Stencil {
            Ref 1
            Comp NotEqual
            ReadMask 1
            Pass Keep
        }
        
        ZTest Off
        
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            sampler2D _MainTex;
            fixed4 _Darkness;

            fixed4 frag (v2f i) : SV_Target
            {
                return _Darkness;
            }
            ENDCG
        }
    }
}
