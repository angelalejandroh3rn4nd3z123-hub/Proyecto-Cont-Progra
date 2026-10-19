//love duilio
//DIOS BENDIGA ESTE PROGRAMA Y A SUS CREADORES
//Dios ayudame a entender este programa por fis
// seara
using System.IO;
using System.Reflection.PortableExecutable;
using System.Timers;
using System.Globalization;
using System.Text;
using System.Linq;

//rutas relativas a los archivos que se encuentran dentro de la solucion y dentro del programa 
//mediante el Path.Combine se unen las carpetas
//el metodo AppDomain.CurrentDomain.BaseDirectory, sirve para buscar el archivo csv dentro de la solucion donde se esta ejecutando este codigo
string ruta_del_catalogo_de_cuentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "catalogo de cuentas.csv");

string ruta_de_los_movimientos_contables = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "movimientos de las cuentas.csv");
//mediante esos dos metodos detectamos la ruta de los dos archivos csv 

try
{
    // se asegura que el codigo pueda leer el docuemento en caso que no se muestra este mensaje y cancela en programa hasta que lo reconozca
    if (!File.Exists(ruta_del_catalogo_de_cuentas))
    {
        Console.WriteLine($"no se encontro el archivo en la ruta: {ruta_del_catalogo_de_cuentas}");
        return;
    }
    else
    {
        Console.WriteLine("loco el catalogo esta alli tate tranqui");
    }

    // same here se asegura que el codigo pueda leer el docuemento en caso que no se muestra este mensaje 
    if (!File.Exists(ruta_de_los_movimientos_contables))
    {
        Console.WriteLine($"no se encontro el archivo en la ruta: {ruta_de_los_movimientos_contables}");
        return;
    }
    else
    {
        Console.WriteLine("loco el los movs estan bien alla en su casa aca relajo mi negro");
    }
}
catch (Exception ex)
{
    //si, dios no quiera sucede un error este catch nos avisa de eso 
    Console.WriteLine("ha ocurrido un error. estamos trabajando en eso");
    Console.WriteLine(ex.Message);
}

//!!!!!!!!!!!!!!!!!! borrar de ser posible
// esto esta para confirmar que los csv estan y donde se encuentran
Console.WriteLine($"La ruta donde se encuentra el catologo de cuentas es: {ruta_del_catalogo_de_cuentas}");
Console.WriteLine($"La ruta donde se encuentra los movimientos contables es: {ruta_de_los_movimientos_contables}");


// variable para que el ciclo continue hasta que el usuario diga basta con la opcion numero 9
bool continuar = true;

while (continuar)
{
    //mostramos el menu al usuario
    MostrarMenu();

    // pedimos una opcion que debe estar entre el rango de la seleccion osea entre 1-9
    int opcion = PedirOpcion();

    switch (opcion)
    {
        case 1: // logica del switch numero 1-1
            bool continuar_switch_uno = true;
            while (continuar_switch_uno)
            {
                //mostramos las opciones que el usuario puede realizar dentro delswitch numero 1
                mostrar_menu1();
                //le pedimos lo que quiere realizar
                int opcion_switch_uno = pedir_opcion_switch_uno();

                //en funcion de eso desplegamos el switch 1-1
                switch (opcion_switch_uno)
                {
                    case 1: // logica del switch numero 1.1 mostrar el catalogo de las cuentas
                        Console.WriteLine();
                        Console.WriteLine("CATÁLOGO DE LAS CUENTAS");
                        Console.WriteLine();

                        //nos aseguramos otra vez que el archivo ese alli
                        if (!File.Exists(ruta_del_catalogo_de_cuentas))
                        {
                            Console.WriteLine($"No se encontro el archivo de las cuentas: {ruta_del_catalogo_de_cuentas}");
                            break;
                        }

                        // el metodo ReadAllLines sirve para que la pc lea el archivo csv y almacene todos los datos en el arreglo lineas_csv_del_catalogo1 
                        string[] lineas_csv_del_catalogo1 = File.ReadAllLines(ruta_del_catalogo_de_cuentas);

                        //si el archivo esta vacio le decimos al usuario que esta vacio
                        if (lineas_csv_del_catalogo1.Length == 0)
                        {
                            Console.WriteLine("El archivo se encuentra vacio");
                        }

                        // esto sirve para que el codigo se vea mas ordenado 
                        Console.WriteLine($"{"Código",-6} | {"Cuenta",-35} | {"Tipo",-10}      |{"Naturaleza",-10}");
                        Console.WriteLine(new string('-', 90));

                        //le damos el valor de verdadero a la nueva variable de tipo bool
                        bool primera_linea1_1 = true;

                        //va leyendo uno por una las lineas que le metimos por medio del csv
                        foreach (string lineas in lineas_csv_del_catalogo1)
                        {
                            //no lee las lineas que se encuentren vacias es lo que hace el metodo IsNullOrWhiteSpace
                            if (string.IsNullOrWhiteSpace(lineas)) continue;

                            // la primera linea del archivo csv son los encabezados los cuales no queremos leer de esta manera asi que usamos esta estructura condicional para 
                            //señalar al programa que no la lea el metodo StartsWith sirve para definir si comienza con la condicion que le dimos que se salte esa linea por que encabezado lo leeremos despues
                            if (primera_linea1_1 && lineas.ToLower().StartsWith("codigo"))
                            {
                                //le reasignamos un valor a primera lineas
                                primera_linea1_1 = false;
                                continue;
                            }

                            // el metodo Split hace que al momento de leer una linea la divide en partes en funcion del separados en este caso las ;
                            string[] datos = lineas.Split(';');

                            //esta condicion es para que lea la linea que cumple la condicion en funcion de las dimensiones del archivo 4 en este caso
                            if (datos.Length >= 4)
                            {
                                //lo mismo que el metodo del punto 3 pero con la condicion del trim el cual elimina cualquier espacio que este al terminar de leer numeros o letras 
                                //los cuales se van a almacenar en los arreglos correspondientes y las variables correspondientes
                                string codgio = datos[0].Trim();
                                string cuenta = datos[1].Trim();
                                string tipo_de_cuenta = datos[2].Trim();
                                string naturaleza_de_la_cuenta = datos[3].Trim();

                                //muestra los datos formateados en orden en sus respectivo campo
                                Console.WriteLine($"{codgio,-6} | {cuenta,-35} | {tipo_de_cuenta,-15} | {naturaleza_de_la_cuenta,-18}");
                            }
                            else
                            {
                                Console.WriteLine("La linea se encuentra incompleta revisalo");
                            }

                        }
                        break;

                    case 2:// La lógica del 1.2 es agregar una cuenta al archivo csv
                        Console.WriteLine();
                        Console.WriteLine("AGREGANDO UNA CUENTA NUEVA EN EL CATALOGO DE CUENTAS");

                        //nos aseguramos otra vez que el archivo ese alli
                        if (!File.Exists(ruta_del_catalogo_de_cuentas))
                        {
                            Console.WriteLine("El archivo no se encuentra. Asegúrate de revisar la ruta y las propiedades.");
                            break;
                        }
                        // pese hacer un numero lo que se le pide al usuario se inicializa como string para poder validar mejor la opcion
                        Console.Write("Ingrese el codigo de nueva cuenta: ");
                         string codigo_validado = "";

                        // este bucle while existe para que el usuario no salga de la ejecucion hasta que ingrese un codigo de la cuenta que sea valido
                        while (true)
                        {
                            
                            //llamamos al metodo pedir numero para validar que no ingrese numeros no validos 
                            int codigo_de_la_cuenta_introducido = pedir_numero();

                            //y lo convertimos a string para que sea mas facil al momento de validar esta variable es la representacion del codigo que ingreso el usuario
                            string codigo_str = codigo_de_la_cuenta_introducido.ToString();

                            // el metodo ReadAllLines sirve para que la pc lea el archivo csv y almacene todos los datos en el arreglo lineas_csv_del_catalogo
                            string[] lineas_existentes = File.ReadAllLines(ruta_del_catalogo_de_cuentas);

                            // esta variable es la que valida que el codigo no haya sido introducido antes 
                            bool existe = false;

                            //leemos todas las lineas del csv 
                            foreach (string linea in lineas_existentes)
                            {
                                // esta funcion se salta las lineas que se encuentren en blanco
                                if (string.IsNullOrWhiteSpace(linea)) continue;

                                //el metodo ToSplit separa la lineas en funcion lo que le ingresemos al metodo en este caso ;
                                string[] datos = linea.Split(';');

                                // comparamos el codigo del csv con el ingresado el metodo To.Trim se salta los espacios en blanco
                                if (datos.Length > 0 && datos[0].Trim().ToLower() == codigo_str.ToLower())
                                {
                                    existe = true; // si el codgio ya existe se cambis el valor del bool para detener la busqueda por que ya encontro un codigo
                                    break;
                                }
                            }
                            //si el codigo ya existe se le indica al usuario que ya existe y se repite la accion de buscar
                            if (existe)
                            {
                                Console.WriteLine($"El código {codigo_str} ya tiene una cuenta asignada. por favor ingresa otro codigo");
                            }
                            else
                            {
                                //si el codigo introducido es valido entonces codgio_valido pasaria hacer igual a codigo_str
                                codigo_validado = codigo_str;
                                break;
                            }
                        }
                        // pedimos el nombre de la cuenta
                        Console.Write("Ingresa el nombre de la cuenta ");
                        //para validar que el nombre de la cuenta no sea repetido introducido por la persona no exista utilizaremos este metodo llamado pedir_nombre_cuenta
                        string cuenta_introducida = pedir_nombre_cuenta(ruta_del_catalogo_de_cuentas);

                        //inicializamos la varible tipo_de_cuenta_introducida  por medio de un switch le pedimos el tipo de cuenta que es de 1 a 6
                        string tipo_de_cuenta_introducida = "";
                        Console.WriteLine($"Indique el tipo para la cuenta '{cuenta_introducida}':");
                        Console.WriteLine("1. Activa | 2. Pasiva | 3. Capital | 4. Gasto | 5. Costo | 6. Ingreso");
                        // pedimoos la opcion por medio de un metodo
                        int opcion_1_1 = pedir_opcion1_1();

                        switch (opcion_1_1)
                        {
                            // y le asignamos un valor a tipo de cuenta en funcion a lo que ingreso el usuario
                            case 1: tipo_de_cuenta_introducida = "activa"; break;
                            case 2: tipo_de_cuenta_introducida = "pasiva"; break;
                            case 3: tipo_de_cuenta_introducida = "capital"; break;
                            case 4: tipo_de_cuenta_introducida = "gasto"; break;
                            case 5: tipo_de_cuenta_introducida = "costo"; break;
                            case 6: tipo_de_cuenta_introducida = "ingreso"; break;
                        }

                        //lo mismo de la de arriba pero ahora se pide la naturaleza
                        string naturaleza_de_la_cuenta_introducida = "";
                        Console.WriteLine($"Indica la naturaleza de '{cuenta_introducida}':");
                        Console.WriteLine("1. Acreedora | 2. Deudora");
                        int opcion_1_1_4 = pedir_opcion_1_2();

                        //le asignamos una naturaleza en funcion de lo que indique el usuario
                        switch (opcion_1_1_4)
                        {
                            case 1: naturaleza_de_la_cuenta_introducida = "acreedora"; break;
                            case 2: naturaleza_de_la_cuenta_introducida = "deudora"; break;
                        }

                        //ahora se guarda dentro del archivo csv 
                        string nuevaLinea = $"{codigo_validado};{cuenta_introducida};{tipo_de_cuenta_introducida};{naturaleza_de_la_cuenta_introducida}";

                        //metodo File.AppendAllText() abre el archivo (Environment.NewLine) ingresa un salto de linea dentro del csv y guarda los cambios automáticamente.
                        File.AppendAllText(ruta_del_catalogo_de_cuentas, Environment.NewLine + nuevaLinea);

                        Console.WriteLine($"Exito la cuenta '{cuenta_introducida}' ({codigo_validado}) se guardo correctamente dentro del catalogo");
                        break;

                    case 3:
                        //logica del programa para elimnar una cuenta del archivo csv
                        Console.WriteLine("ELIMINACIÓN DE UNA CUENTA DEL CATÁLOGO");
                        //verificamos que el catalogo exista dentro del code
                        if (!File.Exists(ruta_del_catalogo_de_cuentas))
                        {
                            Console.WriteLine("No se encontro el archivo.");
                            break;
                        }

                        //eliminamos la cuenta en base el codigo es mas facil que por el nombre 
                        Console.Write("Ingrese el código de la cuenta a eliminar: ");
                        int codigo_eliminar = pedir_numero();

                        //y lo convertimos a string por ser mas facil de leer
                        string codigo_buscar = codigo_eliminar.ToString();

                        string[] lineas1_3 = File.ReadAllLines(ruta_del_catalogo_de_cuentas);

                        //usamos las listas como nos enseño el profe 
                        List<string> nuevas_lineas = new List<string>();

                        // le damos la a la variable de tipo bool false para que nos ayude a detectar si podemos eliminar la cuenta
                        bool encontrada = false;

                        foreach (string linea in lineas1_3)
                        {
                            //se salta los espacios en blanco este metodo
                            if (string.IsNullOrWhiteSpace(linea))
                                continue;

                            //el metodo ToSplit separa la lineas en funcion lo que le ingresemos al metodo en este caso ;
                            string[] datos = linea.Split(';');

                            //lo que pasa aqui es que la primera linea del csv son los encabezados solo se leeran una vez pero despues seran ignorados
                            if (datos[0].Trim().ToLower() == "Codigo")
                            {
                                nuevas_lineas.Add(linea);
                                continue;
                            }

                            //nos aseguramos que la linea tenga las columnas necesarias despues vemos que el codigo de la cuenta este dentro del csv que tenemos
                            if (datos.Length > 0 && datos[0].Trim().ToLower() == codigo_buscar.ToLower())
                            {
                                //si el codigo de la cuenta existe dentro del catalogo le cambiamos la datos a la variable encontrada
                                encontrada = true;

                                // mostramos que si la cuenta fue encontrada sera eliminada 
                                Console.WriteLine($"La cuenta '{datos[1]}' sera eliminada");
                                continue;
                            }

                            //y se salta la linea de que contiene la cuenta que se quiere eliminar
                            nuevas_lineas.Add(linea);
                        }

                        //si la cuenta no se encuentra se manda a mostrar este if
                        if (!encontrada)
                        {
                            Console.WriteLine("No se encontro la cuenta con ese código");
                            break;
                        }

                        File.WriteAllLines(ruta_del_catalogo_de_cuentas, nuevas_lineas);

                        //si todo fue correcto se manda el mensaje de confirmacion
                        Console.WriteLine("Cuenta eliminada correctamente.");
                        break;

                    case 4:
                        continuar_switch_uno = false; // Salimos del ciclo del submenu
                        break;

                }
            }
            break;

        case 2: // logica del punto numero 2 registrar movimiento contable 
            Console.WriteLine("REGISTRAR MOVIMIENTO CONTABLE");
            //evaluamos que el archivo este alli
            if (!File.Exists(ruta_de_los_movimientos_contables))
            {
                Console.WriteLine("No se encontro del archivo csv con los movimientos contable");
            }
            //para comenzar con el registro le pedimos al usuario que ingrese la fecha en formato yyyy-mm-dd
            Console.WriteLine("Ingresa la fecha del asiento de la siguiente manera (yyyy-mm-dd)");
            //llamando al metodo pedir_fecha validamos esta
            string fecha_del_movimiento = pedir_fecha();
            //mediante las List vamos a leer todo 
            List<string> lineas_temporales_movimiento = new List<string>();
            //inicializamos las variables encargadas de la futura evaluaion del debe y el haber
            double suma_debe = 0;
            double suma_haber = 0;
            string cuenta_formateada_para_csv = "";
            //bandera para repetir hasta que todo sea valido
            bool agregando_cuentas = true;
            while (agregando_cuentas)
            {
                while (true)
                {
                    Console.WriteLine("Ingresa el código de la cuenta afectada");
                    //pedimos el codigo y lo validamos por medio de pedir nuumero
                    string codigo_ingresado = pedir_codigo_texto();
                    //y le reasignamos al valor de codigo_Str
                    string codigo_str = codigo_ingresado.ToString();
                    //leemos todo el catalogo de los movimientos usando la funcion de File.ReadAllLines y le asignamos los valores al array lineas_catalogo
                    string[] lineas_catalogo = File.ReadAllLines(ruta_del_catalogo_de_cuentas);
                    bool cuenta_encontrada = false;
                    //recorremos lo leido por medio del foreach
                    foreach (string linea_cat in lineas_catalogo)
                    {
                        //nos aseguramos que la linea no se encuentre vacia
                        if (string.IsNullOrWhiteSpace(linea_cat)) continue;
                        string[] datos_cat = linea_cat.Split(';');
                        //omitimos los encabezados usando
                        if (datos_cat[0].Trim().ToLower() == "Codigo") continue;
                        //como queremos ver los cosigos de las cuentas nos saltamos la fecha
                        // CORREGIDO: Se usa Equals para comparar de forma segura ignorando diferencias menores
                        if (datos_cat.Length >= 2 && datos_cat[0].Trim().Equals(codigo_str, StringComparison.OrdinalIgnoreCase))
                        {
                            //le reasignamos el valor si la cuenta fue encontrada
                            cuenta_encontrada = true;
                            //el valor del codigo sera el del codgio
                            string codigo_cat = datos_cat[0].Trim();
                            //y el nombre tambien
                            string nombre_cat = datos_cat[1].Trim();
                            //la razon para ponerlos como cadena fue para hacer mas facil esta lectura y suma del codigo y la cuenta afectada
                            cuenta_formateada_para_csv = $"{codigo_cat}-{nombre_cat}";
                            break;
                        }
                    }
                    if (cuenta_encontrada)
                    {
                        Console.WriteLine($"Cuenta valida {cuenta_formateada_para_csv}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Código sin existencia {codigo_str} ");
                    }
                }
                // vamos a pedir donde se resgitran los montos para validar que sean iguales
                string monto_debe_str = "0";
                string monto_haber_str = "0";
                double monto_actual = 0;
                while (true)
                {
                    // le pedimos al usuario que indique en que parte del codogo se va a sumar
                    Console.WriteLine("En donde se registra este movimiento?");
                    Console.WriteLine("1 Debe / 2 Haber");
                    int opcion_debe_haber = pedir_opcion_1_2();
                    //le pedimmos que ingrese la cantidad permitiendo decimales con tryParse
                    Console.Write("Ingrese la cantidad: ");
                    string cantidad_ingresada_por_el_usuario = Console.ReadLine()?.Trim() ?? "";
                    if (double.TryParse(cantidad_ingresada_por_el_usuario, out monto_actual) && monto_actual >= 0)
                    {
                        if (opcion_debe_haber == 1)
                        {
                            monto_debe_str = monto_actual.ToString("0.00");
                            suma_debe += monto_actual;
                        }
                        else
                        {
                            monto_haber_str = monto_actual.ToString("0.00");
                            suma_haber += monto_actual;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Cantidad invalida, Intente de nuevo");
                    }
                }
                //le pedimos al usuario el contexto de lo que hara con esa cuenta
                Console.WriteLine("Le pedimos el contexto del movimiento al usuario");
                string concepto_del_movimiento = Console.ReadLine() ?? "";
                // Construimos la línea con el formato exacto de tu CSV
                string nueva_linea_mov = $"{fecha_del_movimiento};{cuenta_formateada_para_csv};{monto_debe_str};{monto_haber_str};{concepto_del_movimiento}";
                //y la agregamos con este metodo
                lineas_temporales_movimiento.Add(nueva_linea_mov);
                //le pedimos que si quiere agregar otro movimiento en este asiento
                Console.WriteLine("Desea agregar otra cuenta a este registro");
                Console.WriteLine("1. si | 2. no");
                int continuar_agregando = pedir_opcion_1_2();
                if (continuar_agregando == 2)
                {
                    agregando_cuentas = false;
                }
            }

            //validamos que el debe y haber sean iguales como ultimo acto de amor
            Console.WriteLine("");
            Console.WriteLine("RESUMEN DEL REGISTRO");
            //le mostramos los datos sl usuario 
            Console.WriteLine($"Total Debe:  {suma_debe:0.00}");
            Console.WriteLine($"Total Haber: {suma_haber:0.00}");
            //en caso el debe y el haber sean iguales y mayores a cero por que no pueden ser negativos se registra el movimiento exitosamenre por medio del File.AppendAllText
            if (suma_debe == suma_haber && suma_debe > 0)
            {
                foreach (string linea_final in lineas_temporales_movimiento)
                {
                    File.AppendAllText(ruta_de_los_movimientos_contables, Environment.NewLine + linea_final);
                }
                Console.WriteLine("Todo esta bien se agrego el movimiento");
            }
            else
            {
                Console.WriteLine("El debe y el haber no cuadran no se registrara hasta que cuadre");
            }
            break;


        case 3: // Lógica del switch número 3: consultar catálogo de cuentas
            Console.WriteLine();
            Console.WriteLine("CATÁLOGO DE LAS CUENTAS");
            Console.WriteLine();

            if (!File.Exists(ruta_del_catalogo_de_cuentas))
            {
                Console.WriteLine($"No se encontró el archivo de las cuentas: {ruta_del_catalogo_de_cuentas}");
                break;
            }

            string[] lineas_catalogo_s3 = File.ReadAllLines(ruta_del_catalogo_de_cuentas);
            if (lineas_catalogo_s3.Length == 0)
            {
                Console.WriteLine("El archivo se encuentra vacío.");
                break;
            }

            Console.WriteLine($"{"Código",-10} | {"Cuenta",-40} | {"Tipo",-12} | {"Naturaleza",-12}");
            Console.WriteLine(new string('-', 78));

            bool primera_linea_s3 = true;
            foreach (string fila in lineas_catalogo_s3)
            {
                if (string.IsNullOrWhiteSpace(fila)) continue;

                if (primera_linea_s3 && (fila.ToLower().StartsWith("código") || fila.ToLower().StartsWith("codigo")))
                {
                    primera_linea_s3 = false;
                    continue;
                }

                string[] datos_s3 = fila.Split(';');
                if (datos_s3.Length >= 4)
                {
                    string codigo_cuenta = datos_s3[0].Trim();
                    string nombre_cuenta = datos_s3[1].Trim();
                    string tipo_cuenta = datos_s3[2].Trim();
                    string naturaleza_cuenta = datos_s3[3].Trim();

                    Console.WriteLine($"{codigo_cuenta,-10} | {nombre_cuenta,-40} | {tipo_cuenta,-12} | {naturaleza_cuenta,-12}");
                }
                else
                {
                    Console.WriteLine("La línea se encuentra incompleta, revísala.");
                }
            }
            break;

        case 4: // El switch 4 muestra los movimientos contables
            Console.WriteLine("MOSTRANDO LOS MOVIMIENTOS CONTABLES");
            if (!File.Exists(ruta_de_los_movimientos_contables))
            {
                Console.WriteLine($"No se encontró el archivo de movimientos en la ruta: {ruta_de_los_movimientos_contables}");
                break;
            }

            string[] lineas_csv_movimientos = File.ReadAllLines(ruta_de_los_movimientos_contables);
            if (lineas_csv_movimientos.Length == 0)
            {
                Console.WriteLine("Los movimientos están vacíos.");
                break;
            }

            Console.WriteLine($"{"FECHA",-10} | {"CUENTA",-50} | {"ASIENTO",-8} | {"DEBE",-10} | {"HABER",-10} | {"CONCEPTO"}");
            Console.WriteLine(new string('-', 105));

            bool primera_linea4 = true;
            foreach (string lineas in lineas_csv_movimientos)
            {
                if (string.IsNullOrWhiteSpace(lineas)) continue;

                if (primera_linea4 && lineas.ToLower().StartsWith("fecha"))
                {
                    primera_linea4 = false;
                    continue;
                }

                string[] datos = lineas.Split(';');
                if (datos.Length >= 6)
                {
                    // Variables correctamente mapeadas a las 6 columnas del CSV
                    string fecha = datos[0].Trim();
                    string cuenta = datos[1].Trim();
                    string asiento = datos[2].Trim();
                    string debe = datos[3].Trim();
                    string haber = datos[4].Trim();
                    string concepto = datos[5].Trim();

                    Console.WriteLine($"{fecha,-10} | {cuenta,-50} | {asiento,-8} | {debe,-10} | {haber,-10} | {concepto}");
                }
                else
                {
                    Console.WriteLine($"[Línea incompleta]: {lineas}");
                }
            }
            break;

        case 5:
{
    Console.WriteLine();
    Console.WriteLine("==================================================");
    Console.WriteLine("               ESTADO DE RESULTADOS               ");
    Console.WriteLine("==================================================");

    if (!File.Exists(ruta_del_catalogo_de_cuentas) || !File.Exists(ruta_de_los_movimientos_contables))
    {
        Console.WriteLine("No se encontro el catalogo o el archivo de movimientos. Revisa las rutas.");
        break;
    }

    // carga el catalogo de manera dinamica mapeando el codigo y el nombre a su tipo correspondiente
    var catalogoResultados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        string codigo = datos[0].Trim();
        string nombre = datos[1].Trim();
        string tipo = datos[2].Trim().ToLower();

        // filtramos las cuentas obligatorias ppara el estado de resultados
        if (tipo == "ingreso" || tipo == "costo" || tipo == "gasto")
        {
            catalogoResultados[codigo] = tipo;
            catalogoResultados[nombre] = tipo;
        }
    }

    var ingresos = new Dictionary<string, decimal>();
    var costos = new Dictionary<string, decimal>();
    var gastos = new Dictionary<string, decimal>();
    int movimientos_sin_catalogo = 0;

    // busca en todos los movements limpiando los formatos y aumulando saldos
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (linea.ToLower().Contains("fecha") || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 5) continue;

        string cuentaMov = datos[1].Trim();
        
        // ignoramos llos puntos para obtener bien los montos 
        string debeStr = datos[2].Trim().Replace(".", "").Replace(",", ".");
        string haberStr = datos[3].Trim().Replace(".", "").Replace(",", ".");

        decimal.TryParse(debeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal debe);
        decimal.TryParse(haberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal haber);

        // loocking for a que tipo pertenece la cuenta del movimiento
        string tipoEncontrado = "";
        string nombreCuentaLimpio = cuentaMov;

        foreach (var kvp in catalogoResultados)
        {
            if (cuentaMov.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                tipoEncontrado = kvp.Value;
                break;
            }
        }

        if (string.IsNullOrEmpty(tipoEncontrado))
        {
            // ignoramos los movimientos que  no pertenecen a ingresos,costos y gastos
            continue;
        }

        // clasificamos segun naturaleza contable
        if (tipoEncontrado == "ingreso")
        {
            ingresos[nombreCuentaLimpio] = ingresos.GetValueOrDefault(nombreCuentaLimpio) + (haber - debe);
        }
        else if (tipoEncontrado == "costo")
        {
            costos[nombreCuentaLimpio] = costos.GetValueOrDefault(nombreCuentaLimpio) + (debe - haber);
        }
        else if (tipoEncontrado == "gasto")
        {
            gastos[nombreCuentaLimpio] = gastos.GetValueOrDefault(nombreCuentaLimpio) + (debe - haber);
        }
    }

    // imprime el estado de resultados
    Console.WriteLine(new string('=', 62));
    Console.WriteLine($"{"ESTADO DE RESULTADOS",41}");
    Console.WriteLine(new string('=', 62));

    decimal total_ingresos = imprimir_seccion("INGRESOS", ingresos);
    decimal total_costos = imprimir_seccion("(-) COSTOS", costos);

    decimal utilidad_bruta = total_ingresos - total_costos;
    Console.WriteLine(new string('-', 62));
    Console.WriteLine($"{"UTILIDAD BRUTA",-46}{utilidad_bruta,14:N2}");
    Console.WriteLine();

    decimal total_gastos = imprimir_seccion("(-) GASTOS", gastos);

    decimal utilidad_neta = utilidad_bruta - total_gastos;
    Console.WriteLine(new string('=', 62));
    Console.WriteLine($"{(utilidad_neta >= 0 ? "UTILIDAD NETA" : "PERDIDA NETA"),-46}{utilidad_neta,14:N2}");
    Console.WriteLine(new string('=', 62));
    Console.WriteLine("==================================================");

    break;
}
    

    case 6:
{
    Console.WriteLine();
    Console.WriteLine("==================================================");
    Console.WriteLine("        ESTADO DE SITUACION FINANCIERA            ");
    Console.WriteLine("==================================================");

    if (!File.Exists(ruta_del_catalogo_de_cuentas) || !File.Exists(ruta_de_los_movimientos_contables))
    {
        Console.WriteLine("No se encontro el catalogo o el archivo de movimientos. Revisa las rutas.");
        break;
    }

    // carga el catalogo para poder clacificar las cuentas por su tipo
    var catalogoBalance = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        string codigo = datos[0].Trim();
        string nombre = datos[1].Trim();
        string tipo = datos[2].Trim().ToLower();

        if (tipo == "activo" || tipo == "pasivo" || tipo == "patrimonio" || tipo == "capital")
        {
            catalogoBalance[codigo] = tipo;
            catalogoBalance[nombre] = tipo;
        }
    }

    var activos = new Dictionary<string, decimal>();
    var pasivos = new Dictionary<string, decimal>();
    var patrimonio = new Dictionary<string, decimal>();

    // recorre los movimiento limpiando formatos y acumulando lo saldos
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (linea.ToLower().Contains("fecha") || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 5) continue;

        string cuentaMov = datos[1].Trim();
        
        // ignoramos llos puntos para obtener bien los montos 
        string debeStr = datos[2].Trim().Replace(".", "").Replace(",", ".");
        string haberStr = datos[3].Trim().Replace(".", "").Replace(",", ".");

        decimal.TryParse(debeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal debe);
        decimal.TryParse(haberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal haber);

        string tipoEncontrado = "";
        foreach (var kvp in catalogoBalance)
        {
            if (cuentaMov.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                tipoEncontrado = kvp.Value;
                break;
            }
        }

        if (string.IsNullOrEmpty(tipoEncontrado)) continue;

        // naturaleza de las cuentas de balance:
        // activo: naturaleza deudora (debe/haber)
        // pasivo y patrimonio: naturaleza acreedora (haber/debe)
        
        if (tipoEncontrado == "activo")
        {
            activos[cuentaMov] = activos.GetValueOrDefault(cuentaMov) + (debe - haber);
        }
        else if (tipoEncontrado == "pasivo")
        {
            pasivos[cuentaMov] = pasivos.GetValueOrDefault(cuentaMov) + (haber - debe);
        }
        else if (tipoEncontrado == "patrimonio" || tipoEncontrado == "capital")
        {
            patrimonio[cuentaMov] = patrimonio.GetValueOrDefault(cuentaMov) + (haber - debe);
        }
    }

      //hacemos tremenda obra de arte usando lo mismo del caso 7 para pioder
      //calcular y saber si se resta o suma recorriendo todos los archivos 
  
    var catalogoResultados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;
        catalogoResultados[datos[0].Trim()] = datos[2].Trim().ToLower();
        catalogoResultados[datos[1].Trim()] = datos[2].Trim().ToLower();
    }

    decimal totalIngresos = 0, totalCostos = 0, totalGastos = 0;
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea) || linea.ToLower().Contains("fecha") || linea.StartsWith(";")) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 5) continue;
        string cuentaMov = datos[1].Trim();
        decimal.TryParse(datos[2].Trim().Replace(".", "").Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal debe);
        decimal.TryParse(datos[3].Trim().Replace(".", "").Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal haber);

        foreach (var kvp in catalogoResultados)
        {
            if (cuentaMov.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                if (kvp.Value == "ingreso") totalIngresos += (haber - debe);
                else if (kvp.Value == "costo") totalCostos += (debe - haber);
                else if (kvp.Value == "gasto") totalGastos += (debe - haber);
                break;
            }
        }
    }
    decimal utilidadNetaPeriodo = totalIngresos - (totalCostos + totalGastos);

    // imprimimos el estado de situacion financiera
    Console.WriteLine();
    Console.WriteLine("--- ACTIVOS ---");
    decimal totalActivos = 0;
    foreach (var c in activos.OrderBy(c => c.Key))
    {
        Console.WriteLine($"   {c.Key,-43}{c.Value,14:N2}");
        totalActivos += c.Value;
    }
    if (activos.Count == 0) Console.WriteLine($"   {"(sin movimientos)",-43}");
    Console.WriteLine($"{"Total Activos",-46}{totalActivos,14:N2}");

    Console.WriteLine();
    Console.WriteLine("--- PASIVOS ---");
    decimal totalPasivos = 0;
    foreach (var c in pasivos.OrderBy(c => c.Key))
    {
        Console.WriteLine($"   {c.Key,-43}{c.Value,14:N2}");
        totalPasivos += c.Value;
    }
    if (pasivos.Count == 0) Console.WriteLine($"   {"(sin movimientos)",-43}");
    Console.WriteLine($"{"Total Pasivos",-46}{totalPasivos,14:N2}");

    Console.WriteLine();
    Console.WriteLine("--- PATRIMONIO ---");
    decimal totalPatrimonioBase = 0;
    foreach (var c in patrimonio.OrderBy(c => c.Key))
    {
        Console.WriteLine($"   {c.Key,-43}{c.Value,14:N2}");
        totalPatrimonioBase += c.Value;
    }
    // incluyo la uti del periodo dentro del patri final
    Console.WriteLine($"   {"Utilidad / Perdida del Periodo",-43}{utilidadNetaPeriodo,14:N2}");
    
    decimal totalPatrimonioFinal = totalPatrimonioBase + utilidadNetaPeriodo;
    Console.WriteLine($"{"Total Patrimonio",-46}{totalPatrimonioFinal,14:N2}");

    Console.WriteLine(new string('=', 62));
    decimal totalPasivoMasPatrimonio = totalPasivos + totalPatrimonioFinal;
    Console.WriteLine($"{"TOTAL PASIVO + PATRIMONIO",-46}{totalPasivoMasPatrimonio,14:N2}");
    Console.WriteLine(new string('=', 62));

    if (Math.Abs(totalActivos - totalPasivoMasPatrimonio) < 0.01m)
    {
        Console.WriteLine("El balance cuadra perfectamente Activo = Pasivo + Patrimonio");
    }
    else
    {
        Console.WriteLine("El balance no balancea, revise sus movimientos (warning)");
    }
    Console.WriteLine("==================================================");
    break;
}


  case 7:
{
    Console.WriteLine();
    Console.WriteLine("==================================================");
    Console.WriteLine("       ESTADO DE CAMBIOS EN EL PATRIMONIO         ");
    Console.WriteLine("==================================================");

    if (!File.Exists(ruta_del_catalogo_de_cuentas) || !File.Exists(ruta_de_los_movimientos_contables))
    {
        Console.WriteLine("No se encontraron los archivos de cuentas o movimientos.");
        break;
    }

    var catalogoPatrimonio = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    var catalogoGeneral = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    // lee los catalogos de cuentas para tener el patrimonio de manera correcta
    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        string codigo = datos[0].Trim();
        string nombre = datos[1].Trim();
        string tipo = datos[2].Trim();

        catalogoGeneral[codigo] = tipo;
        catalogoGeneral[nombre] = tipo;

        if (tipo.Equals("capital", StringComparison.OrdinalIgnoreCase) || tipo.Equals("patrimonio", StringComparison.OrdinalIgnoreCase))
        {
            catalogoPatrimonio[codigo] = nombre;
            catalogoPatrimonio[nombre] = nombre;
        }
    }

    decimal patrimonioInicial = 0;
    decimal aportes = 0;
    decimal retiros = 0;
    decimal totalIngresos = 0;
    decimal totalCostos = 0;
    decimal totalGastos = 0;

    // lee los movimientos contables en busca de las modificaciones 
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (linea.ToLower().Contains("fecha") || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 5) continue;

        string cuentaMov = datos[1].Trim();
        string codigoBusqueda = cuentaMov.Contains("-") ? cuentaMov.Split('-')[0].Trim() : cuentaMov;

        // elimina los puntos a la hora de recorrer los archivos para una lectuura clara de los montos 
        string debeStr = datos[2].Trim().Replace(".", "").Replace(",", ".");
        string haberStr = datos[3].Trim().Replace(".", "").Replace(",", ".");

        decimal.TryParse(debeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal debe);
        decimal.TryParse(haberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal haber);

        // se evaluan los aportes y los retiros para sacar el patrimoniio
        bool esPatrimonio = false;
        foreach (var key in catalogoPatrimonio.Keys)
        {
            if (cuentaMov.Contains(key, StringComparison.OrdinalIgnoreCase))
            {
                esPatrimonio = true;
                break;
            }
        }

        if (esPatrimonio)
        {
            string concepto = datos[4].Trim().ToLower();
            if (concepto.Contains("retiro") || concepto.Contains("dividendo"))
            {
                retiros += (debe - haber);
            }
            else
            {
                aportes += (haber - debe);
            }
        }

        // evaluamos los resultados para la utilidad neta 
        foreach (var kvp in catalogoGeneral)
        {
            if (cuentaMov.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                string tipoCuenta = kvp.Value;
                if (tipoCuenta.Equals("ingreso", StringComparison.OrdinalIgnoreCase))
                    totalIngresos += (haber - debe);
                else if (tipoCuenta.Equals("costo", StringComparison.OrdinalIgnoreCase))
                    totalCostos += (debe - haber);
                else if (tipoCuenta.Equals("gasto", StringComparison.OrdinalIgnoreCase))
                    totalGastos += (debe - haber);
                break;
            }
        }
    }

    decimal utilidadFinalCalculada = totalIngresos - (totalCostos + totalGastos);
    decimal patrimonioFinal = patrimonioInicial + aportes + utilidadFinalCalculada - retiros;

    Console.WriteLine($"{"Patrimonio Inicial:",-40} {patrimonioInicial,15:N2}");
    Console.WriteLine($"{"(+) Aportes de Capital:",-40} {aportes,15:N2}");
    Console.WriteLine($"{"(+) Utilidad del Periodo:",-40} {utilidadFinalCalculada,15:N2}");
    Console.WriteLine($"{"(-) Retiros:",-40} {retiros,15:N2}");
    Console.WriteLine(new string('-', 58));
    Console.WriteLine($"{"PATRIMONIO FINAL:",-40} {patrimonioFinal,15:N2}");
    Console.WriteLine("==================================================");
    break;
}
  
    case 8:
{
    Console.WriteLine();
    Console.WriteLine("==================================================");
    Console.WriteLine("          ESTADO DE FLUJOS DE EFECTIVO            ");
    Console.WriteLine("==================================================");

    if (!File.Exists(ruta_del_catalogo_de_cuentas) || !File.Exists(ruta_de_los_movimientos_contables))
    {
        Console.WriteLine("No se encontro el catalogo o el archivo de movimientos. Revisa las rutas.");
        break;
    }

    decimal flujoOperativo = 0;
    decimal flujoInversion = 0;
    decimal flujoFinanciamiento = 0;

    //  busamos las transacciones que afectan el efectivo recorriendo los movimientos
    
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (linea.ToLower().Contains("fecha") || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 5) continue;

        string cuentaMov = datos[1].Trim();
        string concepto = datos[4].Trim().ToLower();

        // definir si la cuenta es de efectivo o bancco
        bool esEfectivo = cuentaMov.Contains("1101") || cuentaMov.Contains("1102") || 
                          cuentaMov.Contains("caja", StringComparison.OrdinalIgnoreCase) || 
                          cuentaMov.Contains("banco", StringComparison.OrdinalIgnoreCase);

        if (!esEfectivo) continue;

        // ignoramos llos puntos para obtener bien los montos 
        string debeStr = datos[2].Trim().Replace(".", "").Replace(",", ".");
        string haberStr = datos[3].Trim().Replace(".", "").Replace(",", ".");

        decimal.TryParse(debeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal debe);
        decimal.TryParse(haberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal haber);

        // si el efect entra debe y es positivo si no es negativo y haber
        decimal cambioEfectivo = debe - haber;

        //  clasifica segun palabras claves de los conceptps de las transacciones
        if (concepto.Contains("aporte") || concepto.Contains("capital") || concepto.Contains("prestamo") || concepto.Contains("financiamiento") || concepto.Contains("socio"))
        {
            flujoFinanciamiento += cambioEfectivo;
        }
        else if (concepto.Contains("equipo") || concepto.Contains("mobiliario") || concepto.Contains("vehiculo") || concepto.Contains("maquinaria") || concepto.Contains("activo"))
        {
            flujoInversion += cambioEfectivo;
        }
        else
        {
            flujoOperativo += cambioEfectivo;
        }
    }

    decimal flujoNetoTotal = flujoOperativo + flujoInversion + flujoFinanciamiento;

    // imprimimos reporte
    Console.WriteLine($"{"1. Actividades de Operacion:",-42} {flujoOperativo,15:N2}");
    Console.WriteLine($"{"2. Actividades de Inversion:",-42} {flujoInversion,15:N2}");
    Console.WriteLine($"{"3. Actividades de Financiamiento:",-42} {flujoFinanciamiento,15:N2}");
    Console.WriteLine(new string('-', 59));
    Console.WriteLine($"{"FLUJO NETO DE EFECTIVO:",-42} {flujoNetoTotal,15:N2}");
    Console.WriteLine("==================================================");
    break;
}

    case 9:
        Console.WriteLine("Saliendo del programa. Tenga un buen dia.");
        continuar = false; // Rompe el ciclo while
        break;


    }
}



//====00 metodos cabron para todos mantengan esta mierda leible y ordenada para que no haya problemas a futuro----NOP
void MostrarMenu()
{
    Console.WriteLine();
    Console.WriteLine("===============================");
    Console.WriteLine("BIENVENIDO AL SISTEMA CONTABLE");
    Console.WriteLine("===============================");
    Console.WriteLine();
    Console.WriteLine("Por favor elige la opcion que desees realizar");
    Console.WriteLine("1. Administrar catálogo de cuentas ");
    Console.WriteLine("2. Registrar movimiento contable ");
    Console.WriteLine("3. Consultar catálogo de cuentas ");
    Console.WriteLine("4. Consultar movimientos ");
    Console.WriteLine("5. Estado de Resultados ");
    Console.WriteLine("6. Estado de Situación Financiera ");
    Console.WriteLine("7. Estado de Cambios en el Patrimonio ");
    Console.WriteLine("8. Estado de Flujos de Efectivo ");
    Console.WriteLine("9. Salir ");
    Console.WriteLine();
}

int PedirOpcion()
{
    while (true)
    {
        Console.Write("Seleccione una opcion: ");
        string entrada = Console.ReadLine() ?? "";

        if (int.TryParse(entrada, out int opcion))
        {

            if (opcion >= 1 && opcion <= 9)
            {
                return opcion;
            }
        }
        Console.WriteLine("La opcion seleccionada es invalida, Intente de nuevo con una opción entre el 1 y el 9");
    }
}

int pedir_opcion1_1()
{
    while (true)
    {
        Console.Write("Seleccione una opcion: ");
        string entrada = Console.ReadLine() ?? "";

        if (int.TryParse(entrada, out int opcion))
        {

            if (opcion >= 1 && opcion <= 6)
            {
                return opcion;
            }
        }
        Console.WriteLine("La opcion seleccionada es invalida, Intente de nuevamente con las opciones mostradas");
    }
}

int pedir_opcion_switch_uno()
{
    while (true)
    {
        Console.Write("Seleccione una opcion: ");
        string entrada = Console.ReadLine() ?? "";

        if (int.TryParse(entrada, out int opcion))
        {

            if (opcion >= 1 && opcion <= 4)
            {
                return opcion;
            }
        }
        Console.WriteLine("Error, Ingrese correctamente con el menu mostrado");
    }
}

int pedir_numero()
{
    while (true)
    {
        Console.Write("");
        string entrada = Console.ReadLine() ?? "";

        if (int.TryParse(entrada, out int opcion))
        {

            if (opcion >= 0)
            {
                return opcion;
            }
        }
        Console.WriteLine("La opcion seleccionada es invalida, Intente de nuevo");
    }
}

int pedir_opcion_1_2()
{
    while (true)
    {
        Console.Write("Seleccione una opcion: ");
        string entrada = Console.ReadLine() ?? "";

        if (int.TryParse(entrada, out int opcion))
        {

            if (opcion >= 1 && opcion <= 2)
            {
                return opcion;
            }
        }
        Console.WriteLine("La opcion seleccionada es invalida, Intente de nuevo con una opcion entre el 1 y el 2");
    }
}

string pedir_nombre_cuenta(string ruta_catalogo)
{
    //mientras el nombre sea invalido el ciclo se repetira hasta el name sea valida
    while (true)
    {
        //primero pedimos el nombre de la cuenta el metodo ToTrim se salta los espacios en blanco
        string nombre = Console.ReadLine()?.Trim() ?? "";

        //si el usuario no ingresa ningun valor se activa el metodo IsNullOrWhiteSpace donde le indicariamos al usuario que el nombre no puede estar en blano
        if (string.IsNullOrWhiteSpace(nombre) || nombre.Any(char.IsDigit))
        {
            Console.WriteLine("El nombre de la cuenta no puede estar vacio, ni contener numeros");
            continue;
        }

        //por medio de la varibale existe nos aseguraremos que el nombre no se haya introducido antes
        bool existe = false;

        //esta matriz esta para ser capaz de leeer todas las filas del codigo por medio del foreach
        string[] lineas = File.ReadAllLines(ruta_catalogo);

        foreach (string linea in lineas)
        {
            //si el espacio esta vacio se lo salta
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            //los ; estan como saltos entre los cuadrados
            string[] datos = linea.Split(';');

            //para leer las cuentas del csv debe acceder a los nombres pero antes de eso van los codigos por eso ocupamos esta validacion que se salta datos[0] la cual contiene los codes a los cuales no queremos llegar en esta iteracion si no que iremos de una sola vez a las cuentas
            if (datos.Length > 1)
            {
                //leemos que la cuenta no se encuentre dentro de las matriz que almacena las cuentas
                string cuenta_existente = datos[1].Trim();

                //y volvemos asar el metodo ToLower para validar mas facil mente
                if (cuenta_existente.ToLower() == nombre.ToLower())
                {
                    //si no existe se le cambia el tipo a existe y salimos del metodo caso contrario pasa al otro if
                    existe = true;
                    break;
                }
            }
        }

        // como la cuenta si existe se le muestra al usuario este mensaje indicando que la cuenta ya existe y debe volver a ingresar otro nombre
        if (existe)
        {
            Console.WriteLine($"La cuenta '{nombre}' ya existe en el catalogo");
            continue;
        }
        return nombre;
    }
}

void mostrar_menu1()
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine(" ADMINISTRAR CATÁLOGO DE CUENTAS");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Ver catálogo de cuentas");
    Console.WriteLine("2. Agregar nueva cuenta");
    Console.WriteLine("3. Eliminar una cuenta");
    Console.WriteLine("4. Volver al menú principal");
    Console.Write("Indique la acción que desea realizar");
    Console.WriteLine();

}

//!!!!!!!!comentar esta mierda con urgencia extrema, duilio en el menu no crea que sea muy necesario comentarlo o bueno tal vez
string pedir_fecha()
{
    //repetimos el metodo hasta que el usuario meta una fecha valida
    while (true)
    {
        string entrada = Console.ReadLine() ?? "";
        //si esta vacia se lo hacemos saber al usuario por medio de una validacion
        if (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("La fecha no puede quedar vacia ");
            continue;
        }
        try
        {
            if (DateTime.TryParseExact(entrada, "yyyy-MM-dd",
    System.Globalization.CultureInfo.InvariantCulture,
    System.Globalization.DateTimeStyles.None,
    out DateTime fecha_valida))
            {
                if (fecha_valida.Year >= 2000 && fecha_valida.Year <= 2026)
                {
                    return fecha_valida.ToString("yyyy-MM-dd");
                }
                else
                {
                    Console.WriteLine("La fecha se encuentra fuera del rango de registro");
                }
            }
            else
            {
                Console.WriteLine("Formato invalido de fecha por favor intentelo con el formato yyyy-mm-dd");
            }
        }
        catch (Exception )
        {
            Console.WriteLine("Se ha ocasionado un ERROR");
        }
    }
}

string pedir_codigo_texto()
{
    while (true)
    {
        string entrada = Console.ReadLine()?.Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(entrada))
        {
            return entrada;
        }
        Console.WriteLine("El código no puede estar vacío. Intente de nuevo.");
    }
}

static decimal imprimir_seccion(string titulo, Dictionary<string, decimal> cuentas)
{
    Console.WriteLine(titulo);
    
    decimal total = 0;
    foreach (var c in cuentas.OrderBy(c => c.Key))
    {
        Console.WriteLine($"   {c.Key,-43}{c.Value,14:N2}");
        total += c.Value;
    }

    if (cuentas.Count == 0)
        Console.WriteLine($"   {"(sin movimientos)",-43}");

    Console.WriteLine($"{"Total " + titulo.Replace("(-) ", "").ToLower(),-46}{total,14:N2}");
    return total;
}