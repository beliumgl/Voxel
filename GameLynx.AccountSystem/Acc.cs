using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Http;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Monitoring.UI;
using Octokit;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace GameLynx.AccountSystem;

public class Acc
{
    public static int Service = 0;

    public static string ID = "";

    public static string Html_Content = "<!DOCTYPE html>\r\n<html>\r\n    <head>\r\n        <meta charset=\"UTF-8\">\r\n        <title>Успех</title>\r\n        </head>\r\n<body>\r\n\r\n<div class=\"centered-text\">\r\n    <p>Вы успешно авторизовались в Voxel Multiplayer.<p>\r\n</div>\r\n\r\n<style>\r\n.centered-text {\r\n    background-color: black;\r\n    color: white;\r\n    padding: 20px;\r\n    position: absolute;\r\n    top: 50%;\r\n    left: 50%;\r\n    transform: translate(-50%, -50%);\r\n}\r\n\r\nhtml {\r\n    background-color: #2F4F4F\r\n}\r\n</style>\r\n\r\n</body>\r\n</html>";

    public static string Name { get; set; }

    public static Image Avatar { get; set; }

    public static async void Login(int Service, string client_secret, string client_id, string app_name)
    {
        await LoginMain(Service, client_secret, client_id, app_name);
    }

    private static async Task LoginMain(int Service, string client_secret, string client_id, string app_name)
    {
        string redirectURL = $"http://localhost:{new Random().Next(1, 65535)}";
        Acc.Service = Service;
        switch (Acc.Service)
        {
            case 1:
                {
                    ClientSecrets val = new ClientSecrets();
                    val.ClientSecret = client_secret;
                    val.ClientId = client_id;
                    Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow.Initializer val2 = new GoogleAuthorizationCodeFlow.Initializer();
                    ((GoogleAuthorizationCodeFlow.Initializer)val2).ClientSecrets = val;
                    ((GoogleAuthorizationCodeFlow.Initializer)val2).Scopes = new string[3] { "openid", "email", "profile" };
                    GoogleAuthorizationCodeFlow val3 = new GoogleAuthorizationCodeFlow(val2);
                    Process.Start(((AuthorizationCodeFlow)val3).CreateAuthorizationCodeRequest(redirectURL + "/").Build().ToString());
                    HttpListener httpListener = new HttpListener();
                    string text = null;
                    httpListener.Prefixes.Add(redirectURL + "/");
                    httpListener.Start();
                    try
                    {
                        HttpListenerContext context = httpListener.GetContext();
                        HttpListenerRequest request = context.Request;
                        if (request.RawUrl.Contains("code"))
                        {
                            text = Utils.GetQueryStringParameter(new Uri(redirectURL + request.RawUrl).Query, "code").Replace("%2F", "/");
                        }
                        TokenResponse result = ((AuthorizationCodeFlow)val3).ExchangeCodeForTokenAsync("", text, redirectURL + "/", CancellationToken.None).Result;
                        UserCredential httpClientInitializer = new UserCredential((IAuthorizationCodeFlow)(object)val3, Environment.UserName, result);
                        Userinfo obj = ((ClientServiceRequest<Userinfo>)(object)new Oauth2Service(new BaseClientService.Initializer
                        {
                            HttpClientInitializer = (IConfigurableHttpClientInitializer)(object)httpClientInitializer
                        }).Userinfo.Get()).Execute();
                        Name = obj.Name;
                        Avatar = Image.FromStream(WebRequest.Create(obj.Picture).GetResponse().GetResponseStream());
                        ID = obj.Id;
                        HttpListenerResponse response = context.Response;
                        byte[] bytes = Encoding.UTF8.GetBytes(Html_Content);
                        response.ContentLength64 = bytes.Length;
                        Stream outputStream = response.OutputStream;
                        outputStream.Write(bytes, 0, bytes.Length);
                        outputStream.Close();
                    }
                    catch
                    {
                        new GMessageBoxOK("Не удалось залогиниться! Попробуйте снова.").ShowDialog();
                        Application.Exit();
                        return;
                    }
                    break;
                }
            case 0:
                {
                    GitHubClient client = new GitHubClient(new ProductHeaderValue(app_name));
                    User USER_ = new User();
                    await Task.Run(delegate
                    {
                        OauthLoginRequest val4 = new OauthLoginRequest(client_id)
                        {
                            Scopes = { "user", "repo" },
                        };
                        Process.Start(client.Oauth.GetGitHubLoginUrl(val4).ToString());
                        HttpListener httpListener2 = new HttpListener();
                        httpListener2.Prefixes.Add(redirectURL + "/");
                        httpListener2.Start();
                        try
                        {
                            HttpListenerContext context2 = httpListener2.GetContext();
                            HttpListenerRequest request2 = context2.Request;
                            if (request2.RawUrl.Contains("code") && request2.RawUrl.Contains("="))
                            {
                                OauthToken result2 = client.Oauth.CreateAccessToken(new OauthTokenRequest(client_id, client_secret, request2.RawUrl.Split('=')[1])).Result;
                                client.Credentials = new Credentials(result2.AccessToken);
                                USER_ = client.User.Current().Result;
                                HttpListenerResponse response2 = context2.Response;
                                byte[] bytes2 = Encoding.UTF8.GetBytes(Html_Content);
                                response2.ContentLength64 = bytes2.Length;
                                Stream outputStream2 = response2.OutputStream;
                                outputStream2.Write(bytes2, 0, bytes2.Length);
                                outputStream2.Close();
                            }
                            else
                            {
                                MessageBox.Show("Не удалось завершить авторизацию: Неверный запрос.");
                            }
                        }
                        catch
                        {
                            new GMessageBoxOK("Не удалось залогиниться! Попробуйте снова.").ShowDialog();
                            Application.Exit();
                        }
                    });
                    Name = ((Account)USER_).Name;
                    Avatar = Image.FromStream(WebRequest.Create(((Account)USER_).AvatarUrl).GetResponse().GetResponseStream());
                    ID = ((Account)USER_).Id.ToString();
                    break;
                }
        }
        Application.Exit();
        Thread thread = new Thread((ThreadStart)delegate
        {
            new App().ShowDialog();
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }
}
