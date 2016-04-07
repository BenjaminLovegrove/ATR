// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:30704,y:32694|diff-42-OUT,spec-4212-OUT,normal-6496-OUT,amdfl-4348-OUT,amspl-4355-OUT,alpha-6550-OUT,refract-6489-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:31517,y:32457,ptlb:Diffuse(RGB) Spec(A),ptin:_DiffuseRGBSpecA,tex:14bb96bd6f2891147a98ab0bd1662452,ntxv:0,isnm:False|UVIN-4245-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8,x:31471,y:33454,ptlb:Alpha Map 01,ptin:_AlphaMap01,tex:40b820fc45d4f934f8242baed9dda686,ntxv:0,isnm:False|UVIN-14-UVOUT;n:type:ShaderForge.SFN_Panner,id:14,x:31656,y:33454,spu:0.5,spv:0.5|UVIN-15-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:15,x:31860,y:33454,uv:0;n:type:ShaderForge.SFN_Multiply,id:42,x:31283,y:32504|A-43-RGB,B-2-RGB,C-4219-OUT;n:type:ShaderForge.SFN_Color,id:43,x:31517,y:32288,ptlb:Diffuse Color,ptin:_DiffuseColor,glob:False,c1:0,c2:0.6689658,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:171,x:31460,y:33819,ptlb:Alpha Intensity,ptin:_AlphaIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_VertexColor,id:4184,x:30987,y:33813;n:type:ShaderForge.SFN_Multiply,id:4212,x:31299,y:32684|A-2-A,B-4213-OUT;n:type:ShaderForge.SFN_Slider,id:4213,x:31506,y:32754,ptlb:Specular Intensity,ptin:_SpecularIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4219,x:31506,y:32646,ptlb:Color Intensity,ptin:_ColorIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Panner,id:4245,x:31969,y:32457,spu:0.5,spv:1|UVIN-4247-UVOUT,DIST-4259-OUT;n:type:ShaderForge.SFN_TexCoord,id:4247,x:32194,y:32457,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:4249,x:32519,y:32804,ptlb:Diffuse Min Speed,ptin:_DiffuseMinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4251,x:32519,y:32897,ptlb:Diffuse Max Speed,ptin:_DiffuseMaxSpeed,glob:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:4253,x:32272,y:32833|A-4249-OUT,B-4251-OUT,T-4255-OUT;n:type:ShaderForge.SFN_Slider,id:4255,x:32493,y:32989,ptlb:Diffuse Panning Speed,ptin:_DiffusePanningSpeed,min:0,cur:0.3909774,max:1;n:type:ShaderForge.SFN_Time,id:4257,x:32519,y:32657;n:type:ShaderForge.SFN_Multiply,id:4259,x:32192,y:32614|A-4257-TSL,B-4253-OUT;n:type:ShaderForge.SFN_Multiply,id:4348,x:31327,y:32848|A-4350-OUT,B-4352-RGB;n:type:ShaderForge.SFN_Multiply,id:4350,x:31520,y:32848|A-4364-OUT,B-4352-A;n:type:ShaderForge.SFN_Cubemap,id:4352,x:31715,y:32971,ptlb:Diffuse IBL (Cubemap),ptin:_DiffuseIBLCubemap,cube:17af610165f1ddd4da62635fbc8ccd42,pvfc:0|DIR-4353-OUT;n:type:ShaderForge.SFN_NormalVector,id:4353,x:31921,y:32952,pt:True;n:type:ShaderForge.SFN_Multiply,id:4355,x:31499,y:33149|A-4357-OUT,B-4361-RGB;n:type:ShaderForge.SFN_Multiply,id:4357,x:31715,y:33149|A-4654-OUT,B-4361-A;n:type:ShaderForge.SFN_Cubemap,id:4361,x:31911,y:33253,ptlb:Spec IBL (Cubemap),ptin:_SpecIBLCubemap,cube:e4da8687ffd53e84bbda4e43c9842c32,pvfc:1|DIR-4655-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4364,x:31715,y:32872,ptlb:Diffuse IBL Intensity,ptin:_DiffuseIBLIntensity,glob:False,v1:5;n:type:ShaderForge.SFN_ValueProperty,id:4654,x:31911,y:33149,ptlb:Spec IBL Intensity,ptin:_SpecIBLIntensity,glob:False,v1:8;n:type:ShaderForge.SFN_ViewReflectionVector,id:4655,x:32091,y:33253;n:type:ShaderForge.SFN_Panner,id:4660,x:31700,y:34043,spu:0,spv:1|UVIN-4662-UVOUT,DIST-4674-OUT;n:type:ShaderForge.SFN_TexCoord,id:4662,x:31925,y:34043,uv:0;n:type:ShaderForge.SFN_ValueProperty,id:4664,x:32252,y:34383,ptlb:Alpha Map 02 Min Speed,ptin:_AlphaMap02MinSpeed,glob:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4666,x:32252,y:34476,ptlb:Alpha Map 02 Max Speed,ptin:_AlphaMap02MaxSpeed,glob:False,v1:20;n:type:ShaderForge.SFN_Lerp,id:4668,x:32005,y:34412|A-4664-OUT,B-4666-OUT,T-4670-OUT;n:type:ShaderForge.SFN_Slider,id:4670,x:32226,y:34568,ptlb:Alpha Map 02 Panning Speed,ptin:_AlphaMap02PanningSpeed,min:0,cur:0.5002657,max:1;n:type:ShaderForge.SFN_Time,id:4672,x:32252,y:34236;n:type:ShaderForge.SFN_Multiply,id:4674,x:31925,y:34193|A-4672-TSL,B-4668-OUT;n:type:ShaderForge.SFN_Tex2d,id:4675,x:31486,y:34043,ptlb:Alpha Map 02 (RGB) Offset Y,ptin:_AlphaMap02RGBOffsetY,tex:07a25d7126230441e8b386ae5298a96a,ntxv:0,isnm:False|UVIN-4660-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4692,x:31225,y:33611|A-8-R,B-4675-R,C-171-OUT;n:type:ShaderForge.SFN_Multiply,id:6489,x:32314,y:33394|A-6490-OUT,B-6492-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6490,x:32569,y:33394,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6495-OUT;n:type:ShaderForge.SFN_Multiply,id:6492,x:32569,y:33608|A-6494-OUT,B-6493-OUT;n:type:ShaderForge.SFN_Vector1,id:6493,x:32801,y:33702,v1:0.2;n:type:ShaderForge.SFN_Slider,id:6494,x:32801,y:33607,ptlb:Refraction Intensity,ptin:_RefractionIntensity,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:6495,x:32801,y:33394|A-6498-RGB,B-6499-RGB;n:type:ShaderForge.SFN_Lerp,id:6496,x:32519,y:33118|A-6497-OUT,B-6495-OUT,T-6494-OUT;n:type:ShaderForge.SFN_Vector3,id:6497,x:33123,y:33133,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Tex2d,id:6498,x:33212,y:33396,ptlb:Normal 01,ptin:_Normal01,tex:2dd3788f8589b40bf82a92d76ffc5091,ntxv:3,isnm:True|UVIN-6500-OUT;n:type:ShaderForge.SFN_Tex2d,id:6499,x:33212,y:33593,ptlb:Normal 02,ptin:_Normal02,tex:fb6566c21f717904f83743a5a76dd0b0,ntxv:3,isnm:True|UVIN-6502-OUT;n:type:ShaderForge.SFN_Add,id:6500,x:33540,y:33384|A-6505-UVOUT,B-6503-OUT;n:type:ShaderForge.SFN_Add,id:6502,x:33538,y:33593|A-6506-UVOUT,B-6504-OUT;n:type:ShaderForge.SFN_Multiply,id:6503,x:34019,y:33376|A-6507-OUT,B-6509-T;n:type:ShaderForge.SFN_Multiply,id:6504,x:34024,y:33766|A-6508-OUT,B-6510-T;n:type:ShaderForge.SFN_TexCoord,id:6505,x:34019,y:33223,uv:0;n:type:ShaderForge.SFN_TexCoord,id:6506,x:34024,y:33588,uv:0;n:type:ShaderForge.SFN_Append,id:6507,x:34268,y:33176|A-6511-OUT,B-6514-OUT;n:type:ShaderForge.SFN_Append,id:6508,x:34277,y:33636|A-6516-OUT,B-6518-OUT;n:type:ShaderForge.SFN_Time,id:6509,x:34268,y:33333;n:type:ShaderForge.SFN_Time,id:6510,x:34277,y:33792;n:type:ShaderForge.SFN_ValueProperty,id:6511,x:34507,y:33197,ptlb:Normal 01 X Speed,ptin:_Normal01XSpeed,glob:False,v1:0.02;n:type:ShaderForge.SFN_ValueProperty,id:6514,x:34507,y:33303,ptlb:Normal 01 Y Speed,ptin:_Normal01YSpeed,glob:False,v1:0.05;n:type:ShaderForge.SFN_ValueProperty,id:6516,x:34517,y:33654,ptlb:Normal 02 X Speed,ptin:_Normal02XSpeed,glob:False,v1:-0.02;n:type:ShaderForge.SFN_ValueProperty,id:6518,x:34517,y:33760,ptlb:Normal 02 Y Speed,ptin:_Normal02YSpeed,glob:False,v1:-0.05;n:type:ShaderForge.SFN_Multiply,id:6550,x:30711,y:33674|A-6714-OUT,B-4184-G;n:type:ShaderForge.SFN_Add,id:6714,x:30987,y:33596|A-4692-OUT,B-6715-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6715,x:31237,y:33773,ptlb:Alpha Multiplier,ptin:_AlphaMultiplier,glob:False,v1:0.5;proporder:43-4219-4213-2-4249-4251-4255-171-4675-4664-4666-4670-8-4352-4361-4364-4654-6494-6498-6499-6511-6514-6516-6518-6715;pass:END;sub:END;*/

Shader "Shader Forge/DiffuseSpecNormOffset_AlphaVtxClrMlti_AlphaOffset_IBL" {
    Properties {
        _DiffuseColor ("Diffuse Color", Color) = (0,0.6689658,1,1)
        _ColorIntensity ("Color Intensity", Range(0, 1)) = 1
        _SpecularIntensity ("Specular Intensity", Range(0, 1)) = 1
        _DiffuseRGBSpecA ("Diffuse(RGB) Spec(A)", 2D) = "white" {}
        _DiffuseMinSpeed ("Diffuse Min Speed", Float ) = 0
        _DiffuseMaxSpeed ("Diffuse Max Speed", Float ) = 20
        _DiffusePanningSpeed ("Diffuse Panning Speed", Range(0, 1)) = 0.3909774
        _AlphaIntensity ("Alpha Intensity", Range(0, 1)) = 1
        _AlphaMap02RGBOffsetY ("Alpha Map 02 (RGB) Offset Y", 2D) = "white" {}
        _AlphaMap02MinSpeed ("Alpha Map 02 Min Speed", Float ) = 0
        _AlphaMap02MaxSpeed ("Alpha Map 02 Max Speed", Float ) = 20
        _AlphaMap02PanningSpeed ("Alpha Map 02 Panning Speed", Range(0, 1)) = 0.5002657
        _AlphaMap01 ("Alpha Map 01", 2D) = "white" {}
        _DiffuseIBLCubemap ("Diffuse IBL (Cubemap)", Cube) = "_Skybox" {}
        _SpecIBLCubemap ("Spec IBL (Cubemap)", Cube) = "_Skybox" {}
        _DiffuseIBLIntensity ("Diffuse IBL Intensity", Float ) = 5
        _SpecIBLIntensity ("Spec IBL Intensity", Float ) = 8
        _RefractionIntensity ("Refraction Intensity", Range(0, 1)) = 1
        _Normal01 ("Normal 01", 2D) = "bump" {}
        _Normal02 ("Normal 02", 2D) = "bump" {}
        _Normal01XSpeed ("Normal 01 X Speed", Float ) = 0.02
        _Normal01YSpeed ("Normal 01 Y Speed", Float ) = 0.05
        _Normal02XSpeed ("Normal 02 X Speed", Float ) = -0.02
        _Normal02YSpeed ("Normal 02 Y Speed", Float ) = -0.05
        _AlphaMultiplier ("Alpha Multiplier", Float ) = 0.5
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
            uniform sampler2D _AlphaMap01; uniform float4 _AlphaMap01_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _SpecularIntensity;
            uniform float _ColorIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform samplerCUBE _DiffuseIBLCubemap;
            uniform samplerCUBE _SpecIBLCubemap;
            uniform float _DiffuseIBLIntensity;
            uniform float _SpecIBLIntensity;
            uniform float _AlphaMap02MinSpeed;
            uniform float _AlphaMap02MaxSpeed;
            uniform float _AlphaMap02PanningSpeed;
            uniform sampler2D _AlphaMap02RGBOffsetY; uniform float4 _AlphaMap02RGBOffsetY_ST;
            uniform float _RefractionIntensity;
            uniform sampler2D _Normal01; uniform float4 _Normal01_ST;
            uniform sampler2D _Normal02; uniform float4 _Normal02_ST;
            uniform float _Normal01XSpeed;
            uniform float _Normal01YSpeed;
            uniform float _Normal02XSpeed;
            uniform float _Normal02YSpeed;
            uniform float _AlphaMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float4 node_6509 = _Time + _TimeEditor;
                float2 node_6500 = (i.uv0.rg+(float2(_Normal01XSpeed,_Normal01YSpeed)*node_6509.g));
                float4 node_6510 = _Time + _TimeEditor;
                float2 node_6502 = (i.uv0.rg+(float2(_Normal02XSpeed,_Normal02YSpeed)*node_6510.g));
                float3 node_6495 = (UnpackNormal(tex2D(_Normal01,TRANSFORM_TEX(node_6500, _Normal01))).rgb+UnpackNormal(tex2D(_Normal02,TRANSFORM_TEX(node_6502, _Normal02))).rgb);
                float3 normalLocal = lerp(float3(0,0,1),node_6495,_RefractionIntensity);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_6495.rg*(_RefractionIntensity*0.2));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
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
                float4 node_4361 = texCUBE(_SpecIBLCubemap,viewReflectDirection);
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_4245 = (i.uv0.rg+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0.5,1));
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_4245, _DiffuseRGBSpecA));
                float node_4212 = (node_2.a*_SpecularIntensity);
                float3 specularColor = float3(node_4212,node_4212,node_4212);
                float3 specularAmb = ((_SpecIBLIntensity*node_4361.a)*node_4361.rgb) * specularColor;
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor + specularAmb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float4 node_4352 = texCUBE(_DiffuseIBLCubemap,normalDirection);
                diffuseLight += ((_DiffuseIBLIntensity*node_4352.a)*node_4352.rgb); // Diffuse Ambient Light
                finalColor += diffuseLight * (_DiffuseColor.rgb*node_2.rgb*_ColorIntensity);
                finalColor += specular;
                float4 node_6858 = _Time + _TimeEditor;
                float2 node_15 = i.uv0;
                float2 node_14 = (node_15.rg+node_6858.g*float2(0.5,0.5));
                float4 node_4672 = _Time + _TimeEditor;
                float2 node_4660 = (i.uv0.rg+(node_4672.r*lerp(_AlphaMap02MinSpeed,_AlphaMap02MaxSpeed,_AlphaMap02PanningSpeed))*float2(0,1));
                float node_4692 = (tex2D(_AlphaMap01,TRANSFORM_TEX(node_14, _AlphaMap01)).r*tex2D(_AlphaMap02RGBOffsetY,TRANSFORM_TEX(node_4660, _AlphaMap02RGBOffsetY)).r*_AlphaIntensity);
/// Final Color:
                return fixed4(lerp(sceneColor.rgb, finalColor,((node_4692+_AlphaMultiplier)*i.vertexColor.g)),1);
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
            uniform sampler2D _AlphaMap01; uniform float4 _AlphaMap01_ST;
            uniform float4 _DiffuseColor;
            uniform float _AlphaIntensity;
            uniform float _SpecularIntensity;
            uniform float _ColorIntensity;
            uniform float _DiffuseMinSpeed;
            uniform float _DiffuseMaxSpeed;
            uniform float _DiffusePanningSpeed;
            uniform float _AlphaMap02MinSpeed;
            uniform float _AlphaMap02MaxSpeed;
            uniform float _AlphaMap02PanningSpeed;
            uniform sampler2D _AlphaMap02RGBOffsetY; uniform float4 _AlphaMap02RGBOffsetY_ST;
            uniform float _RefractionIntensity;
            uniform sampler2D _Normal01; uniform float4 _Normal01_ST;
            uniform sampler2D _Normal02; uniform float4 _Normal02_ST;
            uniform float _Normal01XSpeed;
            uniform float _Normal01YSpeed;
            uniform float _Normal02XSpeed;
            uniform float _Normal02YSpeed;
            uniform float _AlphaMultiplier;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(6,7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float4 node_6509 = _Time + _TimeEditor;
                float2 node_6500 = (i.uv0.rg+(float2(_Normal01XSpeed,_Normal01YSpeed)*node_6509.g));
                float4 node_6510 = _Time + _TimeEditor;
                float2 node_6502 = (i.uv0.rg+(float2(_Normal02XSpeed,_Normal02YSpeed)*node_6510.g));
                float3 node_6495 = (UnpackNormal(tex2D(_Normal01,TRANSFORM_TEX(node_6500, _Normal01))).rgb+UnpackNormal(tex2D(_Normal02,TRANSFORM_TEX(node_6502, _Normal02))).rgb);
                float3 normalLocal = lerp(float3(0,0,1),node_6495,_RefractionIntensity);
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_6495.rg*(_RefractionIntensity*0.2));
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
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_4257 = _Time + _TimeEditor;
                float2 node_4245 = (i.uv0.rg+(node_4257.r*lerp(_DiffuseMinSpeed,_DiffuseMaxSpeed,_DiffusePanningSpeed))*float2(0.5,1));
                float4 node_2 = tex2D(_DiffuseRGBSpecA,TRANSFORM_TEX(node_4245, _DiffuseRGBSpecA));
                float node_4212 = (node_2.a*_SpecularIntensity);
                float3 specularColor = float3(node_4212,node_4212,node_4212);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_DiffuseColor.rgb*node_2.rgb*_ColorIntensity);
                finalColor += specular;
                float4 node_6859 = _Time + _TimeEditor;
                float2 node_15 = i.uv0;
                float2 node_14 = (node_15.rg+node_6859.g*float2(0.5,0.5));
                float4 node_4672 = _Time + _TimeEditor;
                float2 node_4660 = (i.uv0.rg+(node_4672.r*lerp(_AlphaMap02MinSpeed,_AlphaMap02MaxSpeed,_AlphaMap02PanningSpeed))*float2(0,1));
                float node_4692 = (tex2D(_AlphaMap01,TRANSFORM_TEX(node_14, _AlphaMap01)).r*tex2D(_AlphaMap02RGBOffsetY,TRANSFORM_TEX(node_4660, _AlphaMap02RGBOffsetY)).r*_AlphaIntensity);
/// Final Color:
                return fixed4(finalColor * ((node_4692+_AlphaMultiplier)*i.vertexColor.g),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
