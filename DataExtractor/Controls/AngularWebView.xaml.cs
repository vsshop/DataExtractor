namespace DataExtractor.Controls;

public partial class AngularWebView : ContentView
{
    private WebView _webView;
    public AngularWebView()
	{
		InitializeComponent();

        _webView = new WebView
        {
            Source = "./wwwroot/build/browser/index.html",
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill
        };

        Content = _webView;
    }
}