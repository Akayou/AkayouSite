using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkayouSite
{
    public enum Resultado
    {
        OK, 
        USUARIO_INEXISTENTE, 
        PRIMEIRO_ACESSO, 
        INFORMACAO_INSUFICIENTE, 
        CADASTRO_NAO_ENCONTRADO
    }
}