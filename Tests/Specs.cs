using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicsTest.Tests
{
    public abstract class Specs : IDisposable
    {
        protected BrowserHost Browser { get; private set; }
        protected string BaseUrl { get; set; }

        protected IConfiguration Configuration { get; set; }


        public Specs(Func<BrowserHost> browserHostFactory)
        {
            Browser = browserHostFactory();

            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Specs>();

            Configuration = builder.Build();

            BaseUrl = Configuration["baseUrl"];
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Browser != null)
                    {
                        Browser.Dispose();
                        Browser = null;
                    }
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
