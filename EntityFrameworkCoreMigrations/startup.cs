    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration   
 Configuration { get; }

        public void ConfigureServices(IServiceCollection services)   

        {
            services.AddControllers();
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();   

            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddMetrics();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();   

            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();   

                endpoints.MapMetrics();   

            });
        }
    }

    public interface IMessagePublisher
    {
        Task PublishMessageAsync(string message);
    }

    public class MessagePublisher : IMessagePublisher
    {
        private readonly IConnectionFactory _connectionFactory;

        public MessagePublisher(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public   
 async Task PublishMessageAsync(string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("product_updates",   
 false, false, false, null);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "product_updates", null, body);
            }
        }
    }
}