#ifndef __MATERIAL_H__
#define __MATERIAL_H__

struct Material
{
    uint index;
    float3 albedo;
};

#define MATERIALINDEX_LAMBERT 0
#define MATERIALINDEX_DIELECTRIC 1
#define MATERIALINDEX_METALLIC 2

bool LambertScatter(Material material, Ray ray, RayHit hit, inout float3 attenuation, inout Ray scattered)
{
    // TODO: Replace seed with something other than 0 (from cpu?)
    float3 target = hit.position + hit.normal + RandomInUnitSphere(0);
    scattered = CreateRay(hit.position, target - hit.position);
    attenuation = material.albedo;
    return true;
}

// Lambert material scatter
bool MaterialScatter(Material material, Ray ray, RayHit hit, inout float3 attenuation, inout Ray scattered)
{
    [branch] switch (material.index)
    {
    case MATERIALINDEX_LAMBERT:
        return LambertScatter(material, ray, hit, attenuation, scattered);
    default:
        return false;
    }
}

#endif // #ifndef __MATERIAL_H__