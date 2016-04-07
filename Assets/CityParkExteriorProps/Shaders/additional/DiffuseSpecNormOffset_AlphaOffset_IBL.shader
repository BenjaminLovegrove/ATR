// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30929,y:32686|diff-42-OUT,spec-4212-OUT,normal-4266-RGB,alpha-170-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:31517,y:32457,ptlb:Diffuse(RGB) Spec(A),ptin:_DiffuseRGBSpecA,tex:14bb96bd6f2891147a98ab0bd1662452,ntxv:0,isnm:False|UVIN-4245-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:31376,y:33505,ptlb:Alpha Map (RGB) Offset,ptin:_AlphaMapRGBOffset,tex:6cf014d92aa0e4f43a8af2776d6bd16a,ntxv:0,isnm:False|UVIN-14-UVOUT;n:type:ShaderForge.SFN_Panner,id:14,x:31605,y:33505,spu:0.5,spv:1|UVIN-15-UVOUT,DIST-201-OUT;n:type:ShaderForge.SFN_TexCoord,id:15,x:31830,y:33505,uv:0;n:type:ShaderForge.SFN_Multiply,id:42,x:31283,y:32504|A-43-RGB,B-2-RGB,C-4219-OUT;n:type:ShaderForge.SFN_Color,id:43,x:31517,y:32288,ptlb:Diffuse Color,ptin:_DiffuseColor,glob:False,c1:0,c2:0.6689658,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:170,x:31192,y:33585|A-8-RGB,B-4505-R,C-171-OUT;n:type:ShaderForge.SFN_Slider,id:171,x:31376,y:33873,ptlb:Alpha Intensity,ptin:_AlphaIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ValueProperty,id:178,x:32157,y:33845,ptlb:Alpha Min Speed,ptin:_AlphaMinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:179,x:32157,y:33938,ptlb:Alpha Max Speed,ptin:_AlphaMaxSpeed,glob:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:180,x:31910,y:33874|A-178-OUT,B-179-OUT,T-181-OUT;n:type:ShaderForge.SFN_Slider,id:181,x:32131,y:34030,ptlb:Alpha Panning Speed,ptin:_AlphaPanningSpeed,min:0,cur:0.4917575,max:1;n:type:ShaderForge.SFN_Time,id:183,x:32157,y:33698;n:type:ShaderForge.SFN_Multiply,id:201,x:31830,y:33655|A-183-TSL,B-180-OUT;n:type:ShaderForge.SFN_Multiply,id:4212,x:31299,y:32684|A-2-A,B-4213-OUT;n:type:ShaderForge.SFN_Slider,id:4213,x:31506,y:32754,ptlb:Specular Intensity,ptin:_SpecularIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4219,x:31506,y:32646,ptlb:Color Intensity,ptin:_ColorIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Panner,id:4245,x:31969,y:32457,spu:0.5,spv:1|UVIN-4247-UVOUT,DIST-4259-OUT;n:type:ShaderForge.SFN_TexCoord,id:4247,x:32194,y:32457,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:4249,x:32519,y:32804,ptlb:Diffuse Min Speed,ptin:_DiffuseMinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4251,x:32519,y:32897,ptlb:Diffuse Max Speed,ptin:_DiffuseMaxSpeed,glob:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:4253,x:32272,y:32833|A-4249-OUT,B-4251-OUT,T-4255-OUT;n:type:ShaderForge.SFN_Slider,id:4255,x:32493,y:32989,ptlb:Diffuse Panning Speed,ptin:_DiffusePanningSpeed,min:0,cur:0.4917575,max:1;n:type:ShaderForge.SFN_Time,id:4257,x:32519,y:32657;n:type:ShaderForge.SFN_Multiply,id:4259,x:32192,y:32614|A-4257-TSL,B-4253-OUT;n:type:ShaderForge.SFN_Tex2d,id:4266,x:31794,y:32859,ptlb:Normal,ptin:_Normal,tex:652f99709e9953742800439b969accd9,ntxv:3,isnm:False|UVIN-4245-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4505,x:31376,y:33684,ptlb:Alpha Mask (R),ptin:_AlphaMaskR,tex:08f28e42a647e2a4cbd32e793359aa3c,ntxv:0,isnm:False;proporder:43-4219-4213-2-4249-4251-4255-8-4505-171-178-179-181-4266;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseSpecNormOffset_AlphaOffset_IBL" {
    Properties {
        _DiffuseColor ("Diffuse Color", Color) = (0,0.6689658,1,1)
        _ColorIntensity ("Color Intensity", Range(0, 1)) = 1
        _SpecularIntensity ("Specular Intensity", Range(0, 1)) = 1
        _DiffuseRGBSpecA ("Diffuse(RGB) Spec(A)", 2D) = "white" {}
        _DiffuseMinSpeed ("Diffuse Min Speed", Float ) = 0
        _DiffuseMaxSpeed ("Diffuse Max Speed", Float ) = 20
        _DiffusePanningSpeed ("Diffuse Panning Speed", Range(0, 1)) = 0.4917575
        _AlphaMapRGBOffset ("Alpha Map (RGB) Offset", 2D) = "white" {}
        _AlphaMaskR ("Alpha Mask (R)", 2D) = "white" {}
        _AlphaIntensity ("Alpha Intensity", Range(0, 1)) = 1
        _AlphaMinSpeed ("Alpha Min Speed", Float ) = 0
        _AlphaMaxSpeed ("Alpha Max Speed", Float ) = 20
        _AlphaPanningSpeed ("Alpha Panning Speed", Range(0, 1)) = 0.4917575
        _Normal ("Normal", 2D) = "bump" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform sampler2D _AlphaMapRGBOffset; uniform float4 _AlphaMapRGBOffset_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _AlphaMinSpeed;
            uniform float _AlphaMaxSpeed;
            uniform float _AlphaPanningSpeed;
            uniform float _SpecularIntensity;
            uniform float _ColorIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _AlphaMaskR; uniform float4 _AlphaMaskR_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_4245 = (i.uv0.rg+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0.5,1));
                float3 normalLocal = tex2D(_Normal,TRANSFORM_TEX(node_4245, _Normal)).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_4245, _DiffuseRGBSpecA));
                float node_4212 = (node_2.a*_SpecularIntensity);
                float3 specularColor = float3(node_4212,node_4212,node_4212);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_DiffuseColor.rgb*node_2.rgb*_ColorIntensity);
                finalColor += specular;
                float4 node_183 = _Time + _TimeEditor;
                float2 node_14 = (i.uv0.rg+(node_183.r*lerp(_AlphaMinSpeed,_AlphaMaxSpeed,_AlphaPanningSpeed))*float2(0.5,1));
                float2 node_4516 = i.uv0;
/// Final Color:
                return fixed4(finalColor,(tex2D(_AlphaMapRGBOffset,TRANSFORM_TEX(node_14, _AlphaMapRGBOffset)).rgb*tex2D(_AlphaMaskR,TRANSFORM_TEX(node_4516.rg, _AlphaMaskR)).r*_AlphaIntensity));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform sampler2D _AlphaMapRGBOffset; uniform float4 _AlphaMapRGBOffset_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _AlphaMinSpeed;
            uniform float _AlphaMaxSpeed;
            uniform float _AlphaPanningSpeed;
            uniform float _SpecularIntensity;
            uniform float _ColorIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _AlphaMaskR; uniform float4 _AlphaMaskR_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_4245 = (i.uv0.rg+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0.5,1));
                float3 normalLocal = tex2D(_Normal,TRANSFORM_TEX(node_4245, _Normal)).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_4245, _DiffuseRGBSpecA));
                float node_4212 = (node_2.a*_SpecularIntensity);
                float3 specularColor = float3(node_4212,node_4212,node_4212);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_DiffuseColor.rgb*node_2.rgb*_ColorIntensity);
                finalColor += specular;
                float4 node_183 = _Time + _TimeEditor;
                float2 node_14 = (i.uv0.rg+(node_183.r*lerp(_AlphaMinSpeed,_AlphaMaxSpeed,_AlphaPanningSpeed))*float2(0.5,1));
                float2 node_4517 = i.uv0;
/// Final Color:
                return fixed4(finalColor * (tex2D(_AlphaMapRGBOffset,TRANSFORM_TEX(node_14, _AlphaMapRGBOffset)).rgb*tex2D(_AlphaMaskR,TRANSFORM_TEX(node_4517.rg, _AlphaMaskR)).r*_AlphaIntensity),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
