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
float4 EdgeColor;
float4 BodyColor;
float Thickness;
//float2 Center1;
//float Radius1;
//bool SecondCircle;
//float2 Center2;
//float Radius2;

//Max is 3 with the current algorithm
//Changing this requires changing the method used for populating the parameters accordingly
const int Amount = 2;
float2 Centers[2];
float Radii[2];

float4 MainPS(float4 coords : TEXCOORD0) : COLOR0
{
	//Don't draw if:
	// * Outside of every circle: transparent color
	// * Inside of a circle: body color
	
	//Draw if:
	// * On an outline: save edge color, return if no other circle overwrites it with body color
	
	// Current pixel the shader operates on
    float2 coords2d = coords.xy;
	
	// Color that is being drawn
    float4 color = float4(0, 0, 0, 0);
	
    for (int i = 0; i < Amount; i++)
    {
        float radius = Radii[i];
        if (radius <= 0)
            continue;
		
        float distFromCenter = distance(ScreenPos + (ScreenDim * coords2d), Centers[i]);
        if (distFromCenter < radius)
        {
            if (distFromCenter > radius - Thickness)
            {
				//In outline of this circle: set color, but keep checking other circles
                color = EdgeColor;
            }
            else
            {
				//In body: return straight up
                return BodyColor;
            }
        }
		//Outside of this circle, keep checking other circles
    }
    return color;
	
//PREVIOUS APPROACH
//#########
	
    //Default transparent color
 //   float4 color = float4(0, 0, 0, 0);
	
	//float2 coords2d = coords.xy;
	//float distFromCenter1 = distance(ScreenPos + (ScreenDim * coords2d), Center1);
	//if (distFromCenter1 > Radius1)
 //   {
 //       if (SecondCircle)
 //       {
 //           float distFromCenter2 = distance(ScreenPos + (ScreenDim * coords2d), Center2);
 //           if (distFromCenter2 > Radius2)
 //           {
 //               return color;
 //           }
 //       }
 //       else
 //       {
 //           return color;
 //       }
 //   }
	
	//float distToEdge1 = Radius1 - distFromCenter1;

 //   if (distToEdge1 < Thickness)
 //   {
 //       if (SecondCircle)
 //       {
 //           float distFromCenter2 = distance(ScreenPos + (ScreenDim * coords2d), Center2);
 //           float distToEdge2 = Radius2 - distFromCenter2;
 //           if (distToEdge2 < Thickness)
 //           {
 //               color = EdgeColor;
 //               return color;
 //           }
 //       }
 //       else
 //       {
 //           color = EdgeColor;
 //           return color;
 //       }
	//}
	
	//color = BodyColor;
	//return color;
}

technique CircleDraw
{
	pass P0
	{
		PixelShader = compile ps_2_0 MainPS();
	}
};
