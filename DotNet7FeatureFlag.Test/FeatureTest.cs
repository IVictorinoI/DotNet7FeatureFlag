using DotNet7FeatureFlag.App.App.Clients;
using DotNet7FeatureFlag.App.App.Features;
using DotNet7FeatureFlag.App.App.Features.Dtos;
using DotNet7FeatureFlag.App.Repository.Clients;
using DotNet7FeatureFlag.App.Repository.Features;

namespace DotNet7FeatureFlag.Test
{
    public class FeatureTest
    {
        public FeatureTest()
        {
        }

        [Fact]
        public void OutOfRangePerc()
        {
            var app = GenerateApp();

            var ex = Assert.Throws<Exception>(() => app.Add(new FeatureAddDto()
            {
                Value = "pix_payment",
                ReleasedPerc = 101
            }));
            Assert.Contains("out of range", ex.Message);
        }

        [Fact]
        public void CheckDefaultProfile()
        {
            var app = GenerateApp();

            var feature = app.Add(new FeatureAddDto()
            {
                Value = "pix_payment",
                ReleasedPerc = 50
            });
            var profile = feature.GetProfile();

            Assert.NotNull(profile);
            Assert.Equal(100, profile.ReleasedPerc);
            Assert.Equal("Default", profile.Value);
            Assert.Equal("Default", profile.Description);
        }

        [Fact]
        public void CheckAllowedClient()
        {
            PopulateClients();

            var app = GenerateApp();
            var feature = app.Add(new FeatureAddDto()
            {
                Value = "bank_slip_payment",
                ReleasedPerc = 50
            });
            app.AddAllowedClient(new FeatureProfileClientAddDto()
            {
                ClientValue = "cliente_2",
                FeatureValue = "bank_slip_payment",
                ProfileValue = "Default"
            });
            var profile = feature.GetProfile();

            Assert.NotNull(profile);
            Assert.True(profile.AllowedClient("cliente_2"));
        }

        [Fact]
        public void CheckDaniedClient()
        {
            PopulateClients();

            var app = GenerateApp();
            app.RemoveAll();
            var feature = app.Add(new FeatureAddDto()
            {
                Value = "credit_card_payment",
                ReleasedPerc = 50
            });
            app.AddDaniedClient(new FeatureProfileClientAddDto()
            {
                ClientValue = "cliente_2",
                FeatureValue = "credit_card_payment",
                ProfileValue = "Default"
            });
            var profile = feature.GetProfile();

            Assert.NotNull(profile);
            Assert.False(profile.AllowedClient("cliente_2"));
        }

        [Fact]
        public void CheckClient()
        {
            PopulateClients();

            var app = GenerateApp();
            var feature = app.Add(new FeatureAddDto()
            {
                Value = "debit_card_payment",
                ReleasedPerc = 50
            });
            app.AddClient(new FeatureProfileClientAddDto()
            {
                ClientValue = "cliente_2",
                FeatureValue = "debit_card_payment",
                ProfileValue = "Default"
            });
            var profile = feature.GetProfile();

            Assert.NotNull(profile);
            Assert.True(profile.AllowedClient("cliente_2"));
        }

        [Fact]
        public void FeatureCheckEnabled()
        {
            var appClient = GenerateClientApp();
            appClient.RemoveAll();

            AppAddClient(appClient, "cliente_1");

            var app = GenerateApp();
            app.RemoveAll();

            app.Add(new FeatureAddDto()
            {
                Value = "credit_card_payment",
                ReleasedPerc = 100
            });
            var check = app.Check(new FeatureCheckDto()
            {
                ClientValue = "cliente_1",
                FeatureValue = "credit_card_payment"
            });
            Assert.True(check.Enable);
        }

        [Fact]
        public void FeatureCheckDisabled()
        {
            var appClient = GenerateClientApp();
            appClient.RemoveAll();

            AppAddClient(appClient, "cliente_1");

            var app = GenerateApp();
            app.RemoveAll();

            app.Add(new FeatureAddDto()
            {
                Value = "credit_card_payment",
                ReleasedPerc = 0
            });
            var check = app.Check(new FeatureCheckDto()
            {
                ClientValue = "cliente_1",
                FeatureValue = "credit_card_payment"
            });
            Assert.False(check.Enable);
        }

        [Fact]
        public void FeatureCheckHalfEnable()
        {
            var appClient = GenerateClientApp();
            appClient.RemoveAll();

            AppAddClient(appClient, "cliente_1");
            AppAddClient(appClient, "cliente_2");

            var app = GenerateApp();
            app.RemoveAll();

            app.Add(new FeatureAddDto()
            {
                Value = "credit_card_payment",
                ReleasedPerc = 50
            });
            var checkCustomer1 = app.Check(new FeatureCheckDto()
            {
                ClientValue = "cliente_1",
                FeatureValue = "credit_card_payment"
            });
            var checkCustomer2 = app.Check(new FeatureCheckDto()
            {
                ClientValue = "cliente_2",
                FeatureValue = "credit_card_payment"
            });
            Assert.True(checkCustomer1.Enable);
            Assert.False(checkCustomer2.Enable);
        }

        private void PopulateClients()
        {
            var app = GenerateClientApp();
            app.RemoveAll();
            AppAddClient(app, "cliente_1");
            AppAddClient(app, "cliente_2");
            AppAddClient(app, "cliente_3");
            AppAddClient(app, "cliente_4");
        }

        private FeatureApp GenerateApp()
        {
            var repClient = new RepClientMemory();
            return new FeatureApp(new RepFeatureMemory(), repClient, new ClientApp(repClient));
        }

        private ClientApp GenerateClientApp()
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
