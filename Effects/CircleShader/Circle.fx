//Credit to hamstar for this shader

#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif


sampler2D SpriteTextureSampler;
//Texture2D SpriteTexture
//sampler2D SpriteTextureSampler = sampler_state {
//	Texture = SpriteTexture;
//};

float2 ScreenPos;
float2 ScreenDim;
float2 EntCenter;
float4 EdgeColor;
float4 BodyColor;
float Radius;
float HpPercent;
float ShrinkResistScale;


float PseudoRNG( in float2 uv ) {
    float2 noise = ( frac( sin( dot( uv ,float2(12.9898, 78.233)*2.0 ) ) * 43758.5453 ) );
    return abs(noise.x + noise.y) * 0.5;
}


float4 MainPS( float4 coords: TEXCOORD0 ) : COLOR0 {
	float2 coords2d = coords.xy;
	float distFromCenter = distance( ScreenPos + (ScreenDim * coords2d), EntCenter );
	if( distFromCenter > Radius ) {
		return float4(0,0,0,0);
	}
	
	float rand = PseudoRNG( coords2d );
	float percentToRadius = distFromCenter / Radius;
	float distToEdge = Radius - distFromCenter;
	float stability = 1 - (rand * (1 - HpPercent));
	
    //float4 color = tex2d( spritetexturesampler, coords );
	float4 color;

	if( distToEdge < (ShrinkResistScale * 24) ) {
		color = EdgeColor * stability;
	} else {
		//float intensity = 1 - (float)sqrt( 1 - (percentToRadius * percentToRadius) );
		float intensity = 1 - (float)sqrt( 1 - (percentToRadius * percentToRadius) );

		color = lerp( float4(0,0,0,0), BodyColor, (0.15 + (intensity * 0.65)) * stability );
	}
	
	return color;
}

technique BarrierDraw {
	pass P0 {
		PixelShader = compile ps_2_0 MainPS();
	}
};
