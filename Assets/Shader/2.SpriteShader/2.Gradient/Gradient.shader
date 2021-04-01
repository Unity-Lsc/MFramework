Shader "Master/Sprite/Gradient" {
	
	Properties {

		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
		//初始颜色
		_FromColor ("FromColor",Color) = (1,1,1,1)
		//目标颜色
		_ToColor ("ToColor",Color) = (1,1,1,1)
		//是否是垂直方向
		[Toggle(VerticalDirection)] _VerticalDirection ("Vertival Direction",Int) = 0
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0

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
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
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

			float4 _FromColor;
			float4 _ToColor;
			int _VerticalDirection;

			fixed4 SampleSpriteTexture(float2 uv) {
				fixed4 color = tex2D(_MainTex,uv);
				float toColorFactor = lerp(uv.x,uv.y,_VerticalDirection);
				color = lerp(_FromColor,_ToColor,toColorFactor) * color;

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
