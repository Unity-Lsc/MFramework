﻿Shader "Master/Sprite/GaussianBlur" {
	
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture",2D) = "white"{}
		_Color ("Tint", Color) = (1,1,1,1)
		//模糊程度
		_Blur ("Blur",Range(0,1)) = 0.01
		[MaterialToggle] PixelSanp ("Pixel Snap",Float) = 0
	}

	SubShader {

		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
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
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN) {
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(IN.vertex);
				#endif
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			float _Blur;

			fixed4 SampleSpriteTexture(float2 uv) {
				//1/16
				float offset = _Blur * 0.0625f;

				//左上
				fixed4 color = tex2D(_MainTex,float2(uv.x - offset,uv.y + offset)) * 0.0947416f;
				//上
				color += tex2D(_MainTex,float2(uv.x,uv.y + offset)) * 0.118318f;
				//右上
				color += tex2D(_MainTex,float2(uv.x + offset,uv.y + offset)) * 0.0947416f;
				//左
				color += tex2D(_MainTex,float2(uv.x - offset,uv.y)) * 0.118318f;
				//中
				color += tex2D(_MainTex,float2(uv.x,uv.y)) * 0.147761f;
				//右
				color += tex2D(_MainTex,float2(uv.x,uv.y + offset)) * 0.118318f;
				//左下
				color += tex2D(_MainTex,float2(uv.x - offset,uv.y - offset)) * 0.0947416f;
				//下
				color += tex2D(_MainTex,float2(uv.x,uv.y - offset)) * 0.118318f;
				//右下
				color += tex2D(_MainTex,float2(uv.x + offset,uv.y - offset)) * 0.0947416f;

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
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
