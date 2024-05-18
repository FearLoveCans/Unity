using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public RawImage videoScreen;     // 用于显示视频帧的UI RawImage对象
    public VideoPlayer videoPlayer;  // 视频播放器组件

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();  // 获取视频播放器组件
        videoPlayer.Play();  // 播放视频

        // 创建一个新的RenderTexture，用于渲染视频帧
        videoPlayer.targetTexture = new RenderTexture((int)videoPlayer.clip.width, (int)videoPlayer.clip.height, 24);
        
        // 将RenderTexture赋值给videoScreen的texture属性，以在UI RawImage中显示视频帧
        videoScreen.texture = videoPlayer.targetTexture;
    }
}
