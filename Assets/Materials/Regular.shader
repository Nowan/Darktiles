Shader "Unlit/SolidColor"
{
    Properties
    {
		_MainColor ("Main Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			fixed4 _MainColor;

            struct FragData
            {
                float4 pos : SV_POSITION;
				float4 color : COLOR;
            };

            FragData vert (appdata_base vd)
            {
                FragData fd;
                fd.pos = UnityObjectToClipPos(vd.vertex);
				fd.color = _MainColor;
                return fd;
            }

            fixed4 frag (FragData fd) : SV_Target
            {
                return fd.color;
            }

            ENDCG
        }

		Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			fixed4 _MainColor;

            struct FragData
            {
                float4 pos : SV_POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
            };

            FragData vert (appdata_base vd)
            {
                FragData fd;
                fd.pos = UnityObjectToClipPos(vd.vertex);
				fd.color = _MainColor;
                return fd;
            }

            fixed4 frag (FragData fd) : SV_Target
            {
                return fd.color;
            }

            ENDCG
        }
    }
}
