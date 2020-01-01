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
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
            };

            FragData vert (appdata_base vd)
            {
                FragData fd;
                fd.pos = UnityObjectToClipPos(vd.vertex);
				fd.uv = vd.texcoord;
				fd.color = _MainColor;
                return fd;
            }

            fixed4 frag (FragData fd) : SV_Target
            {
				fixed2 uvs = float2(fd.uv.x, fd.uv.y);
				//fixed f = uvs[0] * uvs[1];
				fixed f = smoothstep(0.2, 0.8, uvs.x);
                return fixed4(f, f, f, 1);
            }

			fixed circle(fixed2 uvs, fixed radius){
				
			}
            ENDCG
        }
    }
}
