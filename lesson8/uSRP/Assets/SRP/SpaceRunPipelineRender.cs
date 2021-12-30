using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class SpaceRunPipelineRender : RenderPipeline
{
    private ScriptableRenderContext _context;
    private Camera _camera;

    private readonly CommandBuffer _commandBuffer = new CommandBuffer { name = bufferName };
    private const string bufferName = "My Camera Render";

    private CullingResults _cullingResults;

    private static readonly List<ShaderTagId> drawingShaderTagIds = new List<ShaderTagId>
    {
        new ShaderTagId("SRPDefaultUnlit"),
    };

#if UNITY_EDITOR
    private static readonly ShaderTagId[] _legasyShaderTagIds =
        {
        new ShaderTagId("Always"),
        new ShaderTagId("ForwardBase"),
        new ShaderTagId("PrepassBase"),
        new ShaderTagId("Vertex"),
        new ShaderTagId("VertexLMRGBM"),
        new ShaderTagId("VertexLM")
    };
    private static Material _errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));

    void DrawUnsupportedShaders()
    {
        var drawingSettings = new DrawingSettings(_legasyShaderTagIds[0], new SortingSettings(_camera))
        {
            overrideMaterial = _errorMaterial,
        };

        for (var i = 1; i < _legasyShaderTagIds.Length; i++)
            drawingSettings.SetShaderPassName(i, _legasyShaderTagIds[i]);

        var filteringSettings = FilteringSettings.defaultValue;

        _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
    }
#endif

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        _context = context;
        foreach (var camera in cameras)
            Render(context, camera);
    }

    private void Render(ScriptableRenderContext context, Camera camera)
    {
        _camera = camera;

        if (!camera.TryGetCullingParameters(out var parameters))
            return;

        //Settings
        _cullingResults = _context.Cull(ref parameters);

        _context.SetupCameraProperties(_camera);

        _commandBuffer.ClearRenderTarget(true, true, Color.clear);
        _commandBuffer.BeginSample(bufferName);
        _context.ExecuteCommandBuffer(_commandBuffer);
        _commandBuffer.Clear();

        //DrawVisible
        var drawingSettings = CreateDrawingSettings(drawingShaderTagIds, SortingCriteria.CommonOpaque, out var sortingSettings);
        var filteringSettings = new FilteringSettings(RenderQueueRange.all);

        _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
        _context.DrawSkybox(_camera);

        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;

        _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);

        DrawUnsupportedShaders();
        DrawGizmos();

        //Submit
        //_commandBuffer.EndSample(bufferName);
        _context.ExecuteCommandBuffer(_commandBuffer);
        _commandBuffer.Clear();
        _context.Submit();
    }


    private DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTags, SortingCriteria sortingCriteria, out SortingSettings sortingSettings)
    {
        sortingSettings = new SortingSettings(_camera)
        {
            criteria = sortingCriteria,
        };

        var drawingSettings = new DrawingSettings(shaderTags[0], sortingSettings);

        for (var i = 1; i < shaderTags.Count; i++)
            drawingSettings.SetShaderPassName(i, shaderTags[i]);


        return drawingSettings;
    }

    void DrawGizmos()
    {
        if (!Handles.ShouldRenderGizmos())
            return;

        _context.DrawGizmos(_camera, GizmoSubset.PreImageEffects);
        _context.DrawGizmos(_camera, GizmoSubset.PostImageEffects);
    }
}
