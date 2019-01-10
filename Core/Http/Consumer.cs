using Core.Http.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Core.Http
{
    public class Consumer
    {
        private HttpClient http;
        
        public Consumer(HttpClient httpClient)
        {
            http = httpClient;
        }
        
        public IObservable<TReturn> Post<TReturn, TEntity>(string path, TEntity body, IDictionary<string, string> headers = null)
        {
            return Observable.Create<TReturn>(async observer =>
            {
                try
                {
                    var jsonContent = JsonConvert.SerializeObject(body);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await http.PostAsync(path, contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.UTF8))
                        {
                            string responseText = reader.ReadToEnd();
                            var objReturn = JsonConvert.DeserializeObject<TReturn>(responseText);
                            observer.OnNext(objReturn);
                            observer.OnCompleted();
                        }
                    }
                    else
                    {
                        throw new HttpResponseException();
                    }
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }

                return Disposable.Empty;
            });
           
        }
    }
}
