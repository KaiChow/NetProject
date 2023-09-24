namespace Net6.WebApi.Utility
{
    /// <summary>
    /// http帮助类
    /// </summary>
    public class HttpClientUtil
    {
        /// <summary>
        /// 文件上传的
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="fileUrl">文件的url</param>
        /// <returns></returns>
        public static string UploadFile(string url, string fileUrl)
        {

            Stream fileStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            var formData = new MultipartFormDataContent();
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            formData.Add(new StreamContent(fileStream), "file", fileUrl);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = client.PostAsync(url, formData).Result;
                string responseResult = result.Content.ReadAsStringAsync().Result;
                client.Dispose();
                fileStream.Close();
                return responseResult;
            }
        }
    }
}
