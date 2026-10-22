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
                        Console.WriteLine("CATALOGO DE LAS CUENTAS");
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
                            break;
                        }

                        // esto sirve para que el codigo se vea mas ordenado 
                        Console.WriteLine($"{"Codigo",-6} | {"Cuenta",-35} | {"Tipo",-15} | {"Naturaleza",-18}");
                        Console.WriteLine(new string('-', 90));

                        //le damos el valor de verdadero a la nueva variable de tipo bool
                        bool primera_linea1_1 = true;

                        //va leyendo uno por una las lineas que le metimos por medio del csv
                        foreach (string lineas in lineas_csv_del_catalogo1)
                        {
                            //no lee las lineas que se encuentren vacias es lo que hace el metodo IsNullOrWhiteSpace
                            if (string.IsNullOrWhiteSpace(lineas)) continue;

                            if (primera_linea1_1 && es_encabezado(lineas))
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

                    case 2:// La logica del 1.2 es agregar una cuenta al archivo csv
                        Console.WriteLine();
                        Console.WriteLine("AGREGANDO UNA CUENTA NUEVA EN EL CATALOGO DE CUENTAS");

                        //nos aseguramos otra vez que el archivo ese alli
                        if (!File.Exists(ruta_del_catalogo_de_cuentas))
                        {
                            Console.WriteLine("El archivo no se encuentra. Asegurate de revisar la ruta y las propiedades.");
                            break;
                        }
                        // pese hacer un numero lo que se le pide al usuario se inicializa como string para poder validar mejor la opcion
                        Console.Write("Ingrese el codigo de nueva cuenta: ");
                         string codigo_validado = "";

                        // este bucle while existe para que el usuario no salga de la ejecucion hasta que ingrese un codigo de la cuenta que sea valido
                        while (true)
                        {
                            string codigo_str = pedir_codigo_texto();

                            // el metodo ReadAllLines sirve para que la pc lea el archivo csv y almacene todos los datos en el arreglo lineas_csv_del_catalogo
                            string[] lineas_existentes = File.ReadAllLines(ruta_del_catalogo_de_cuentas);

                            // esta variable es la que valida que el codigo no haya sido introducido antes 
                            bool existe = false;

                            //leemos todas las lineas del csv 
                            foreach (string linea in lineas_existentes)
                            {
                                // esta funcion se salta las lineas que se encuentren en blanco
                                if (string.IsNullOrWhiteSpace(linea)) continue;

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
                                Console.WriteLine($"El codigo {codigo_str} ya tiene una cuenta asignada. por favor ingresa otro codigo");
                            }
                            else
                            {
                                //si el codigo introducido es valido entonces codgio_valido pasaria hacer igual a codigo_str
                                codigo_validado = codigo_str;
                                break;
                            }
                        }
                        // pedimos el nombre de la cuenta
                        Console.Write("Ingresa el nombre de la cuenta: ");
                        string cuenta_introducida = pedir_nombre_cuenta(ruta_del_catalogo_de_cuentas);

                        //inicializamos la varible tipo_de_cuenta_introducida  por medio de un switch le pedimos el tipo de cuenta que es de 1 a 6
                        string tipo_de_cuenta_introducida = "";
                        Console.WriteLine($"Indique el tipo para la cuenta '{cuenta_introducida}':");
                        Console.WriteLine("1. Activo | 2. Pasivo | 3. Patrimonio | 4. Gasto | 5. Costo | 6. Ingreso");
                        int opcion_1_1 = pedir_opcion1_1();

                        switch (opcion_1_1)
                        {
                            // y le asignamos un valor a tipo de cuenta en funcion a lo que ingreso el usuario
                            case 1: tipo_de_cuenta_introducida = "Activo"; break;
                            case 2: tipo_de_cuenta_introducida = "Pasivo"; break;
                            case 3: tipo_de_cuenta_introducida = "Patrimonio"; break;
                            case 4: tipo_de_cuenta_introducida = "Gasto"; break;
                            case 5: tipo_de_cuenta_introducida = "Costo"; break;
                            case 6: tipo_de_cuenta_introducida = "Ingreso"; break;
                        }

                        //lo mismo de la de arriba pero ahora se pide la naturaleza
                        string naturaleza_de_la_cuenta_introducida = "";
                        Console.WriteLine($"Indica la naturaleza de '{cuenta_introducida}':");
                        Console.WriteLine("1. Acreedora | 2. Deudora");
                        int opcion_1_1_4 = pedir_opcion_1_2();

                        //le asignamos una naturaleza en funcion de lo que indique el usuario
                        switch (opcion_1_1_4)
                        {
                            case 1: naturaleza_de_la_cuenta_introducida = "Acreedora"; break;
                            case 2: naturaleza_de_la_cuenta_introducida = "Deudora"; break;
                        }

                        //ahora se guarda dentro del archivo csv 
                        string nuevaLinea = $"{codigo_validado};{cuenta_introducida};{tipo_de_cuenta_introducida};{naturaleza_de_la_cuenta_introducida}";

                        //metodo File.AppendAllText() abre el archivo (Environment.NewLine) ingresa un salto de linea dentro del csv y guarda los cambios automaticamente.
                        File.AppendAllText(ruta_del_catalogo_de_cuentas, Environment.NewLine + nuevaLinea);

                        Console.WriteLine($"Exito la cuenta '{cuenta_introducida}' ({codigo_validado}) se guardo correctamente dentro del catalogo");
                        break;

                    case 3:
                        //logica del programa para elimnar una cuenta del archivo csv
                        Console.WriteLine("ELIMINACION DE UNA CUENTA DEL CATALOGO");
                        //verificamos que el catalogo exista dentro del code
                        if (!File.Exists(ruta_del_catalogo_de_cuentas))
                        {
                            Console.WriteLine("No se encontro el archivo.");
                            break;
                        }

                        //eliminamos la cuenta en base el codigo es mas facil que por el nombre 
                        Console.Write("Ingrese el codigo de la cuenta a eliminar: ");
                        string codigo_buscar = pedir_codigo_texto();

                        if (cuenta_tiene_movimientos(ruta_de_los_movimientos_contables, codigo_buscar))
                        {
                            Console.WriteLine("No se puede eliminar la cuenta porque ya tiene movimientos registrados");
                            break;
                        }

                        string[] lineas1_3 = File.ReadAllLines(ruta_del_catalogo_de_cuentas);

                        List<string> nuevas_lineas = new List<string>();
                        bool encontrada = false;

                        foreach (string linea in lineas1_3)
                        {
                            if (string.IsNullOrWhiteSpace(linea))
                                continue;

                            string[] datos = linea.Split(';');

                            if (es_encabezado(linea))
                            {
                                nuevas_lineas.Add(linea);
                                continue;
                            }

                            if (datos.Length > 0 && datos[0].Trim().ToLower() == codigo_buscar.ToLower())
                            {
                                encontrada = true;
                                Console.WriteLine($"La cuenta '{(datos.Length > 1 ? datos[1] : "")}' sera eliminada");
                                continue;
                            }

                            nuevas_lineas.Add(linea);
                        }

                        if (!encontrada)
                        {
                            Console.WriteLine("No se encontro la cuenta con ese codigo");
                            break;
                        }

                        File.WriteAllLines(ruta_del_catalogo_de_cuentas, nuevas_lineas);
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
            if (!File.Exists(ruta_de_los_movimientos_contables))
            {
                Console.WriteLine("No se encontro del archivo csv con los movimientos contable");
                break;
            }
            Console.WriteLine("Ingresa la fecha del asiento de la siguiente manera (dd/MM/yyyy)");
            string fecha_del_movimiento = pedir_fecha();
            int numero_de_asiento_nuevo = obtener_siguiente_asiento(ruta_de_los_movimientos_contables);
            List<string> lineas_temporales_movimiento = new List<string>();
            decimal suma_debe = 0;
            decimal suma_haber = 0;
            string cuenta_formateada_para_csv = "";
            bool agregando_cuentas = true;
            while (agregando_cuentas)
            {
                while (true)
                {
                    Console.WriteLine("Ingresa el codigo de la cuenta afectada");
                    string codigo_ingresado = pedir_codigo_texto();
                    string codigo_str = codigo_ingresado.ToString();
                    string[] lineas_catalogo = File.ReadAllLines(ruta_del_catalogo_de_cuentas);
                    bool cuenta_encontrada = false;
                    foreach (string linea_cat in lineas_catalogo)
                    {
                        if (string.IsNullOrWhiteSpace(linea_cat)) continue;
                        string[] datos_cat = linea_cat.Split(';');
                        if (es_encabezado(linea_cat)) continue;
                        if (datos_cat.Length >= 2 && datos_cat[0].Trim().Equals(codigo_str, StringComparison.OrdinalIgnoreCase))
                        {
                            cuenta_encontrada = true;
                            string codigo_cat = datos_cat[0].Trim();
                            string nombre_cat = datos_cat[1].Trim();
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
                        Console.WriteLine($"Codigo sin existencia {codigo_str} ");
                    }
                }
                string monto_debe_str = "0";
                string monto_haber_str = "0";
                decimal monto_actual = 0;
                while (true)
                {
                    Console.WriteLine("En donde se registra este movimiento?");
                    Console.WriteLine("1 Debe / 2 Haber");
                    int opcion_debe_haber = pedir_opcion_1_2();
                    Console.Write("Ingrese la cantidad: ");
                    string cantidad_ingresada_por_el_usuario = Console.ReadLine()?.Trim() ?? "";
                    if (decimal.TryParse(cantidad_ingresada_por_el_usuario, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out monto_actual) && monto_actual >= 0)
                    {
                        if (opcion_debe_haber == 1)
                        {
                            monto_debe_str = monto_actual.ToString("0.00", CultureInfo.InvariantCulture);
                            suma_debe += monto_actual;
                        }
                        else
                        {
                            monto_haber_str = monto_actual.ToString("0.00", CultureInfo.InvariantCulture);
                            suma_haber += monto_actual;
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Cantidad invalida, Intente de nuevo");
                    }
                }
                Console.WriteLine("Le pedimos el contexto del movimiento al usuario");
                string concepto_del_movimiento = Console.ReadLine() ?? "";
                concepto_del_movimiento = concepto_del_movimiento.Replace(";", ",").Trim();
                string nueva_linea_mov = $"{fecha_del_movimiento};{cuenta_formateada_para_csv};{numero_de_asiento_nuevo};{monto_debe_str};{monto_haber_str};{concepto_del_movimiento}";
                lineas_temporales_movimiento.Add(nueva_linea_mov);
                Console.WriteLine("Desea agregar otra cuenta a este registro");
                Console.WriteLine("1. si | 2. no");
                int continuar_agregando = pedir_opcion_1_2();
                if (continuar_agregando == 2)
                {
                    agregando_cuentas = false;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("RESUMEN DEL REGISTRO");
            Console.WriteLine($"Total Debe:  {suma_debe:0.00}");
            Console.WriteLine($"Total Haber: {suma_haber:0.00}");
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


        case 3: // Logica del switch numero 3: consultar catalogo de cuentas
            Console.WriteLine();
            Console.WriteLine("CATALOGO DE LAS CUENTAS");
            Console.WriteLine();

            if (!File.Exists(ruta_del_catalogo_de_cuentas))
            {
                Console.WriteLine($"No se encontro el archivo de las cuentas: {ruta_del_catalogo_de_cuentas}");
                break;
            }

            string[] lineas_catalogo_s3 = File.ReadAllLines(ruta_del_catalogo_de_cuentas);
            if (lineas_catalogo_s3.Length == 0)
            {
                Console.WriteLine("El archivo se encuentra vacio.");
                break;
            }

            Console.WriteLine($"{"Codigo",-10} | {"Cuenta",-40} | {"Tipo",-12} | {"Naturaleza",-12}");
            Console.WriteLine(new string('-', 78));

            bool primera_linea_s3 = true;
            foreach (string fila in lineas_catalogo_s3)
            {
                if (string.IsNullOrWhiteSpace(fila)) continue;

                if (primera_linea_s3 && es_encabezado(fila))
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
                    Console.WriteLine("La linea se encuentra incompleta, revisala.");
                }
            }
            break;

        case 4: // El switch 4 muestra los movimientos contables
            Console.WriteLine("MOSTRANDO LOS MOVIMIENTOS CONTABLES");
            if (!File.Exists(ruta_de_los_movimientos_contables))
            {
                Console.WriteLine($"No se encontro el archivo de movimientos en la ruta: {ruta_de_los_movimientos_contables}");
                break;
            }

            string[] lineas_csv_movimientos = File.ReadAllLines(ruta_de_los_movimientos_contables);
            if (lineas_csv_movimientos.Length == 0)
            {
                Console.WriteLine("Los movimientos estan vacios.");
                break;
            }

            Console.WriteLine($"{"FECHA",-10} | {"CUENTA",-50} | {"ASIENTO",-8} | {"DEBE",-10} | {"HABER",-10} | {"CONCEPTO"}");
            Console.WriteLine(new string('-', 105));

            bool primera_linea4 = true;
            foreach (string lineas in lineas_csv_movimientos)
            {
                if (string.IsNullOrWhiteSpace(lineas)) continue;

                if (primera_linea4 && es_encabezado(lineas))
                {
                    primera_linea4 = false;
                    continue;
                }

                string[] datos = lineas.Split(';');
                if (datos.Length >= 6)
                {
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
                    Console.WriteLine($"[Linea incompleta]: {lineas}");
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

    var catalogoResultados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        string codigo = datos[0].Trim();
        string tipo = datos[2].Trim().ToLower();

        if (tipo == "ingreso" || tipo == "costo" || tipo == "gasto")
        {
            catalogoResultados[codigo] = tipo;
        }
    }

    var ingresos = new Dictionary<string, decimal>();
    var costos = new Dictionary<string, decimal>();
    var gastos = new Dictionary<string, decimal>();

    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (es_encabezado(linea) || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 6) continue;

        string cuentaMov = datos[1].Trim();
        decimal debe = convertir_monto(datos[3]);
        decimal haber = convertir_monto(datos[4]);

        string tipoEncontrado = buscar_tipo_de_cuenta(cuentaMov, catalogoResultados);
        string nombreCuentaLimpio = cuentaMov;

        if (string.IsNullOrEmpty(tipoEncontrado)) continue;

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

    var catalogoBalance = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        string codigo = datos[0].Trim();
        string tipo = datos[2].Trim().ToLower();

        if (tipo == "activo" || tipo == "pasivo" || tipo == "patrimonio" || tipo == "capital")
        {
            catalogoBalance[codigo] = tipo;
        }
    }

    var activos = new Dictionary<string, decimal>();
    var pasivos = new Dictionary<string, decimal>();
    var patrimonio = new Dictionary<string, decimal>();

    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (es_encabezado(linea) || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 6) continue;

        string cuentaMov = datos[1].Trim();
        decimal debe = convertir_monto(datos[3]);
        decimal haber = convertir_monto(datos[4]);

        string tipoEncontrado = buscar_tipo_de_cuenta(cuentaMov, catalogoBalance);

        if (string.IsNullOrEmpty(tipoEncontrado)) continue;

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

    var catalogoResultados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;
        catalogoResultados[datos[0].Trim()] = datos[2].Trim().ToLower();
    }

    decimal totalIngresos = 0, totalCostos = 0, totalGastos = 0;
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea) || es_encabezado(linea) || linea.StartsWith(";")) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 6) continue;
        string cuentaMov = datos[1].Trim();
        decimal debe = convertir_monto(datos[3]);
        decimal haber = convertir_monto(datos[4]);

        string tipo_de_la_cuenta_mov = buscar_tipo_de_cuenta(cuentaMov, catalogoResultados);
        if (tipo_de_la_cuenta_mov == "ingreso") totalIngresos += (haber - debe);
        else if (tipo_de_la_cuenta_mov == "costo") totalCostos += (debe - haber);
        else if (tipo_de_la_cuenta_mov == "gasto") totalGastos += (debe - haber);
    }
    decimal utilidadNetaPeriodo = totalIngresos - (totalCostos + totalGastos);

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

    foreach (string linea in File.ReadLines(ruta_del_catalogo_de_cuentas, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        string codigo = datos[0].Trim();
        string nombre = datos[1].Trim();
        string tipo = datos[2].Trim();

        catalogoGeneral[codigo] = tipo;

        if (tipo.Equals("capital", StringComparison.OrdinalIgnoreCase) || tipo.Equals("patrimonio", StringComparison.OrdinalIgnoreCase))
        {
            catalogoPatrimonio[codigo] = nombre;
        }
    }

    decimal patrimonioInicial = 0;
    decimal aportes = 0;
    decimal retiros = 0;
    decimal totalIngresos = 0;
    decimal totalCostos = 0;
    decimal totalGastos = 0;

    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (es_encabezado(linea) || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 6) continue;

        string cuentaMov = datos[1].Trim();
        decimal debe = convertir_monto(datos[3]);
        decimal haber = convertir_monto(datos[4]);

        bool esPatrimonio = false;
        if (buscar_codigo_de_cuenta(cuentaMov, catalogoPatrimonio) != "")
        {
            esPatrimonio = true;
        }

        if (esPatrimonio)
        {
            string concepto = datos[5].Trim().ToLower();
            if (concepto.Contains("retiro") || concepto.Contains("dividendo"))
            {
                retiros += (debe - haber);
            }
            else
            {
                aportes += (haber - debe);
            }
        }

        string tipoCuenta = buscar_tipo_de_cuenta(cuentaMov, catalogoGeneral);
        if (tipoCuenta.Equals("ingreso", StringComparison.OrdinalIgnoreCase))
            totalIngresos += (haber - debe);
        else if (tipoCuenta.Equals("costo", StringComparison.OrdinalIgnoreCase))
            totalCostos += (debe - haber);
        else if (tipoCuenta.Equals("gasto", StringComparison.OrdinalIgnoreCase))
            totalGastos += (debe - haber);
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
    
    foreach (string linea in File.ReadLines(ruta_de_los_movimientos_contables, Encoding.UTF8))
    {
        if (string.IsNullOrWhiteSpace(linea)) continue;
        if (es_encabezado(linea) || linea.StartsWith(";")) continue;

        string[] datos = linea.Split(';');
        if (datos.Length < 6) continue;

        string cuentaMov = datos[1].Trim();
        string concepto = datos[5].Trim().ToLower();

        bool esEfectivo = cuentaMov.Contains("1101") || cuentaMov.Contains("1102") || 
                          cuentaMov.Contains("caja", StringComparison.OrdinalIgnoreCase) || 
                          cuentaMov.Contains("banco", StringComparison.OrdinalIgnoreCase);

        if (!esEfectivo) continue;

        decimal debe = convertir_monto(datos[3]);
        decimal haber = convertir_monto(datos[4]);
        decimal cambioEfectivo = debe - haber;

        if (concepto.Contains("aporte") || concepto.Contains("aportacion") || concepto.Contains("capital") || concepto.Contains("prestamo") || concepto.Contains("financiamiento") || concepto.Contains("socio"))
        {
            flujoFinanciamiento += cambioEfectivo;
        }
        else if (concepto.Contains("equipo") || concepto.Contains("mobiliario") || concepto.Contains("vehiculo") || concepto.Contains("maquinaria") || concepto.Contains("activo") || concepto.Contains("enganche") || concepto.Contains("compra de licencias"))
        {
            flujoInversion += cambioEfectivo;
        }
        else
        {
            flujoOperativo += cambioEfectivo;
        }
    }

    decimal flujoNetoTotal = flujoOperativo + flujoInversion + flujoFinanciamiento;

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
        continuar = false;
        break;
    }
}

void MostrarMenu()
{
    Console.WriteLine();
    Console.WriteLine("===============================");
    Console.WriteLine("BIENVENIDO AL SISTEMA CONTABLE");
    Console.WriteLine("===============================");
    Console.WriteLine();
    Console.WriteLine("Por favor elige la opcion que desees realizar");
    Console.WriteLine("1. Administrar catalogo de cuentas ");
    Console.WriteLine("2. Registrar movimiento contable ");
    Console.WriteLine("3. Consultar catalogo de cuentas ");
    Console.WriteLine("4. Consultar movimientos ");
    Console.WriteLine("5. Estado de Resultados ");
    Console.WriteLine("6. Estado de Situacion Financiera ");
    Console.WriteLine("7. Estado de Cambios en el Patrimonio ");
    Console.WriteLine("8. Estado de Flujos de Efectivo ");
    Console.WriteLine("9. Salir ");
    Console.WriteLine();
}

// CORREGIDO: Se valida que la opción no tenga signos (+ o -) para evitar entradas como "+8"
int PedirOpcion()
{
    while (true)
    {
        Console.Write("Seleccione una opcion: ");
        string entrada = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrEmpty(entrada) && entrada.All(char.IsDigit))
        {
            if (int.TryParse(entrada, out int opcion) && opcion >= 1 && opcion <= 9)
            {
                return opcion;
            }
        }
        Console.WriteLine("La opcion seleccionada es invalida, Intente de nuevo con una opcion entre el 1 y el 9");
    }
}

int pedir_opcion1_1()
{
    while (true)
    {
        Console.Write("Seleccione una opcion: ");
        string entrada = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrEmpty(entrada) && entrada.All(char.IsDigit))
        {
            if (int.TryParse(entrada, out int opcion) && opcion >= 1 && opcion <= 6)
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
        string entrada = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrEmpty(entrada) && entrada.All(char.IsDigit))
        {
            if (int.TryParse(entrada, out int opcion) && opcion >= 1 && opcion <= 4)
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
        string entrada = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrEmpty(entrada) && entrada.All(char.IsDigit))
        {
            if (int.TryParse(entrada, out int opcion) && opcion >= 0)
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
        string entrada = Console.ReadLine()?.Trim() ?? "";

        if (!string.IsNullOrEmpty(entrada) && entrada.All(char.IsDigit))
        {
            if (int.TryParse(entrada, out int opcion) && opcion >= 1 && opcion <= 2)
            {
                return opcion;
            }
        }
        Console.WriteLine("La opcion seleccionada es invalida, Intente de nuevo con una opcion entre el 1 y el 2");
    }
}

string pedir_nombre_cuenta(string ruta_catalogo)
{
    while (true)
    {
        string nombre = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nombre) || nombre.Contains(";"))
        {
            Console.WriteLine("El nombre de la cuenta no puede estar vacio, ni contener punto y coma (;)");
            continue;
        }

        bool existe = false;
        string[] lineas = File.ReadAllLines(ruta_catalogo);

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            string[] datos = linea.Split(';');

            if (datos.Length > 1)
            {
                string cuenta_existente = datos[1].Trim();
                if (cuenta_existente.ToLower() == nombre.ToLower())
                {
                    existe = true;
                    break;
                }
            }
        }

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
    Console.WriteLine(" ADMINISTRAR CATALOGO DE CUENTAS");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Ver catalogo de cuentas");
    Console.WriteLine("2. Agregar nueva cuenta");
    Console.WriteLine("3. Eliminar una cuenta");
    Console.WriteLine("4. Volver al menu principal");
    Console.Write("Indique la accion que desea realizar: ");
    Console.WriteLine();
}

string pedir_fecha()
{
    while (true)
    {
        string entrada = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("La fecha no puede quedar vacia ");
            continue;
        }
        try
        {
            if (DateTime.TryParseExact(entrada, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime fecha_valida))
            {
                if (fecha_valida.Year >= 2000 && fecha_valida.Year <= DateTime.Now.Year)
                {
                    return fecha_valida.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Console.WriteLine("La fecha se encuentra fuera del rango de registro");
                }
            }
            else
            {
                Console.WriteLine("Formato invalido de fecha por favor intentelo con el formato dd/mm/yyyy");
            }
        }
        catch (Exception)
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
            if (entrada.Contains(";"))
            {
                Console.WriteLine("El codigo no puede llevar punto y coma (;). Intente de nuevo.");
                continue;
            }
            return entrada;
        }
        Console.WriteLine("El codigo no puede estar vacio. Intente de nuevo.");
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

string quitar_acentos(string texto)
{
    string normalizado = texto.Normalize(NormalizationForm.FormD);
    StringBuilder resultado = new StringBuilder();
    foreach (char letra in normalizado)
    {
        if (CharUnicodeInfo.GetUnicodeCategory(letra) != UnicodeCategory.NonSpacingMark)
        {
            resultado.Append(letra);
        }
    }
    return resultado.ToString().Normalize(NormalizationForm.FormC);
}

bool es_encabezado(string linea)
{
    string limpia = quitar_acentos(linea).Trim().TrimStart('\uFEFF').ToLower();
    return limpia.StartsWith("codigo") || limpia.StartsWith("fecha");
}

// CORREGIDO: Conversión inteligente de montos para manejar formatos con miles o decimales sin recortar ceros
decimal convertir_monto(string texto)
{
    string s = texto.Trim().Replace(" ", "");
    if (string.IsNullOrEmpty(s)) return 0m;

    if (s.Contains(',') && s.Contains('.'))
    {
        if (s.LastIndexOf('.') > s.LastIndexOf(','))
            s = s.Replace(",", "");
        else
            s = s.Replace(".", "").Replace(",", ".");
    }
    else if (s.Contains(','))
    {
        string[] partes = s.Split(',');
        if (partes.Length == 2 && (partes[1].Length == 1 || partes[1].Length == 2))
            s = s.Replace(",", ".");
        else
            s = s.Replace(",", "");
    }
    else if (s.Contains('.'))
    {
        string[] partes = s.Split('.');
        if (partes.Length == 2 && partes[1].Length == 3 && partes[0] != "0" && partes[0] != "")
            s = s.Replace(".", "");
    }

    if (decimal.TryParse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal resultado))
    {
        return resultado;
    }
    return 0m;
}

string buscar_codigo_de_cuenta(string cuenta_mov, Dictionary<string, string> catalogo)
{
    foreach (string codigo_cat in catalogo.Keys.OrderByDescending(k => k.Length))
    {
        if (cuenta_mov.Equals(codigo_cat, StringComparison.OrdinalIgnoreCase) ||
            cuenta_mov.StartsWith(codigo_cat + "-", StringComparison.OrdinalIgnoreCase))
        {
            return codigo_cat;
        }
    }
    return "";
}

string buscar_tipo_de_cuenta(string cuenta_mov, Dictionary<string, string> catalogo)
{
    string codigo_encontrado = buscar_codigo_de_cuenta(cuenta_mov, catalogo);
    if (codigo_encontrado == "")
    {
        return "";
    }
    return catalogo[codigo_encontrado];
}

bool cuenta_tiene_movimientos(string ruta_movimientos, string codigo)
{
    if (!File.Exists(ruta_movimientos)) return false;

    foreach (string linea in File.ReadLines(ruta_movimientos))
    {
        if (string.IsNullOrWhiteSpace(linea) || es_encabezado(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 2) continue;

        string cuenta_mov = datos[1].Trim();
        if (cuenta_mov.Equals(codigo, StringComparison.OrdinalIgnoreCase) ||
            cuenta_mov.StartsWith(codigo + "-", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
    }
    return false;
}

int obtener_siguiente_asiento(string ruta_movimientos)
{
    int mayor_asiento = 0;
    foreach (string linea in File.ReadLines(ruta_movimientos))
    {
        if (string.IsNullOrWhiteSpace(linea) || es_encabezado(linea)) continue;
        string[] datos = linea.Split(';');
        if (datos.Length < 3) continue;

        if (int.TryParse(datos[2].Trim(), out int numero_asiento) && numero_asiento > mayor_asiento)
        {
            mayor_asiento = numero_asiento;
        }
    }
    return mayor_asiento + 1;
}