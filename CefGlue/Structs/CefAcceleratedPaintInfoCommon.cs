namespace Xilium.CefGlue;

using Xilium.CefGlue.Interop;

public class CefAcceleratedPaintInfoCommon
{
    internal static CefAcceleratedPaintInfoCommon FromNative(cef_accelerated_paint_info_common_t info)
    {
        return new CefAcceleratedPaintInfoCommon
        {
            Timestamp = info.timestamp,
            CodedSize = new CefSize(info.coded_size.width, info.coded_size.height),
            VisibleRectangle = new CefRectangle(info.visible_rect),
            ContentRectangle = new CefRectangle(info.content_rect),
            SourceSize = new CefSize(info.source_size.width, info.source_size.height),
            CaptureUpdateRectangle = new CefRectangle(info.capture_update_rect),
            RegionCaptureRectangle = new CefRectangle(info.region_capture_rect),
            CaptureCounter = info.capture_counter,
            HasCaptureUpdateRectangle = info.has_capture_update_rect != 0,
            HasRegionCaptureRectangle = info.has_region_capture_rect != 0,
            HasSourceSize = info.has_source_size != 0,
            HasCaptureCounter = info.has_capture_counter != 0,
            SurfaceId = info.surface_id,
            PoolSurfaceId = info.pool_surface_id,
            CaptureSessionId = info.capture_session_id,
        };
    }

    public ulong Timestamp { get; init; }
    public CefSize CodedSize { get; init; }
    public CefRectangle VisibleRectangle { get; init; }
    public CefRectangle ContentRectangle { get; init; }
    public CefSize SourceSize { get; init; }
    public CefRectangle CaptureUpdateRectangle { get; init; }
    public CefRectangle RegionCaptureRectangle { get; init; }

    public ulong CaptureCounter { get; init; }
    public bool HasCaptureUpdateRectangle { get; init; }
    public bool HasRegionCaptureRectangle;
    public bool HasSourceSize;
    public bool HasCaptureCounter;

    /// <summary>
    /// Opaque identifier for a leased surface, or 0 when no lease was granted.
    /// </summary>
    /// <remarks>
    /// While a lease is held the surface is not returned to the capture pool and its texture handle stays
    /// valid, so it may be sampled after the paint callback returns. Release it through
    /// <see cref="CefBrowserHost.ReleaseAcceleratedPaintSurface"/> once it is no longer in use: the pool is
    /// small, and holding leases stalls capture.
    /// </remarks>
    public ulong SurfaceId { get; init; }

    /// <summary>
    /// Identifier for the underlying pool surface, or 0 when unavailable.
    /// </summary>
    /// <remarks>
    /// STABLE across paints and NEVER REUSED: two paints carrying the same value are the same underlying
    /// texture, and a value once retired is not handed out again for a different one. So work done per surface,
    /// such as importing it into a graphics API and wrapping it in a texture object, can be done once and reused.
    /// <para>
    /// This is exactly what <see cref="SurfaceId"/> is NOT. That identifies a LEASE and is fresh on every
    /// paint, so anything cached against it is rebuilt every frame. Key per-surface state on this instead.
    /// </para>
    /// </remarks>
    public ulong PoolSurfaceId { get; init; }

    /// <summary>
    /// Identifies the capture session a paint belongs to, or 0 when unavailable.
    /// </summary>
    /// <remarks>
    /// Every paint of one session carries the same value, and it changes when capture is rebuilt, which happens
    /// on navigation and on resize. A new session's surfaces are NEW textures, so anything kept per surface from
    /// an older session can never be asked for again and should be released when this value changes.
    /// <para>
    /// Without it there is no way to tell "no more paints of that surface for now" from "that surface is gone",
    /// and keeping per-surface work for the second case costs a texture's worth of video memory per navigation.
    /// </para>
    /// </remarks>
    public ulong CaptureSessionId { get; init; }
}
