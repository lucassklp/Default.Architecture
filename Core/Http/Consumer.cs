using Core.Http.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Core.Http
{
    public abstract class Consumer
    {
        public string BaseUrl { get; private set; }
        private IDeserializer deserializer;

        public Consumer(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }
        

        private string Resolve(string path)
        {
            return $"{this.BaseUrl}/{path}";
        }



        public IObservable<TReturn> Post<TReturn>(string path, IHttpBody body = null, IDictionary<string, string> headers = null)
        {
            Observable.Return(JsonConvert.DeserializeObject<TReturn>(""));
        }
    }
}
