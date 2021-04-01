// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Master/Cartoon" {
	Properties {
		_Diffuse("Diffuse",Color) = (1,1,1,1)
		_Specular("Specular",Color) = (1,1,1,1)
		_Gloss("Gloss",Range(8.0,256)) = 20
	}

	SubShader {
		//外描边通道
		Pass {
			Tags{"LightMode" = "ForwardBase"}
			//剔除掉正面
			Cull Front

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			struct v2f {
				//float3 worldNormal : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			float4 _Diffuse;

			v2f vert(appdata v) {

				v2f o;

				//获取法线
				float3 normal = v.normal;
				//顶点加0.02倍的法线
				v.vertex.xyz += normal * 0.02;
				o.pos = UnityObjectToClipPos(v.vertex);

				//为了看到结果 每个顶点都向右偏移一点
				//o.pos.x += 0.5;

				return o;

			}

			float4 frag(v2f i) : SV_Target {
				//全部设置成黑色
				return fixed4(0,0,0,1);
			}
			ENDCG
		}

		//显示通道
		Pass {
			Tags{"LightMode" = "ForwardBase"}
			Cull Back

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			//把顶点着色器计算的结果传递到片元着色器
			struct v2f {
				float3 worldNormal : TEXCOORD0;
				float4 pos : SV_POSITION;//存储物体顶点在屏幕坐标上的位置(裁剪空间中的顶点坐标)
				float3 worldPos : TEXCOORD1;
				//float3 color : COLOR;//告诉shader,color存储的是顶点颜色
			};

			fixed4 _Diffuse;
			fixed4 _Specular;
			float _Gloss;


			v2f vert(appdata v) {

				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));
				o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;

				return o;

			}

			//片元着色器(像素着色器)
			//逐像素计算颜色,输出到屏幕上(或帧缓冲中)
			//SV_Target 数据直接用于渲染,是语义关键字(变量) 告诉shader数据来源和去向
			float4 frag(v2f i) : SV_Target {
				
				//环境光
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldNormal = normalize(i.worldNormal);
				//顶点到光源方向
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

				//Cdiffuse = (Clight * Mdiffuse) * max(0,N^*L^)
				float dotL = 0.5 + 0.5 * saturate(dot(i.worldNormal,worldLightDir));
				if(dotL > 0.6) {
					dotL = 1;
				} else if(dotL > 0.2) {
					dotL = 0.3;
				} else {
					dotL = 0;
				}
				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * dotL;

				fixed3 reflectDir = normalize(reflect(-worldLightDir,worldNormal));
				fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);

				float spec = pow(saturate(dot(reflectDir,viewDir)),_Gloss);
				
				//高光只有两个色阶,即白色和无色
				if(spec > 0.001) {
					spec = 1;
				}else {
					spec = 0;
				}

				fixed3 specular = _LightColor0.rgb * _Specular.rgb * spec;

				return fixed4(ambient + diffuse + specular,1.0);
			}


			ENDCG

		}
	}

}
