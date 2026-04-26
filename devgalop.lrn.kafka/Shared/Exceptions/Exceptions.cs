namespace devgalop.lrn.kafka.Shared.Exceptions;

public class MissingConfigurationException (string key) 
: Exception ($"No se ha configurado la clave: {key} dentro de las variables de entorno o el archivo de configuración.")
{
}
