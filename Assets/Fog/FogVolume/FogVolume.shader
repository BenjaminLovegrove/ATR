Shader "Hidden/FogVolume"
{
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha   

        Cull Front  Lighting Off ZWrite Off  ZTest Always
	
        Pass
        {	
            CGPROGRAM
            #pragma multi_compile FOG_VOLUME_INSCATTERING_ON FOG_VOLUME_INSCATTERING_OFF
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma target 3.0
            
            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float4 _Color, _InscatteringColor, _BoxMin, _BoxMax;
            float3 L = float3(0, 0, 1);
            float _InscatteringIntensity=1, _InscateringExponent=2, _Visibility, InscatteringStartDistance = 100, InscatteringTransitionWideness = 500;

//http://www.cs.cornell.edu/courses/CS4620/2013fa/lectures/03raytracing1.pdf
//http://www.clockworkcoders.com/oglsl/rt/gpurt1.htm
          
 //float hitbox(Ray r, vec3 m1, vec3 m2, out float tmin, out float tmax) 
 float hitbox (float3 startpoint, float3 direction, float3 m1, float3 m2, inout float tmin, inout float tmax)
 {
   float tymin, tymax, tzmin, tzmax; 
   float flag = 1.0; 
 
    if (direction.x > 0) 
    {
       tmin = (m1.x - startpoint.x) / direction.x;
         tmax = (m2.x - startpoint.x) / direction.x;
    }
    else 
    {
       tmin = (m2.x - startpoint.x) / direction.x;
       tmax = (m1.x - startpoint.x) / direction.x;
    }
    if (direction.y > 0) 
    {
       tymin = (m1.y - startpoint.y) / direction.y; 
       tymax = (m2.y - startpoint.y) / direction.y; 
    }
    else 
    {
       tymin = (m2.y - startpoint.y) / direction.y; 
       tymax = (m1.y - startpoint.y) / direction.y; 
    }
     
    if ((tmin > tymax) || (tymin > tmax)) flag = -1.0; 
    if (tymin > tmin) tmin = tymin; 
    if (tymax < tmax) tmax = tymax; 
      
    if (direction.z > 0) 
    {
       tzmin = (m1.z - startpoint.z) / direction.z; 
       tzmax = (m2.z - startpoint.z) / direction.z; 
    }
    else 
    {
       tzmin = (m2.z - startpoint.z) / direction.z; 
       tzmax = (m1.z - startpoint.z) / direction.z; 
    }
    if ((tmin > tzmax) || (tzmin > tmax)) flag = -1.0; 
    if (tzmin > tmin) tmin = tzmin; 
    if (tzmax < tmax) tmax = tzmax; 
      
    return (flag > 0); 
 }            
            struct v2f
            {
                float4 pos         : SV_POSITION;
                float3 Wpos        : TEXCOORD0;
                float4 ScreenUVs   : TEXCOORD1;
                float3 LocalPos    : TEXCOORD2;
                float3 ViewPos     : TEXCOORD3;
                float3 LocalEyePos : TEXCOORD4;                
            };
    
            v2f vert (appdata_full i)
            {
                v2f o;
				
				o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
				o.Wpos.xyz = mul((float4x4)_Object2World, float4(i.vertex.xyz, 1)).xyz;
				o.ScreenUVs = ComputeScreenPos(o.pos);
				o.ViewPos = mul((float4x4)UNITY_MATRIX_MV, float4(i.vertex.xyz, 1)).xyz;
                o.LocalPos = i.vertex.xyz;
                o.LocalEyePos = mul((float4x4)_World2Object, (float4(_WorldSpaceCameraPos, 1))).xyz;           

                return o;
            }

            float4 frag (v2f i) : COLOR
            {                
                float3 direction = normalize(i.LocalPos - i.LocalEyePos);
                float tmin, tmax;

                float Volume = hitbox(i.LocalEyePos, direction, _BoxMin, _BoxMax, tmin, tmax);
                
                // tmin must be 0 when inside the volume
                int Inside[3] = {0, 0, 0}, bOutside;

                Inside[0] = step(0, abs(i.LocalEyePos.x) - _BoxMax.x);
                Inside[1] = step(0, abs(i.LocalEyePos.y) - _BoxMax.y);
                Inside[2] = step(0, abs(i.LocalEyePos.z) - _BoxMax.z);
                
                bOutside  = min(1,(float)(Inside[0] + Inside[1] + Inside[2]));
            			
				tmin*=bOutside;

                float Depth =  length(DECODE_EYEDEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.ScreenUVs)).r )/normalize(i.ViewPos).z);
    	
                float MinMax[2] = {max(tmin, tmax), min(tmin, tmax)};
				
                float thickness = min(MinMax[0], Depth) - min(MinMax[1], Depth);

                 
                float Fog = 1-exp2(-(thickness) / _Visibility) * Volume ;                
				//There is an artifact when AA is enabled, so I scaled the volume size a bit. That makes the edge harder, so I have to fix it here:
				Fog*=Fog;
				float4 Final;
				
                #if FOG_VOLUME_INSCATTERING_ON
                //Inscattering
                float3 CameraWSdir = normalize(i.Wpos - _WorldSpaceCameraPos); 
                float Inscattering = pow(max(0, dot(L, CameraWSdir)), _InscateringExponent);
                //clamp by distance:
                Inscattering *= saturate( Depth/InscatteringTransitionWideness- InscatteringStartDistance);   
                Final = float4(_Color.rgb + _InscatteringColor * _InscatteringIntensity * Inscattering, saturate(Fog * _Color.a));    
                #else
                Final = float4(_Color.rgb , saturate(Fog * _Color.a));                   
                #endif
                
                return Final;
            }
            ENDCG
        }
       
	} 
	
}
