
# Cliente

Reorganizaciòn del cliente


## Método cargarFacturas()

en factura.service.ts

1. Cambiamos el puerto de private baseUrl = 'https://localhost:¨Puerto/api/factura'; al que utilice nuestra maquina del backend. 

2. Implementamos el metodo cargarFacturas en el factura.service.ts

        cargarFacturas(): Observable<factura[]> {
            return this.http.get<factura[]>(this.baseUrl);
        }

Descomentamos "private http: HttpClient" y eliminamos "@Inject(HttpClient) private http: HttpClient"

3. Luego nos vamos a facturas.components.ts y agregamos el metodo:

        cargarFacturas(): void {

            this._facturaService.cargarFacturas().subscribe((facturas) => {
            this.dataSource = facturas;
            });

        }

cambiamos el datasource a: 

    dataSource: factura[] = [];

y lo ponemos al inicio.

## Base de Datos

1. Si no tenemos la base de datos creada, la creamos utiliando "Code First" o manualmente de acuerdo a los models existentes en el Backend.

2. Modificamos la conexion en el archivo appsettings.json de acuerdo a nuestras maquinas.
## Verificacion

Por ultimo ponemos a correr tanto el frontend como el backend y verificamos que la conexion funcione y los datos se esten enviando correctamente.