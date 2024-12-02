Shader "Custom/Fade"
{
      Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MinDistance ("Min Distance", Float) = 1.0
        _MaxDistance ("Max Distance", Float) = 10.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        float _MinDistance;
        float _MaxDistance;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Texture color
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            
            // Distance from camera to object
            float distance = length(_WorldSpaceCameraPos - IN.worldPos);
            
            // Calculate alpha based on distance
            float alpha = saturate((distance - _MinDistance) / (_MaxDistance - _MinDistance));
            
            o.Albedo = c.rgb;
            o.Alpha = alpha * c.a;
        }
        ENDCG
    }
    FallBack "Transparent/Cutout/VertexLit"
}
