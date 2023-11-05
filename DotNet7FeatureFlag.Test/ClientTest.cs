using DotNet7FeatureFlag.App.App.Clients;
using DotNet7FeatureFlag.App.Repository.Clients;

namespace DotNet7FeatureFlag.Test
{
    public class ClientTest
    {
        [Fact]
        public void AlreadyExistsException()
        {
            var app = GenerateApp();
            app.RemoveAll();
            AppAddClient(app, "cliente_test_1");


            var ex = Assert.Throws<Exception>(() => AppAddClient(app, "cliente_test_1"));
            Assert.Contains("already exists", ex.Message);
        }

        private ClientApp GenerateApp()
        {
            return new ClientApp(new RepClientMemory());
        }

        private void AppAddClient(ClientApp app, string value)
        {
            app.Add(new ClientAddDto()
            {
                Value = value
            });
        }
    }
}
