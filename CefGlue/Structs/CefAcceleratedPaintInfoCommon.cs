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
}
