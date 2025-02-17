using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake;

public static class Images // container for image assets
{
    public static readonly ImageSource Empty = LoadImage("Empty.png");
    public static readonly ImageSource Body = LoadImage("Body.png");
    public static readonly ImageSource DeadBody = LoadImage("DeadBody.png");
    public static readonly ImageSource Head = LoadImage("Head.png");
    public static readonly ImageSource DeadHead = LoadImage("DeadHead.png");
    public static readonly ImageSource Food = LoadImage("Food.png");
    
    private static ImageSource LoadImage(string fileName)
    {
        return new BitmapImage(new Uri($"Assets/{fileName}", UriKind.Relative));
    }
}