Shader "Custom/TornadoShader1"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _EmissionValue ("Emission Value", Range(0,5)) = 1
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        _SinTex ("SinTex", 2D) = "white" {}
        _XVel ("xVel", Range(-3,3)) = 0.5
        _YVel ("yVel", Range(-3,3)) = 0.0
        _WavesAmount ("wavesAmount", Range(0,5)) = 3
        _WavesValue ("wavesValue", Range(0,5)) = 2
        _Transparency ("Transparency", Range(0,3)) = 0.0
        _Cutoff("Cut off", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { 
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType"="TransparentCutout" 
        }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:blend
	    #pragma vertex vert
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
sampler2D _NoiseTex;
sampler2D _SinTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
            float3 worldNormal;
            float3 viewDir;
        };

        float _YVel;
        float _Cutoff;
        float _XVel;
        float _Transparency;
        float _WavesAmount;
        float _WavesValue;
        float _EmissionValue;
        float4 _Color;
        float4 _EmissionColor;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 UVMovement = IN.uv_NoiseTex;
            float dx = _Time.y * _XVel;
            float dy = -_Time.y * _YVel ;
            UVMovement += float2(dx,dy); 
            // Albedo comes from a texture tinted by color
            fixed4 c = (tex2D (_NoiseTex, UVMovement) + 0.2) * _Color;
            
            float bordes = abs(dot(IN.worldNormal, IN.viewDir));
            /*  if(bordes < 0.35)
            {
                bordes = 1;
            }else
            {
                bordes = 0;
            }*/
            o.Emission =  pow((1- bordes ),3) * _EmissionColor * _EmissionValue + (pow(c,5)* 20); 
            o.Albedo = c.rgb ;
            o.Alpha = saturate( c.r * _Transparency)  ;
            clip(o.Alpha-_Cutoff);
        }
        void vert(inout appdata_full v,out Input IN)
        {
        UNITY_INITIALIZE_OUTPUT(Input, IN);

        float2 UVMovement = v.texcoord.xy;
        float waveSpeed = -_Time.y * _YVel * 4;

        UVMovement += float2(0,waveSpeed); 
        float time = 0; 
        time = time + _Time.y;
        float4 m = tex2Dlod(_SinTex, float4(UVMovement, 0, 0));
        v.vertex.xyz = float3(v.vertex.x  ,v.vertex.y,v.vertex.z) + (v.normal /** (1 + sin(time * 2))*/ * _WavesValue * m.r* v.vertex.z);
        
        }
        ENDCG
    }
    FallBack "Diffuse"
}
