using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UI.ApiHelpers
{
    public class ApiProcess
    {
        public static async Task<ResponseModel> PostMetod<ReguestModel, ResponseModel>(string URL, ReguestModel requestModel) where ReguestModel : class where ResponseModel : class
        {
            ResponseModel reservationList;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

                    using (var responsenew = Task.Run(async () => await httpClient.PostAsync(URL, content)).Result)
                    {
                        string apiResponse = await responsenew.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<ResponseModel>(apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    reservationList = null;
                    string error = ex.StackTrace;
                }
            }
            return reservationList;
        }
        public static async Task<ResponseModel> GetMetod<ResponseModel>(string URL) where ResponseModel : class
        {
            ResponseModel reservationList;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var responsenew = Task.Run(async () => await httpClient.GetAsync(URL)).Result)
                    {
                        string apiResponse = await responsenew.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<ResponseModel>(apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    reservationList = null;
                    string error = ex.StackTrace;
                }
            }
            return reservationList;
        }
        public static async Task<ResponseModel> DeleteMetod<ResponseModel>(string URL, int requestModel) where ResponseModel : class
        {
            ResponseModel reservationList;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var responsenew = Task.Run(async () => await httpClient.DeleteAsync(URL + requestModel)).Result)
                    {
                        string apiResponse = await responsenew.Content.ReadAsStringAsync();
                        reservationList = JsonConvert.DeserializeObject<ResponseModel>(apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    reservationList = null;
                    string error = ex.StackTrace;
                }
            }
            return reservationList;
        }
    }
}
