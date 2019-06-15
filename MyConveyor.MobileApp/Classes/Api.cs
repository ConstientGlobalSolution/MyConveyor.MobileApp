using MyConveyor.MobileApp.StaticClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyConveyor.MobileApp.Classes
{
    public class Api
    {
        public static HttpClient Client { get; set; }

        public Api()
        {
            Client = new HttpClient();
        }

        /// ------------------------------------------------------------------------------------------------        
        /// Name		GetData
        ///  
        /// <summary>
        /// Get the single data
        /// </summary>
        /// <param name="controllerUrl"></param>       
        /// ------------------------------------------------------------------------------------------------
        public async Task<TEntity> GetEntity<TEntity>(string controllerUrl)
        {
            TEntity entity = JsonConvert.DeserializeObject<TEntity>(string.Empty);
            Uri uri = new Uri(Constants.HostName + controllerUrl);
            try
            {
                HttpResponseMessage httpResponse = await Client.GetAsync(uri);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string responsedata = await httpResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responsedata))
                    {
                        entity = JsonConvert.DeserializeObject<TEntity>(responsedata);
                    }

                }

                return entity;
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return entity;
            }
        }

        /// ------------------------------------------------------------------------------------------------        
        /// Name		GetAllData
        ///  
        /// <summary>
        /// Get List of data
        /// </summary>
        /// <param name="controllerUrl"></param>
        /// ------------------------------------------------------------------------------------------------
        public async Task<List<TEntity>> GetAllEntity<TEntity>(string controllerUrl)
        {
            try
            {
                List<TEntity> entitiesList = new List<TEntity>();
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("ContentType", "application/json");
                Client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");

                HttpResponseMessage httpResponse = await Client.GetAsync(Constants.HostName + controllerUrl);// verify it
                if (httpResponse.IsSuccessStatusCode)
                {
                    string responsedata = await httpResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responsedata))
                    {
                        entitiesList = JsonConvert.DeserializeObject<List<TEntity>>(responsedata);

                    }

                }

                return entitiesList;

            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return null;
            }
        }

        /// ------------------------------------------------------------------------------------------------        
        /// Name		PostData
        ///  
        /// <summary>
        /// Post the single data
        /// </summary>
        /// <param name="controllerUrl"></param>
        ///  <param name="entity"></param>
        /// <param name="returnEntity"></param>
        /// ------------------------------------------------------------------------------------------------
        public async Task<REntity> PostEntity<TEntity, REntity>(string controllerUrl, TEntity entity, REntity returnEntity)
        {
            try
            {
                string json = JsonConvert.SerializeObject(entity);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("ContentType", "application/json");

                HttpResponseMessage httpResponse = await Client.PostAsync(Constants.HostName + controllerUrl, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string responsedata = await httpResponse.Content.ReadAsStringAsync();
                    returnEntity = JsonConvert.DeserializeObject<REntity>(responsedata);
                }

                return returnEntity;
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return returnEntity;
            }
        }

        /// ------------------------------------------------------------------------------------------------        
        /// Name		PostDataList
        ///  
        /// <summary>
        /// Post the data and get list of data
        /// </summary>
        /// <param name="controllerUrl"></param>
        /// <param name="entity"></param>
        /// ------------------------------------------------------------------------------------------------
        public async Task<List<TEntity>> PostEntityList<TEntity>(string controllerUrl, TEntity entity)
        {
            try
            {
                List<TEntity> entitiesList = new List<TEntity>();

                string json = JsonConvert.SerializeObject(entity);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("ContentType", "application/json");

                HttpResponseMessage httpResponse = await Client.PostAsync(Constants.HostName + controllerUrl, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string responsedata = await httpResponse.Content.ReadAsStringAsync();
                    entitiesList = JsonConvert.DeserializeObject<List<TEntity>>(responsedata);
                }

                return entitiesList;
            }
            catch (Exception ex)
            {
                LogTracking.LogTrace(ex.Message + ex.StackTrace);
                return null;
            }
        }
    }
}
