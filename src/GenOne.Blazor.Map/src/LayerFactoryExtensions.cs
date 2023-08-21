﻿using GenOne.DPBlazorMapLibrary.Factorys;
using GenOne.DPBlazorMapLibrary.Models.Basics;
using GenOne.DPBlazorMapLibrary.Models.Layers.Markers;

namespace GenOne.Blazor.Map
{
    internal static class LayerFactoryExtensions
    {
        internal static async Task<Marker?> AddOrUpdateMarker(this LayerFactory layerFactory, Marker? existingMarker, DPBlazorMapLibrary.Components.Map.Map? map, LatLng? location, Func<Task<MarkerOptions>> markerFactory)
        {
            if (location is null)
            {
                if (existingMarker is not null)
                    await existingMarker.Remove();

            } else if (existingMarker is null)
            {
                if (map is null)
                    return existingMarker;

                var markerOptions = await markerFactory();

                existingMarker = await layerFactory.CreateMarkerAndAddToMap(location, map, markerOptions);
            } else
            {
                await existingMarker.SetLatLng(location);
            }

            return existingMarker;
        }
    }
}
