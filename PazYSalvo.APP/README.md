
# PazYSalvo




## Crear carpeta

Creamos una carpeta llamada "PazYSalvo.APP" en la cual estara contenido todo nuestro proyecto.
## Crear capas

Abrimos la terminal.

1. Creamos la capa de presentacion "PazYSalvoAPP.WebAPP" con el comando "dotnet new mvc -n PazYSalvoAPP.WebApp"

    --captura 1
2. Creamos la capa "PazYSalvoAPP.Data" con el comando "dotnet new classlib -n PazYSalvoAPP.Data"

    --captura 2
3. Creamos la capa "PazYSalvoAPP.Business" con el comando "dotnet new classlib -n PazYSalvoAPP.Business"

    --captura 3

Posterior a esto borramos manualmente las Clases de las bibliotecas de clases.


## Agregar referencias

Referencias de las capas que se an a crear en el archivo csproj de l capa de presentacion.

    <ItemGroup>
        <ProjectReference Include="..\PazYSalvoAPP.Business\PazYSalvoAPP.Business.csproj"/>
        <ProjectReference Include="..\PazYSalvoAPP.Data\PazYSalvoAPP.Data.csproj"/>
    </ItemGroup>
## Data configuraciones

1. Se agrega un paquete System.Data.SqlClient en el PazYSalvoAPP.Data.csproj para el acceso de datos utilizando el comando "dotnet add package System.Data.SqlClient" en la terminal
    
    --captura 4

2. Se crea la clase "Producto" par agregar las referencias.

        namespace PazYSalvoAPP.Data
        {
            public class Producto
            {
                public int Id { get; set; }
                public string Nombre { get; set; } = null!;
            public decimal Precio { get; set; }
            }
        }

3. Se crea la clase "ProductoData" que va a tener el acceso de datos con el servidor y se agregan los metodos

        using System.Data.SqlClient;

        namespace DataAccess
        {
            public class ProductoData(string connectionString)
            {
                private readonly string _connectionString = connectionString;

                public List<Producto> GetAllProductos()
                {
                    List<Producto> productos = [];

                    using (SqlConnection connection = new(_connectionString))
                    {
                        string query = "SELECT Id, Nombre, Precio FROM Productos";
                        SqlCommand command = new(query, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            productos.Add(
                            new Producto
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = reader.GetDecimal(2)
                            }
                        );
                        }
                    }
                    return productos;
                }


                public Producto GetProductoById(int id)
                {
                    Producto producto = new();

                    using (SqlConnection connection = new(_connectionString))
                    {
                        string query = "SELECT Id, Nombre, Precio FROM Productos WHERE Id=@Id";
                        SqlCommand command = new(query, connection);
                        command.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            producto = 
                            new Producto
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Precio = reader.GetDecimal(2)
                            };
                        }
                    }
                    return producto;
                }

                public void AddProducto(Producto producto)
                {
                    using SqlConnection connection = new(_connectionString);
                    string query = "INSERT INTO Productos (Nombre, Precio) VALUES (@Nombre,     @Precio)";
                    SqlCommand command = new(query, connection);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                public void UpdateProducto(Producto producto)
                {
                    using SqlConnection connection = new(_connectionString);
                    string query = "UPDATE Productos SET Nombre=@Nombre, Precio=@Precio WHERE Id=@Id";
                    SqlCommand command = new(query, connection);
                    command.Parameters.AddWithValue("@Id", producto.Id);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@Precio", producto.Precio);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                public void DeleteProducto(int id)
                {
                    using SqlConnection connection = new(_connectionString);
                    string query = "DELETE FROM Productos WHERE Id=@Id";
                    SqlCommand command = new(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


## Business configuraciones

1. Se agrega la referencia de la capa de Data al PazYSalvoAPP.Business.csproj 

        <ItemGroup>
            <ProjectReference Include="..\PazYSalvoAPP.Data\PazYSalvoAPP.Data.csproj"/>
        </ItemGroup>

2. Creamos una clase de serivicio.
## Creación BD en SQL SERVER

Nos vamos a SQL SERVER y creamos una base de datos llamada "PazSalvo"

--captura 5
## Capa de presentación configuraciones

1. Nos vamos al appsettings.json y creamos la conexion con la base de datos anteriormente creada con el codigo:

        "AllowedHosts": "*",
        "ConnectionStrings": {
            "DefaultConnection":"server=localhost; Database=PazSalvo; user id=user ; password=password ; Encrypt=false;"
        }

2. Configuramos la inyeccion de depedencias de nuestra capa de negocios en el program

        using PazYSalvoAPP.Business;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (connectionString!=null)
        {
            builder.Services.AddScoped<ProductoService>(provider => new ProductoService(connectionString));
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

## Controllers

Creamos la vista del Controller Producto.

-- captura 6


## Conexión a la Base de Datos

1. Instalamos la extension de SQL SERVER en VSC llamada "SQL SERVER (mssql)"

2. En esta nueva pestaña le damos "Agregar conexion" y seguimos todos los pasos


## Ejecución 

1. Se abre la termial y se ejecuta el siguiente codigo:

        dotnet run --project PazYSalvoAPP.WebApp/PazYSalvoAPP.WebApp.csproj

2. Se va al link que arroja.

3. en el link se agrega:

        /Producto

    y este nos llevara a donde podremos ver y utiliar nuestra pagina finalizada.