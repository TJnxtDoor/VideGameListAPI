var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(@"
        <html>
        <head>
            <title>Video Game List API</title>
            <style>
                body { font-family: sans-serif; margin: 40px; line-height: 1.6; background: #f4f7f6; color: #333; }
                .container { max-width: 600px; margin: 0 auto; background: white; padding: 30px; border-radius: 8px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }
                h1 { color: #e67e22; }
                a { color: #e67e22; text-decoration: none; font-weight: bold; }
                a:hover { text-decoration: underline; }
                ul { padding-left: 20px; }
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>Video Game List API</h1>
                <p>Welcome to your custom-made Video Game API! Access the list of games using the link below:</p>
                <ul>
                    <li><a href='/api/games'>Get All Video Games (/api/games)</a></li>
                </ul>
            </div>
        </body>
        </html>
    ");
});

app.Run();
