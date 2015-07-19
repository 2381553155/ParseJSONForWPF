
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace ParseJSONForWPF
{
    class HttpRequest
    {
        MainWindow mainWindow = new MainWindow();
        public HttpRequest(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        /// <summary>
        /// 图灵机器人的key值
        /// </summary>
        private string[] key = { "2b15219bcc2fc0c93e6beab1719da82a", 
                                 "2f6ad3d1ab8d7ea7be90987674ab0f3c" 
                               };
        /// <summary>
        /// 访问图灵机器人的url
        /// </summary>
        string url;

        private string urlHelp(string key, string info)
        {
            url = "http://www.tuling123.com/openapi/api?key=" + key + "&info=" + info;
            return url;
        }

        public void getTuRingReply(string info)
        {
            url = this.urlHelp("2b15219bcc2fc0c93e6beab1719da82a", info);
            this.DoHttpWebRequest(url);
        }

        private void DoHttpWebRequest(string url)
        {

            //创建WebRequest类
            WebRequest request = HttpWebRequest.Create(url);


            //返回异步操作的状态
            IAsyncResult result = (IAsyncResult)request.BeginGetResponse(ResponseCallback, request);

        }

        private void ResponseCallback(IAsyncResult result)
        {
            //获取异步操作返回的的信息
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;

            //结束对 Internet 资源的异步请求
            WebResponse response = request.EndGetResponse(result);

            try
            {
                Stream stream = response.GetResponseStream();

                Encoding ec = Encoding.UTF8;

                StreamReader reader = new StreamReader(stream, ec);
                string tuRingResponse = reader.ReadToEnd();//读取返回的json数据

                Console.WriteLine(tuRingResponse);
                Console.WriteLine("序列化:");

                string code = tuRingResponse.Substring(8, 6);
               // Console.WriteLine(code);

                //文字类回复实体类对象
                TextResponse txtrs = null;

                ////列车类回复实体类对象
                //TrainResponse trrs = null;

                ////航班类回复实体类对象
                //FlightResponse flrs = null;


                //Callout peoplecallout = new Callout();
                //文字类回复
                if (code == "100000")
                {
                    txtrs = this.DeserializeFromJson<TextResponse>(tuRingResponse);

                    this.mainWindow.updateContent(txtrs);
                  
                    //Console.WriteLine(txtrs.Code);
                    //this.mainWindow.Dispatcher.Invoke(new Action(() => { this.mainWindow.responseContent_Text.Text = txtrs.Text;
                    //}));

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public T DeserializeFromJson<T>(string jsonString) where T : class
        {
            T obj = JsonConvert.DeserializeObject<T>(jsonString);
            return obj;
        }

    }
}
