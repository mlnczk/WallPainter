Shader "Custom/MobileOcclusion"
{
    SubShader {
	    	Pass {
	    		// Render the Occlusion shader before all
				// opaque geometry to prime the depth buffer.
				Tags { "Queue"="Transparent" "RenderType" = "Transparent" }

				ZWrite Off

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
					float4 position : SV_POSITION;
				};

				v2f vert (appdata input)
				{
					v2f output;

					output.position = UnityObjectToClipPos(input.vertex);
					return output;
				}

				fixed4 frag (v2f input) : SV_Target
				{
					fixed4 col = tex2D(_Maintex, input.uv) + _Tintcolor;
					col.a = 0.5f
					return col;
				}
				ENDCG
	    	}
	}
}
