// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:False,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30929,y:32686|diff-6370-OUT,spec-6422-OUT,gloss-6447-OUT,normal-5331-OUT,alpha-5669-OUT,refract-5335-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:31628,y:32392,ptlb:Diffuse(RGB) Spec(A),ptin:_DiffuseRGBSpecA,tex:59ac46d95fca52d499ba15e347a0ea0c,ntxv:0,isnm:False|UVIN-5107-OUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:31550,y:33637,ptlb:Alpha Map (R) Offset,ptin:_AlphaMapROffset,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:42,x:31415,y:32398|A-43-RGB,B-2-RGB,C-4219-OUT;n:type:ShaderForge.SFN_Color,id:43,x:31628,y:32223,ptlb:Diffuse Color,ptin:_DiffuseColor,glob:False,c1:0.6797686,c2:0.8497227,c3:0.9338235,c4:1;n:type:ShaderForge.SFN_Slider,id:171,x:31550,y:33839,ptlb:Alpha Intensity,ptin:_AlphaIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:4212,x:31415,y:32684|A-2-A,B-4213-OUT;n:type:ShaderForge.SFN_Slider,id:4213,x:31622,y:32754,ptlb:Specular Intensity,ptin:_SpecularIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4219,x:31622,y:32646,ptlb:Diffuse Intensity,ptin:_DiffuseIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ValueProperty,id:4249,x:32926,y:32328,ptlb:Diffuse Min Speed,ptin:_DiffuseMinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4251,x:32926,y:32421,ptlb:Diffuse Max Speed,ptin:_DiffuseMaxSpeed,glob:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:4253,x:32679,y:32357|A-4249-OUT,B-4251-OUT,T-4255-OUT;n:type:ShaderForge.SFN_Slider,id:4255,x:32900,y:32513,ptlb:Diffuse Panning Speed,ptin:_DiffusePanningSpeed,min:0,cur:0.4917575,max:1;n:type:ShaderForge.SFN_Time,id:4257,x:32926,y:32181;n:type:ShaderForge.SFN_Tex2d,id:4266,x:32986,y:32876,ptlb:Normal,ptin:_Normal,tex:2dd3788f8589b40bf82a92d76ffc5091,ntxv:3,isnm:True|UVIN-5193-OUT;n:type:ShaderForge.SFN_Lerp,id:5107,x:31802,y:32082|A-5110-UVOUT,B-5109-R,T-5111-OUT;n:type:ShaderForge.SFN_Tex2d,id:5109,x:32071,y:32090,ptlb:Diff Distortion Map (R),ptin:_DiffDistortionMapR,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-5120-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5110,x:32071,y:31914,uv:0;n:type:ShaderForge.SFN_Slider,id:5111,x:32071,y:32286,ptlb:Diffuse Distortion Intensity,ptin:_DiffuseDistortionIntensity,min:0,cur:0.2874747,max:1;n:type:ShaderForge.SFN_Panner,id:5120,x:32273,y:32090,spu:0,spv:0.1|DIST-5145-OUT;n:type:ShaderForge.SFN_Multiply,id:5145,x:32567,y:32109|A-4257-TSL,B-4253-OUT;n:type:ShaderForge.SFN_Tex2d,id:5179,x:32989,y:33160,ptlb:Normal 02,ptin:_Normal02,tex:fb6566c21f717904f83743a5a76dd0b0,ntxv:3,isnm:True|UVIN-5194-OUT;n:type:ShaderForge.SFN_Vector3,id:5188,x:32986,y:32724,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Add,id:5193,x:33216,y:32907|A-5195-UVOUT,B-5196-OUT;n:type:ShaderForge.SFN_Add,id:5194,x:33216,y:33136|A-5202-UVOUT,B-5204-OUT;n:type:ShaderForge.SFN_TexCoord,id:5195,x:33598,y:32807,uv:0;n:type:ShaderForge.SFN_Multiply,id:5196,x:33598,y:32986|A-5197-OUT,B-5198-T;n:type:ShaderForge.SFN_Append,id:5197,x:33846,y:32847|A-5199-OUT,B-5200-OUT;n:type:ShaderForge.SFN_Time,id:5198,x:33846,y:33001;n:type:ShaderForge.SFN_ValueProperty,id:5199,x:34125,y:32807,ptlb:Normal 01 X Speed,ptin:_Normal01XSpeed,glob:False,v1:0.02;n:type:ShaderForge.SFN_ValueProperty,id:5200,x:34125,y:32896,ptlb:Normal 01 Y Speed,ptin:_Normal01YSpeed,glob:False,v1:0.05;n:type:ShaderForge.SFN_TexCoord,id:5202,x:33600,y:33166,uv:0;n:type:ShaderForge.SFN_Multiply,id:5204,x:33600,y:33344|A-5206-OUT,B-5208-T;n:type:ShaderForge.SFN_Append,id:5206,x:33848,y:33206|A-5210-OUT,B-5212-OUT;n:type:ShaderForge.SFN_Time,id:5208,x:33848,y:33360;n:type:ShaderForge.SFN_ValueProperty,id:5210,x:34127,y:33166,ptlb:Normal 02 X Speed,ptin:_Normal02XSpeed,glob:False,v1:-0.02;n:type:ShaderForge.SFN_ValueProperty,id:5212,x:34127,y:33255,ptlb:Normal 02 Y Speed,ptin:_Normal02YSpeed,glob:False,v1:-0.05;n:type:ShaderForge.SFN_Slider,id:5325,x:32604,y:33126,ptlb:Refraction Intensity,ptin:_RefractionIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Lerp,id:5331,x:32369,y:32606|A-5188-OUT,B-5364-OUT,T-5325-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5334,x:32387,y:32894,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5364-OUT;n:type:ShaderForge.SFN_Multiply,id:5335,x:32202,y:32894|A-5334-OUT,B-5346-OUT;n:type:ShaderForge.SFN_Multiply,id:5346,x:32387,y:33102|A-5325-OUT,B-5347-OUT;n:type:ShaderForge.SFN_Vector1,id:5347,x:32584,y:33245,v1:0.2;n:type:ShaderForge.SFN_Add,id:5364,x:32706,y:32890|A-4266-RGB,B-5179-RGB;n:type:ShaderForge.SFN_Multiply,id:5669,x:31328,y:33608|A-6342-R,B-8-R,C-171-OUT;n:type:ShaderForge.SFN_Tex2d,id:6342,x:31550,y:33455,ptlb:Alpha Mask (R),ptin:_AlphaMaskR,tex:01b622d4830754f4886f4b03606fb1ec,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:6370,x:31203,y:32398|A-42-OUT,B-6371-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6371,x:31415,y:32610,ptlb:Diffuse Multiplier,ptin:_DiffuseMultiplier,cmnt:Make Diffuse more noticable,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6422,x:31223,y:32684|A-4212-OUT,B-6424-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6424,x:31415,y:32853,ptlb:Specular Multiplier,ptin:_SpecularMultiplier,cmnt:Make Specular more noticable,glob:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:6447,x:31223,y:32810,v1:2;proporder:43-6371-4219-6424-4213-2-5109-4249-4251-4255-5111-6342-8-171-5325-4266-5199-5200-5179-5210-5212;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseSpecNormOffset_AlphaRotate_AlphaCutout_IBL" {
    Properties {
        _DiffuseColor ("Diffuse Color", Color) = (0.6797686,0.8497227,0.9338235,1)
        _DiffuseMultiplier ("Diffuse Multiplier", Float ) = 1
        _DiffuseIntensity ("Diffuse Intensity", Range(0, 1)) = 1
        _SpecularMultiplier ("Specular Multiplier", Float ) = 1
        _SpecularIntensity ("Specular Intensity", Range(0, 1)) = 1
        _DiffuseRGBSpecA ("Diffuse(RGB) Spec(A)", 2D) = "white" {}
        _DiffDistortionMapR ("Diff Distortion Map (R)", 2D) = "white" {}
        _DiffuseMinSpeed ("Diffuse Min Speed", Float ) = 0
        _DiffuseMaxSpeed ("Diffuse Max Speed", Float ) = 20
        _DiffusePanningSpeed ("Diffuse Panning Speed", Range(0, 1)) = 0.4917575
        _DiffuseDistortionIntensity ("Diffuse Distortion Intensity", Range(0, 1)) = 0.2874747
        _AlphaMaskR ("Alpha Mask (R)", 2D) = "white" {}
        _AlphaMapROffset ("Alpha Map (R) Offset", 2D) = "white" {}
        _AlphaIntensity ("Alpha Intensity", Range(0, 1)) = 1
        _RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 1
        _Normal ("Normal", 2D) = "bump" {}
        _Normal01XSpeed ("Normal 01 X Speed", Float ) = 0.02
        _Normal01YSpeed ("Normal 01 Y Speed", Float ) = 0.05
        _Normal02 ("Normal 02", 2D) = "bump" {}
        _Normal02XSpeed ("Normal 02 X Speed", Float ) = -0.02
        _Normal02YSpeed ("Normal 02 Y Speed", Float ) = -0.05
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform sampler2D _AlphaMapROffset; uniform float4 _AlphaMapROffset_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _SpecularIntensity;
            uniform float _DiffuseIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _DiffDistortionMapR; uniform float4 _DiffDistortionMapR_ST;
            uniform float _DiffuseDistortionIntensity;
            uniform sampler2D _Normal02; uniform float4 _Normal02_ST;
            uniform float _Normal01XSpeed;
            uniform float _Normal01YSpeed;
            uniform float _Normal02XSpeed;
            uniform float _Normal02YSpeed;
            uniform float _RefractionIntensity;
            uniform sampler2D _AlphaMaskR; uniform float4 _AlphaMaskR_ST;
            uniform float _DiffuseMultiplier;
            uniform float _SpecularMultiplier;
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
                float4 screenPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_5198 = _Time + _TimeEditor;
                float2 node_5193 = (i.uv0.rg+(float2(_Normal01XSpeed,_Normal01YSpeed)*node_5198.g));
                float4 node_5208 = _Time + _TimeEditor;
                float2 node_5194 = (i.uv0.rg+(float2(_Normal02XSpeed,_Normal02YSpeed)*node_5208.g));
                float3 node_5364 = (UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_5193, _Normal))).rgb+UnpackNormal(tex2D(_Normal02,TRANSFORM_TEX(node_5194, _Normal02))).rgb);
                float3 normalLocal = lerp(float3(0,0,1),node_5364,_RefractionIntensity);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_5364.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 2.0;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_6472 = i.uv0;
                float2 node_5120 = (node_6472.rg+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0,0.1));
                float4 _DiffDistortionMapR_var = tex2D(_DiffDistortionMapR,TRANSFORM_TEX(node_5120, _DiffDistortionMapR));
                float2 node_5107 = lerp(i.uv0.rg,float2(_DiffDistortionMapR_var.r,_DiffDistortionMapR_var.r),_DiffuseDistortionIntensity);
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_5107, _DiffuseRGBSpecA));
                float node_6422 = ((node_2.a*_SpecularIntensity)*_SpecularMultiplier);
                float3 specularColor = float3(node_6422,node_6422,node_6422);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * ((_DiffuseColor.rgb*node_2.rgb*_DiffuseIntensity)*_DiffuseMultiplier);
                finalColor += specular;
/// Final Color:
                return fixed4(lerp(sceneColor.rgb, finalColor,(tex2D(_AlphaMaskR,TRANSFORM_TEX(node_6472.rg, _AlphaMaskR)).r*tex2D(_AlphaMapROffset,TRANSFORM_TEX(node_6472.rg, _AlphaMapROffset)).r*_AlphaIntensity)),1);
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
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseRGBSpecA; uniform float4 _DiffuseRGBSpecA_ST;
            uniform sampler2D _AlphaMapROffset; uniform float4 _AlphaMapROffset_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _SpecularIntensity;
            uniform float _DiffuseIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _DiffDistortionMapR; uniform float4 _DiffDistortionMapR_ST;
            uniform float _DiffuseDistortionIntensity;
            uniform sampler2D _Normal02; uniform float4 _Normal02_ST;
            uniform float _Normal01XSpeed;
            uniform float _Normal01YSpeed;
            uniform float _Normal02XSpeed;
            uniform float _Normal02YSpeed;
            uniform float _RefractionIntensity;
            uniform sampler2D _AlphaMaskR; uniform float4 _AlphaMaskR_ST;
            uniform float _DiffuseMultiplier;
            uniform float _SpecularMultiplier;
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
                float4 screenPos : TEXCOORD5;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float4 node_5198 = _Time + _TimeEditor;
                float2 node_5193 = (i.uv0.rg+(float2(_Normal01XSpeed,_Normal01YSpeed)*node_5198.g));
                float4 node_5208 = _Time + _TimeEditor;
                float2 node_5194 = (i.uv0.rg+(float2(_Normal02XSpeed,_Normal02YSpeed)*node_5208.g));
                float3 node_5364 = (UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(node_5193, _Normal))).rgb+UnpackNormal(tex2D(_Normal02,TRANSFORM_TEX(node_5194, _Normal02))).rgb);
                float3 normalLocal = lerp(float3(0,0,1),node_5364,_RefractionIntensity);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_5364.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = 2.0;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_6473 = i.uv0;
                float2 node_5120 = (node_6473.rg+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0,0.1));
                float4 _DiffDistortionMapR_var = tex2D(_DiffDistortionMapR,TRANSFORM_TEX(node_5120, _DiffDistortionMapR));
                float2 node_5107 = lerp(i.uv0.rg,float2(_DiffDistortionMapR_var.r,_DiffDistortionMapR_var.r),_DiffuseDistortionIntensity);
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_5107, _DiffuseRGBSpecA));
                float node_6422 = ((node_2.a*_SpecularIntensity)*_SpecularMultiplier);
                float3 specularColor = float3(node_6422,node_6422,node_6422);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * ((_DiffuseColor.rgb*node_2.rgb*_DiffuseIntensity)*_DiffuseMultiplier);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * (tex2D(_AlphaMaskR,TRANSFORM_TEX(node_6473.rg, _AlphaMaskR)).r*tex2D(_AlphaMapROffset,TRANSFORM_TEX(node_6473.rg, _AlphaMapROffset)).r*_AlphaIntensity),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
