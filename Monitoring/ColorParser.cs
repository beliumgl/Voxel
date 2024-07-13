namespace Monitoring;

public class ColorParser
{
    public static string parse(string input)
    {
        string text = "§";
        input = input.Replace(text + "0", getHtmlColor("#000000"));
        input = input.Replace(text + "1", getHtmlColor("#0000AA"));
        input = input.Replace(text + "2", getHtmlColor("#00AA00"));
        input = input.Replace(text + "3", getHtmlColor("#00AAAA"));
        input = input.Replace(text + "4", getHtmlColor("#AA0000"));
        input = input.Replace(text + "5", getHtmlColor("#AA00AA"));
        input = input.Replace(text + "6", getHtmlColor("#FFAA00"));
        input = input.Replace(text + "7", getHtmlColor("#AAAAAA"));
        input = input.Replace(text + "8", getHtmlColor("#555555"));
        input = input.Replace(text + "9", getHtmlColor("#5555FF"));
        input = input.Replace(text + "a", getHtmlColor("#55FF55"));
        input = input.Replace(text + "b", getHtmlColor("#55FFFF"));
        input = input.Replace(text + "c", getHtmlColor("#FF5555"));
        input = input.Replace(text + "d", getHtmlColor("#FF55FF"));
        input = input.Replace(text + "e", getHtmlColor("#FFFF55"));
        input = input.Replace(text + "f", getHtmlColor("#FFFFFF"));
        return input;
    }

    public static string getHtmlColor(string color)
    {
        return "<spa style=\"color:" + color + "\">";
    }

    public static string removeColors(string input)
    {
        string text = "§";
        input = input.Replace(text + "0", "");
        input = input.Replace(text + "1", "");
        input = input.Replace(text + "2", "");
        input = input.Replace(text + "3", "");
        input = input.Replace(text + "4", "");
        input = input.Replace(text + "5", "");
        input = input.Replace(text + "6", "");
        input = input.Replace(text + "7", "");
        input = input.Replace(text + "8", "");
        input = input.Replace(text + "9", "");
        input = input.Replace(text + "a", "");
        input = input.Replace(text + "b", "");
        input = input.Replace(text + "c", "");
        input = input.Replace(text + "d", "");
        input = input.Replace(text + "e", "");
        input = input.Replace(text + "f", "");
        return input;
    }
}
