#ifndef __RAY_H__
#define __RAY_H__

#include "material.hlsl"

struct Ray
{
    float3 origin;
    float3 direction;
    float distance;
};

struct RayHit
{
    float t;
    float3 position;
    float3 normal;

    Material material;
};

Ray CreateRay(float3 origin, float3 direction, float distance = 1000.0f)
{
    Ray ray;
    ray.origin = origin;
    ray.direction = direction;
    ray.distance = distance;
    return ray;
}

float3 EvaluateRay(Ray r, float t)
{
    return r.origin + t * r.direction;
}

#endif // #ifndef __RAY_H__