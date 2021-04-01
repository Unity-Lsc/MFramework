Shader "Master/Sprite/Gray" {
	
	Properties {

		[PerRendererData] _MainTex("Sprite Texture",2D) = "white"{}
		_Color("Tint",Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel Snap",Float) = 0
		_GrayFactor("GrayFactor",Range(0,1)) = 1

	}

	SubShader {
		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAltas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass {

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN) {
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			float _GrayFactor;

			fixed4 SampleSpriteTexture(float2 uv) {
				fixed4 color = tex2D(_MainTex,uv);
				float grayValue = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;
				float4 grayColor = float4(grayValue,grayValue,grayValue,color.a);
				color = lerp(color,grayColor,_GrayFactor);

#if UNITY_TEXTUREALPHASPLIT_ALLOWED
				if(_AlphaSplitEnabled) {
					color.a = tex2D(_AlphaTex,uv).r;
				}
#endif
				return color;
			}

			fixed4 frag(v2f IN) : SV_TARGET {
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
				c.rgb *= c.a;
				return c;
			}

			ENDCG

		}

	}

}
