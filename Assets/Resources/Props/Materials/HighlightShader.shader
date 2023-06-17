Shader "Custom/HighlightShader"
{
    Properties
    {
        _Colour ("Colour", Color) = (1,1,1,1)
        _HighlightColour ("Highlight Colour", Color) = (1,1,1,1)
        _Glow ("Intensity", Range(0, 3)) = 1
        _LastHighlightTime ("Last Highlight Time", float) = 0
        _HighlightFadeTime ("Highlight Fade Time", float) = 0.3
        _HighlightFadeDelay ("Highlight Fade Delay", float) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        struct Input {
            float4 pos;    
        };
        fixed4 _Colour;
        half _Glow;
        float4 _HighlightColour;
        float _LastHighlightTime;
        float _HighlightFadeTime;
        float _HighlightFadeDelay;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float lerpVal = max(0, min(1, (_Time.y - _LastHighlightTime - _HighlightFadeDelay) / _HighlightFadeTime));
            float4 lerpedColour = lerp(_HighlightColour, _Colour, lerpVal);
            o.Albedo = lerpedColour.rgb * _Glow;
            o.Alpha = lerpedColour.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
